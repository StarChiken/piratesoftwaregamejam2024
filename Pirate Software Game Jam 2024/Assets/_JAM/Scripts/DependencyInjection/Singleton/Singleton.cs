using UnityEngine;

namespace DesignPatterns.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static bool HasInstance => _instance;

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

                _instance = FindFirstObjectByType<T>();

                if (_instance) return _instance;

                // Create game object and component and let awake deal with assigning and setting up the instance
                new GameObject(typeof(T).Name).AddComponent<T>();

                return _instance;
            }
        }

        public static bool TryGetInstance(out T instance)
        {
            instance = _instance;

            return HasInstance;
        }

        private static T _instance;

        protected virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}