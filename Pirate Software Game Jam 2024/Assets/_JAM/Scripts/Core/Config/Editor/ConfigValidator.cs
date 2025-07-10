using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Base.Core.Config.Editor
{
    /// <summary>
    /// Validates all configs using UI Toolkit for a modern interface.
    /// </summary>
    public class ConfigValidator : EditorWindow
    {
        private ListView validationListView;
        private Toggle errorsToggle;
        private Toggle warningsToggle;
        private Toggle infoToggle;
        private List<ValidationResult> validationResults = new();

        public class ValidationResult
        {
            public BaseConfig Config;
            public string Message;
            public MessageType Type;
            public string PropertyPath;
            public System.Action FixAction;
        }

        [MenuItem("Tools/Configs/Validate All Configs")]
        public static void ShowWindow()
        {
            var window = GetWindow<ConfigValidator>("Config Validator");
            window.minSize = new Vector2(600, 500);
            window.Show();
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;
            root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_JAM/Scripts/Core/Config/Editor/ConfigValidator.uss"));

            // Header
            var header = new VisualElement();
            header.AddToClassList("header");
            header.Add(new Label("Config Validation Tool") { name = "title" });
            header.Add(new Label("Checks all configs for common issues and provides fixes") { name = "subtitle" });
            root.Add(header);

            // Toolbar
            var toolbar = new VisualElement();
            toolbar.AddToClassList("toolbar");
            
            errorsToggle = new Toggle("Errors") { value = true };
            errorsToggle.RegisterValueChangedCallback(OnFilterChanged);
            toolbar.Add(errorsToggle);
            
            warningsToggle = new Toggle("Warnings") { value = true };
            warningsToggle.RegisterValueChangedCallback(OnFilterChanged);
            toolbar.Add(warningsToggle);
            
            infoToggle = new Toggle("Info") { value = true };
            infoToggle.RegisterValueChangedCallback(OnFilterChanged);
            toolbar.Add(infoToggle);
            
            var refreshButton = new Button(() => ValidateAllConfigs()) { text = "Refresh" };
            toolbar.Add(refreshButton);
            
            var fixAllButton = new Button(() => FixAllIssues()) { text = "Fix All" };
            toolbar.Add(fixAllButton);
            
            root.Add(toolbar);

            // Validation List
            validationListView = new ListView();
            validationListView.makeItem = () => CreateValidationItem();
            validationListView.bindItem = BindValidationItem;
            validationListView.itemsSource = validationResults;
            validationListView.selectionType = SelectionType.Single;
            validationListView.AddToClassList("validation-list");
            root.Add(validationListView);

            // Footer
            var footer = new VisualElement();
            footer.AddToClassList("footer");
            
            var statsLabel = new Label();
            statsLabel.name = "stats";
            footer.Add(statsLabel);
            
            root.Add(footer);

            ValidateAllConfigs();
        }

        private VisualElement CreateValidationItem()
        {
            var item = new VisualElement();
            item.AddToClassList("validation-item");
            
            var header = new VisualElement();
            header.AddToClassList("validation-header");
            
            var icon = new VisualElement();
            icon.AddToClassList("validation-icon");
            header.Add(icon);
            
            var content = new VisualElement();
            content.AddToClassList("validation-content");
            
            var configName = new Label();
            configName.AddToClassList("config-name");
            content.Add(configName);
            
            var message = new Label();
            message.AddToClassList("validation-message");
            content.Add(message);
            
            var propertyPath = new Label();
            propertyPath.AddToClassList("property-path");
            content.Add(propertyPath);
            
            header.Add(content);
            item.Add(header);
            
            var fixButton = new Button() { text = "Fix" };
            fixButton.AddToClassList("fix-button");
            item.Add(fixButton);
            
            return item;
        }

        private void BindValidationItem(VisualElement element, int index)
        {
            if (index >= validationResults.Count) return;
            
            var result = validationResults[index];
            var icon = element.Q<VisualElement>("validation-icon");
            var configName = element.Q<Label>("config-name");
            var message = element.Q<Label>("validation-message");
            var propertyPath = element.Q<Label>("property-path");
            var fixButton = element.Q<Button>("fix-button");
            
            // Set icon based on type
            icon.ClearClassList();
            icon.AddToClassList("validation-icon");
            icon.AddToClassList(GetIconClass(result.Type));
            
            configName.text = $"{result.Config.name} ({result.Config.GetType().Name})";
            message.text = result.Message;
            propertyPath.text = string.IsNullOrEmpty(result.PropertyPath) ? "" : $"Property: {result.PropertyPath}";
            
            fixButton.style.display = result.FixAction != null ? DisplayStyle.Flex : DisplayStyle.None;
            fixButton.RegisterCallback<ClickEvent>(_ => {
                result.FixAction?.Invoke();
                ValidateAllConfigs();
            });
        }

        private string GetIconClass(MessageType type)
        {
            return type switch
            {
                MessageType.Error => "icon-error",
                MessageType.Warning => "icon-warning",
                MessageType.Info => "icon-info",
                _ => "icon-info"
            };
        }

        private void OnFilterChanged(ChangeEvent<bool> evt)
        {
            UpdateValidationList();
        }

        private void UpdateValidationList()
        {
            var filteredResults = validationResults.Where(r => 
                (errorsToggle.value && r.Type == MessageType.Error) ||
                (warningsToggle.value && r.Type == MessageType.Warning) ||
                (infoToggle.value && r.Type == MessageType.Info)
            ).ToList();
            
            validationListView.itemsSource = filteredResults;
            validationListView.Rebuild();
            
            UpdateStats();
        }

        private void UpdateStats()
        {
            var errorCount = validationResults.Count(r => r.Type == MessageType.Error);
            var warningCount = validationResults.Count(r => r.Type == MessageType.Warning);
            var infoCount = validationResults.Count(r => r.Type == MessageType.Info);
            
            var statsLabel = rootVisualElement.Q<Label>("stats");
            statsLabel.text = $"Total: {validationResults.Count} | Errors: {errorCount} | Warnings: {warningCount} | Info: {infoCount}";
        }

        private void ValidateAllConfigs()
        {
            validationResults.Clear();
            
            var configs = ConfigManager.GetAllConfigs();
            
            foreach (var config in configs)
            {
                ValidateConfig(config);
            }
            
            UpdateValidationList();
        }

        private void ValidateConfig(BaseConfig config)
        {
            var serializedObject = new SerializedObject(config);
            var iterator = serializedObject.GetIterator();
            
            while (iterator.NextVisible(true))
            {
                ValidateProperty(iterator, config);
            }
        }

        private void ValidateProperty(SerializedProperty property, BaseConfig config)
        {
            var propertyPath = property.propertyPath;
            
            // Check for null object references
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceValue == null)
                {
                    validationResults.Add(new ValidationResult
                    {
                        Config = config,
                        Message = $"Missing reference: {property.displayName}",
                        Type = MessageType.Error,
                        PropertyPath = propertyPath,
                        FixAction = () => Debug.Log($"Please assign a value to {property.displayName} in {config.name}")
                    });
                }
            }
            
            // Check for empty strings
            else if (property.propertyType == SerializedPropertyType.String)
            {
                if (string.IsNullOrEmpty(property.stringValue))
                {
                    validationResults.Add(new ValidationResult
                    {
                        Config = config,
                        Message = $"Empty string: {property.displayName}",
                        Type = MessageType.Warning,
                        PropertyPath = propertyPath,
                        FixAction = () => Debug.Log($"Please provide a value for {property.displayName} in {config.name}")
                    });
                }
            }
            
            // Check for negative values
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                if (property.intValue < 0)
                {
                    validationResults.Add(new ValidationResult
                    {
                        Config = config,
                        Message = $"Negative value: {property.displayName} = {property.intValue}",
                        Type = MessageType.Warning,
                        PropertyPath = propertyPath,
                        FixAction = () => 
                        {
                            property.intValue = 0;
                            property.serializedObject.ApplyModifiedProperties();
                            EditorUtility.SetDirty(config);
                        }
                    });
                }
            }
            
            // Check for negative float values
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                if (property.floatValue < 0)
                {
                    validationResults.Add(new ValidationResult
                    {
                        Config = config,
                        Message = $"Negative value: {property.displayName} = {property.floatValue}",
                        Type = MessageType.Warning,
                        PropertyPath = propertyPath,
                        FixAction = () => 
                        {
                            property.floatValue = 0;
                            property.serializedObject.ApplyModifiedProperties();
                            EditorUtility.SetDirty(config);
                        }
                    });
                }
            }
            
            // Check for empty arrays/lists
            else if (property.propertyType == SerializedPropertyType.ArraySize)
            {
                if (property.arraySize == 0)
                {
                    validationResults.Add(new ValidationResult
                    {
                        Config = config,
                        Message = $"Empty array: {property.displayName}",
                        Type = MessageType.Info,
                        PropertyPath = propertyPath,
                        FixAction = null
                    });
                }
            }
        }

        private void FixAllIssues()
        {
            var fixableResults = validationResults.Where(r => r.FixAction != null).ToList();
            
            foreach (var result in fixableResults)
            {
                result.FixAction();
            }
            
            ValidateAllConfigs();
            Debug.Log($"Fixed {fixableResults.Count} issues");
        }
    }
} 