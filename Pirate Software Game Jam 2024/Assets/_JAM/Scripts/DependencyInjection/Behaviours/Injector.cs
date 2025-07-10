using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DesignPatterns.DependencyInjection.Attributes;
using DesignPatterns.DependencyInjection.Interfaces;
using DesignPatterns.Singleton;
using JetBrains.Annotations;
using Logging;
using UnityEngine;

namespace DesignPatterns.DependencyInjection.Behaviours
{
    [DefaultExecutionOrder(-1000)]
    public class Injector : Singleton<Injector>, IProvider, IDynamicObjectProvider, IObjectGetter
    {
        [Provide, UsedImplicitly] private IDynamicObjectProvider DynamicObjectProvider() => this;
        [Provide, UsedImplicitly] private IObjectGetter InjectorGetterInterface() => this;

        // Access to the logging manager is required before dependency injection is complete
        [SerializeField] private LoggingManager loggingManager;

        private const BindingFlags ProvideBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private const BindingFlags InjectBindingFlags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly Dictionary<Type, object> _providedObjectsDictionary = new();
        private readonly Dictionary<Type, object> _dynamicProvidedObjectsDictionary = new();

        protected override void Awake()
        {
            base.Awake();

            var monoBehaviours = GetMonoBehaviours();

            // Get all objects from existing providers and self providers
            HandleProviders(monoBehaviours);

            // Inject all dependencies
            HandleInjections(monoBehaviours);
        }

        private void HandleProviders(IEnumerable objects)
        {
            foreach (var obj in objects)
            {
                if (obj is ISelfProvider selfProvider)
                {
                    AddProvidedObjectFromSelfProviderToDictionary(selfProvider);
                }

                if (obj is IProvider provider)
                {
                    AddProvidedObjectsFromProviderToDictionary(provider);
                }
            }
        }

        // Public so that it can be called from requesters
        public void HandleInjections(IEnumerable objects)
        {
            foreach (var obj in objects)
            {
                TryInjectRequestedObjects(obj);
            }
        }

        private static MonoBehaviour[] GetMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        }

        private void AddProvidedObjectFromSelfProviderToDictionary(object obj)
        {
            var type = obj.GetType();

            if (!_providedObjectsDictionary.TryAdd(type, obj))
            {
                throw new Exception($"Injector was already provided an object of type {type.Name}");
            }

            loggingManager.Log(this, $"Injector was provided an object of type {type.Name}");
        }

        private void AddProvidedObjectsFromProviderToDictionary(object obj)
        {
            var type = obj.GetType();
            var fields = type.GetFields(ProvideBindingFlags);

            foreach (var field in fields)
            {
                if (!Attribute.IsDefined(field, typeof(ProvideAttribute))) continue;

                var fieldType = field.FieldType;
                var fieldObject = field.GetValue(obj);

                if (fieldObject != null)
                {
                    if (!_providedObjectsDictionary.TryAdd(fieldType, fieldObject))
                    {
                        throw new Exception($"Injector was already provided an object of type {fieldType.Name} making field {field.Name} from provider of type {type.Name} redundant");
                    }

                    loggingManager.Log(this, $"Injector was provided an object of type {fieldType.Name} using field {field.Name} from provider of type {type.Name}");
                }
                else
                {
                    throw new Exception($"Injector was provided a null object of type {fieldType.Name} using field {field.Name} from provider of type {type.Name}");
                }
            }

            var methods = type.GetMethods(ProvideBindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

                var returnType = method.ReturnType;
                var returnObject = method.Invoke(obj, null);

                if (returnObject != null)
                {
                    if (!_providedObjectsDictionary.TryAdd(returnType, returnObject))
                    {
                        throw new Exception($"Injector was already provided an object of type {returnType.Name} making method {method.Name} from provider of type {type.Name} redundant");
                    }

                    loggingManager.Log(this, $"Injector was provided an object of type {returnType.Name} using method {method.Name} from provider of type {type.Name}");
                }
                else
                {
                    throw new Exception($"Injector was provided a null object of type {returnType.Name} using method {method.Name} from provider of type {type.Name}");
                }
            }
        }

        private static bool IsTypeRequestingInjections(Type type) => type.GetMembers(InjectBindingFlags).Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

        private void TryInjectRequestedObjects(object obj)
        {
            var type = obj.GetType();

            if (!IsTypeRequestingInjections(type)) return;

            var fields = type.GetFields(InjectBindingFlags);

            foreach (var field in fields)
            {
                if (!Attribute.IsDefined(field, typeof(InjectAttribute))) continue;

                var fieldType = field.FieldType;

                if (_providedObjectsDictionary.TryGetValue(fieldType, out var providedObject))
                {
                    field.SetValue(obj, providedObject);
                    loggingManager.Log(this, $"Injector injected object of type {fieldType.Name} into field {field.Name} on object of type {type.Name}");
                }
                else
                {
                    throw new Exception($"Injector could not find object of type {fieldType.Name} for field {field.Name} on object of type {type.Name}");
                }
            }

            var methods = type.GetMethods(InjectBindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(InjectAttribute))) continue;

                var parameterTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
                var parameters = parameterTypes.Select(parameterType => _providedObjectsDictionary.GetValueOrDefault(parameterType)).ToArray();

                if (parameters.All(parameter => parameter != null))
                {
                    method.Invoke(obj, parameters);
                    loggingManager.Log(this, $"Injector injected object(s) for method {method.Name} on object of type {type.Name}");
                }
                else
                {
                    var missingParameterTypes = parameters.Select((parameter, index) => parameter == null ? index : -1).Where(index => index != -1).Select(index => parameterTypes[index].ToString()).ToArray();

                    throw new Exception($"Injector could not find object(s) of type(s) {missingParameterTypes} for method {method.Name} on object of type {type.Name}");
                }
            }
        }

        public T GetDynamicObject<T>()
        {
            if (!_dynamicProvidedObjectsDictionary.TryGetValue(typeof(T), out var obj))
                throw new Exception($"Object of type {typeof(T).Name} is not registered");

            return (T)obj;
        }

        public void RegisterDynamicObject(object obj)
        {
            if (!_dynamicProvidedObjectsDictionary.TryAdd(obj.GetType(), obj))
                throw new Exception($"Object of type {obj.GetType().Name} is already registered");
        }

        public void DeregisterDynamicObject(object obj)
        {
            if (!_dynamicProvidedObjectsDictionary.Remove(obj.GetType(), out _))
                throw new Exception($"Object of type {obj.GetType().Name} is not registered");
        }

        public T GetObject<T>()
        {
            if (!_providedObjectsDictionary.TryGetValue(typeof(T), out var obj))
                throw new Exception($"Object of type{typeof(T).Name} is not registered");
            return (T)obj;
        }
    }
}

public interface IObjectGetter
{
    T GetObject<T>();
}