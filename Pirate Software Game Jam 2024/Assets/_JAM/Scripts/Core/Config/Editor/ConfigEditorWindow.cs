using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Base.Core.Config.Editor
{
    /// <summary>
    /// Main editor window for managing all game configs using UI Toolkit.
    /// </summary>
    public class ConfigEditorWindow : EditorWindow
    {
        private ListView configListView;
        private TextField searchField;
        private DropdownField categoryDropdown;
        private List<BaseConfig> allConfigs = new();
        private List<BaseConfig> filteredConfigs = new();
        private ConfigManager m_configManager;

        public static void SetConfigManager(ConfigManager configManager)
        {
            // Allow injection from editor setup or test harness
            m_configManager = configManager;
        }

        [MenuItem("Tools/Game Configs")]
        public static void ShowWindow()
        {
            var window = GetWindow<ConfigEditorWindow>("Game Configs");
            window.minSize = new Vector2(500, 600);
            window.Show();
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;
            root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_JAM/Scripts/Core/Config/Editor/ConfigEditorWindow.uss"));

            // Header
            var header = new VisualElement();
            header.AddToClassList("header");
            header.Add(new Label("Game Configuration Manager") { name = "title" });
            header.Add(new Label("Manage all game configs from one place") { name = "subtitle" });
            root.Add(header);

            // Toolbar
            var toolbar = new VisualElement();
            toolbar.AddToClassList("toolbar");
            
            categoryDropdown = new DropdownField("Category", GetCategoryOptions(), 0);
            categoryDropdown.RegisterValueChangedCallback(OnCategoryChanged);
            toolbar.Add(categoryDropdown);
            
            searchField = new TextField("Search");
            searchField.RegisterValueChangedCallback(OnSearchChanged);
            toolbar.Add(searchField);
            
            var refreshButton = new Button(() => RefreshConfigs()) { text = "Refresh" };
            toolbar.Add(refreshButton);
            
            root.Add(toolbar);

            // Config List
            configListView = new ListView();
            configListView.makeItem = () => CreateConfigItem();
            configListView.bindItem = BindConfigItem;
            configListView.itemsSource = filteredConfigs;
            configListView.selectionType = SelectionType.Single;
            configListView.AddToClassList("config-list");
            root.Add(configListView);

            // Footer
            var footer = new VisualElement();
            footer.AddToClassList("footer");
            
            var createButton = new Button(() => ShowCreateMenu()) { text = "Create New Config" };
            footer.Add(createButton);
            
            var generateButton = new Button(() => GenerateAllConfigs()) { text = "Generate All Configs" };
            footer.Add(generateButton);
            
            root.Add(footer);

            RefreshConfigs();
        }

        private VisualElement CreateConfigItem()
        {
            var item = new VisualElement();
            item.AddToClassList("config-item");
            
            var header = new VisualElement();
            header.AddToClassList("config-header");
            
            var nameLabel = new Label();
            nameLabel.AddToClassList("config-name");
            header.Add(nameLabel);
            
            var typeLabel = new Label();
            typeLabel.AddToClassList("config-type");
            header.Add(typeLabel);
            
            item.Add(header);
            
            var pathLabel = new Label();
            pathLabel.AddToClassList("config-path");
            item.Add(pathLabel);
            
            var actions = new VisualElement();
            actions.AddToClassList("config-actions");
            
            var editButton = new Button() { text = "Edit" };
            editButton.AddToClassList("action-button");
            actions.Add(editButton);
            
            var duplicateButton = new Button() { text = "Duplicate" };
            duplicateButton.AddToClassList("action-button");
            actions.Add(duplicateButton);
            
            item.Add(actions);
            
            return item;
        }

        private void BindConfigItem(VisualElement element, int index)
        {
            if (index >= filteredConfigs.Count) return;
            
            var config = filteredConfigs[index];
            var nameLabel = element.Q<Label>("config-name");
            var typeLabel = element.Q<Label>("config-type");
            var pathLabel = element.Q<Label>("config-path");
            var editButton = element.Q<Button>("edit-button");
            var duplicateButton = element.Q<Button>("duplicate-button");
            
            nameLabel.text = config.name;
            typeLabel.text = config.GetType().Name;
            pathLabel.text = AssetDatabase.GetAssetPath(config);
            
            editButton?.RegisterCallback<ClickEvent>(_ => {
                Selection.activeObject = config;
                EditorGUIUtility.PingObject(config);
            });
            
            duplicateButton?.RegisterCallback<ClickEvent>(_ => DuplicateConfig(config));
        }

        private List<string> GetCategoryOptions()
        {
            return new List<string> { "All", "Gameplay", "City", "Player", "Building", "Audio", "Camera", "Events" };
        }

        private void OnCategoryChanged(ChangeEvent<string> evt)
        {
            FilterConfigs();
        }

        private void OnSearchChanged(ChangeEvent<string> evt)
        {
            FilterConfigs();
        }

        private void FilterConfigs()
        {
            var category = categoryDropdown.value;
            var searchTerm = searchField.value.ToLower();
            
            filteredConfigs = allConfigs.Where(config => {
                var matchesCategory = category == "All" || config.GetType().Name.ToLower().Contains(category.ToLower());
                var matchesSearch = string.IsNullOrEmpty(searchTerm) || 
                                  config.name.ToLower().Contains(searchTerm) || 
                                  config.GetType().Name.ToLower().Contains(searchTerm);
                return matchesCategory && matchesSearch;
            }).ToList();
            
            configListView.itemsSource = filteredConfigs;
            configListView.Rebuild();
        }

        private void RefreshConfigs()
        {
            if (m_configManager == null)
            {
                Debug.LogError("ConfigManager instance not set in ConfigEditorWindow. Please inject via SetConfigManager().");
                allConfigs = new List<BaseConfig>();
            }
            else
            {
                allConfigs = m_configManager.GetAllConfigs();
            }
            FilterConfigs();
        }

        private void ShowCreateMenu()
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("City Config"), false, () => CreateConfig<CityConfig>());
            menu.AddItem(new GUIContent("Player Config"), false, () => CreateConfig<PlayerConfig>());
            menu.AddItem(new GUIContent("Devotion Config"), false, () => CreateConfig<DevotionConfig>());
            menu.AddItem(new GUIContent("Building Config"), false, () => CreateConfig<BuildingConfig>());
            menu.AddItem(new GUIContent("Gameplay Config"), false, () => CreateConfig<GameplayConfig>());
            menu.AddItem(new GUIContent("Audio Config"), false, () => CreateConfig<AudioConfig>());
            menu.AddItem(new GUIContent("Camera Config"), false, () => CreateConfig<CameraConfig>());
            menu.AddItem(new GUIContent("Random Events Config"), false, () => CreateConfig<RandomEventsConfig>());
            menu.AddItem(new GUIContent("Save Load Manager Config"), false, () => CreateConfig<SaveLoadManagerConfig>());
            menu.ShowAsContext();
        }

        private void CreateConfig<T>() where T : BaseConfig
        {
            var config = CreateInstance<T>();
            var path = $"Assets/_JAM/Resources/Configs/{typeof(T).Name}.asset";
            
            var directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            
            AssetDatabase.CreateAsset(config, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Selection.activeObject = config;
            EditorGUIUtility.PingObject(config);
            RefreshConfigs();
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
            
            RefreshConfigs();
        }

        private void GenerateAllConfigs()
        {
            ConfigAssetGenerator.GenerateAllConfigAssets();
            RefreshConfigs();
        }
    }
} 