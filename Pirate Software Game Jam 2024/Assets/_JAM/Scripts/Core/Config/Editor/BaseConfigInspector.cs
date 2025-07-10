using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Base.Core.Config.Editor
{
    /// <summary>
    /// UI Toolkit-based custom inspector for BaseConfig.
    /// </summary>
    [CustomEditor(typeof(BaseConfig), true)]
    public class BaseConfigInspector : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var config = target as BaseConfig;
            
            if (config == null) return root;
            
            // Add default inspector
            var defaultInspector = new IMGUIContainer(() => DrawDefaultInspector());
            root.Add(defaultInspector);
            
            // Add custom sections
            root.Add(CreateValidationSection(config));
            root.Add(CreateDebugSection(config));
            root.Add(CreateActionsSection(config));
            
            return root;
        }
        
        private VisualElement CreateValidationSection(BaseConfig config)
        {
            var section = new Foldout { text = "Validation", value = true };
            
            var validationResults = ValidateConfig(config);
            if (validationResults.Count == 0)
            {
                section.Add(new HelpBox("✓ All validation checks passed!", HelpBoxMessageType.Info));
            }
            else
            {
                section.Add(new HelpBox($"⚠ Found {validationResults.Count} validation issues:", HelpBoxMessageType.Warning));
                foreach (var result in validationResults)
                {
                    var item = new VisualElement();
                    item.style.flexDirection = FlexDirection.Row;
                    item.style.marginTop = 2;
                    
                    var bullet = new Label("•") { style = { marginRight = 5, color = Color.red } };
                    var message = new Label(result) { style = { flexGrow = 1 } };
                    
                    item.Add(bullet);
                    item.Add(message);
                    section.Add(item);
                }
            }
            
            return section;
        }
        
        private VisualElement CreateDebugSection(BaseConfig config)
        {
            var section = new Foldout { text = "Debug Info", value = false };
            
            var infoContainer = new VisualElement();
            infoContainer.style.marginTop = 5;
            
            infoContainer.Add(new Label($"Config Type: {config.GetType().Name}"));
            infoContainer.Add(new Label($"Config Name: {config.name}"));
            infoContainer.Add(new Label($"Asset Path: {AssetDatabase.GetAssetPath(config)}"));
            infoContainer.Add(new Label($"Config File Name: {config.GetType().Name}"));
            
            var isLoaded = ConfigManager.Instance != null;
            if (isLoaded)
            {
                var method = typeof(ConfigManager).GetMethod("GetConfig").MakeGenericMethod(config.GetType());
                var loadedConfig = method.Invoke(ConfigManager.Instance, null) as BaseConfig;
                isLoaded = loadedConfig != null;
                infoContainer.Add(new Label($"Loaded Config: {loadedConfig?.name ?? "None"}"));
            }
            infoContainer.Add(new Label($"Is Loaded: {isLoaded}"));
            
            section.Add(infoContainer);
            return section;
        }
        
        private VisualElement CreateActionsSection(BaseConfig config)
        {
            var section = new VisualElement();
            section.style.marginTop = 10;
            
            var buttonContainer = new VisualElement();
            buttonContainer.style.flexDirection = FlexDirection.Row;
            buttonContainer.style.justifyContent = Justify.SpaceBetween;
            
            var resetButton = new Button(() => ResetConfigToDefaults(config)) { text = "Reset to Defaults" };
            resetButton.style.backgroundColor = new Color(0.8f, 0.4f, 0.4f);
            resetButton.style.color = Color.white;
            buttonContainer.Add(resetButton);
            
            var duplicateButton = new Button(() => DuplicateConfig(config)) { text = "Duplicate Config" };
            duplicateButton.style.backgroundColor = new Color(0.4f, 0.6f, 0.8f);
            duplicateButton.style.color = Color.white;
            buttonContainer.Add(duplicateButton);
            
            var findButton = new Button(() => FindReferences(config)) { text = "Find References" };
            findButton.style.backgroundColor = new Color(0.4f, 0.8f, 0.4f);
            findButton.style.color = Color.white;
            buttonContainer.Add(findButton);
            
            section.Add(buttonContainer);
            return section;
        }
        
        private List<string> ValidateConfig(BaseConfig config)
        {
            var issues = new List<string>();
            var serializedObject = new SerializedObject(config);
            var iterator = serializedObject.GetIterator();
            
            while (iterator.NextVisible(true))
            {
                if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                {
                    if (iterator.objectReferenceValue == null)
                    {
                        issues.Add($"Missing reference: {iterator.displayName}");
                    }
                }
                else if (iterator.propertyType == SerializedPropertyType.String)
                {
                    if (string.IsNullOrEmpty(iterator.stringValue))
                    {
                        issues.Add($"Empty string: {iterator.displayName}");
                    }
                }
                else if (iterator.propertyType == SerializedPropertyType.Integer)
                {
                    if (iterator.intValue < 0)
                    {
                        issues.Add($"Negative value: {iterator.displayName} = {iterator.intValue}");
                    }
                }
                else if (iterator.propertyType == SerializedPropertyType.Float)
                {
                    if (iterator.floatValue < 0)
                    {
                        issues.Add($"Negative value: {iterator.displayName} = {iterator.floatValue}");
                    }
                }
            }
            
            return issues;
        }
        
        private void CopySerializedProperty(SerializedProperty source, SerializedProperty dest)
        {
            switch (source.propertyType)
            {
                case SerializedPropertyType.Integer:
                    dest.intValue = source.intValue;
                    break;
                case SerializedPropertyType.Float:
                    dest.floatValue = source.floatValue;
                    break;
                case SerializedPropertyType.String:
                    dest.stringValue = source.stringValue;
                    break;
                case SerializedPropertyType.Boolean:
                    dest.boolValue = source.boolValue;
                    break;
                case SerializedPropertyType.Vector2:
                    dest.vector2Value = source.vector2Value;
                    break;
                case SerializedPropertyType.Vector3:
                    dest.vector3Value = source.vector3Value;
                    break;
                case SerializedPropertyType.Vector4:
                    dest.vector4Value = source.vector4Value;
                    break;
                case SerializedPropertyType.Color:
                    dest.colorValue = source.colorValue;
                    break;
                case SerializedPropertyType.ObjectReference:
                    dest.objectReferenceValue = source.objectReferenceValue;
                    break;
                case SerializedPropertyType.ArraySize:
                    dest.arraySize = source.arraySize;
                    break;
                default:
                    // For complex types, we'll just copy the serialized data
                    dest.serializedObject.CopyFromSerializedProperty(source);
                    break;
            }
        }
        
        private void ResetConfigToDefaults(BaseConfig config)
        {
            if (EditorUtility.DisplayDialog("Reset Config", 
                "Are you sure you want to reset this config to default values?", 
                "Reset", "Cancel"))
            {
                var defaultConfig = CreateInstance(config.GetType());
                var serializedObject = new SerializedObject(config);
                var defaultSerializedObject = new SerializedObject(defaultConfig);
                
                var iterator = defaultSerializedObject.GetIterator();
                while (iterator.NextVisible(true))
                {
                    var property = serializedObject.FindProperty(iterator.name);
                    if (property != null)
                    {
                        CopySerializedProperty(iterator, property);
                    }
                }
                
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(config);
                Debug.Log($"Reset {config.name} to default values");
            }
        }
        
        private void DuplicateConfig(BaseConfig original)
        {
            var path = AssetDatabase.GetAssetPath(original);
            var directory = System.IO.Path.GetDirectoryName(path);
            var filename = System.IO.Path.GetFileNameWithoutExtension(path);
            var extension = System.IO.Path.GetExtension(path);
            
            var newPath = AssetDatabase.GenerateUniqueAssetPath($"{directory}/{filename}_Copy{extension}");
            AssetDatabase.CopyAsset(path, newPath);
            AssetDatabase.Refresh();
            
            var newConfig = AssetDatabase.LoadAssetAtPath<BaseConfig>(newPath);
            Selection.activeObject = newConfig;
            EditorGUIUtility.PingObject(newConfig);
        }
        
        private void FindReferences(BaseConfig config)
        {
            var references = new List<Object>();
            var allAssets = AssetDatabase.FindAssets("t:Object");
            
            foreach (var guid in allAssets)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                
                if (asset != null && asset != config)
                {
                    var serializedObject = new SerializedObject(asset);
                    var iterator = serializedObject.GetIterator();
                    
                    while (iterator.NextVisible(true))
                    {
                        if (iterator.propertyType == SerializedPropertyType.ObjectReference)
                        {
                            if (iterator.objectReferenceValue == config)
                            {
                                references.Add(asset);
                                break;
                            }
                        }
                    }
                }
            }
            
            if (references.Count > 0)
            {
                Selection.objects = references.ToArray();
                EditorGUIUtility.PingObject(references[0]);
                Debug.Log($"Found {references.Count} references to {config.name}");
            }
            else
            {
                Debug.Log($"No references found for {config.name}");
            }
        }
    }
} 