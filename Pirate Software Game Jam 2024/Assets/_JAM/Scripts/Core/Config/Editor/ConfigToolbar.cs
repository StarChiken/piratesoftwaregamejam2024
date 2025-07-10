using System.Collections.Generic;
using Base.Core.Config.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Toolbar extension for quick access to configs.
/// </summary>
[InitializeOnLoad]
public static class ConfigToolbar
{
    private static readonly string[] s_configTypes = {
        "CityConfig",
        "PlayerConfig", 
        "DevotionConfig",
        "BuildingConfig",
        "GameplayConfig",
        "AudioConfig",
        "CameraConfig",
        "RandomEventsConfig",
        "SaveLoadManagerConfig"
    };

    static ConfigToolbar()
    {
        EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
        // This ensures the toolbar is updated when the editor state changes
    }

    [MenuItem("Tools/Configs/City Config", false, 1)]
    public static void OpenCityConfig()
    {
        OpenConfig<CityConfig>();
    }

    [MenuItem("Tools/Configs/Player Config", false, 2)]
    public static void OpenPlayerConfig()
    {
        OpenConfig<PlayerConfig>();
    }

    [MenuItem("Tools/Configs/Devotion Config", false, 3)]
    public static void OpenDevotionConfig()
    {
        OpenConfig<DevotionConfig>();
    }

    [MenuItem("Tools/Configs/Building Config", false, 4)]
    public static void OpenBuildingConfig()
    {
        OpenConfig<BuildingConfig>();
    }

    [MenuItem("Tools/Configs/Gameplay Config", false, 5)]
    public static void OpenGameplayConfig()
    {
        OpenConfig<GameplayConfig>();
    }

    [MenuItem("Tools/Configs/Audio Config", false, 6)]
    public static void OpenAudioConfig()
    {
        OpenConfig<AudioConfig>();
    }

    [MenuItem("Tools/Configs/Camera Config", false, 7)]
    public static void OpenCameraConfig()
    {
        OpenConfig<CameraConfig>();
    }

    [MenuItem("Tools/Configs/Random Events Config", false, 8)]
    public static void OpenRandomEventsConfig()
    {
        OpenConfig<RandomEventsConfig>();
    }

    [MenuItem("Tools/Configs/Save Load Manager Config", false, 9)]
    public static void OpenSaveLoadManagerConfig()
    {
        OpenConfig<SaveLoadManagerConfig>();
    }

    [MenuItem("Tools/Configs/Config Manager Window", false, 100)]
    public static void OpenConfigManagerWindow()
    {
        ConfigEditorWindow.ShowWindow();
    }

    private static void OpenConfig<T>() where T : BaseConfig
    {
        var configs = ConfigManager.GetAllConfigs();
        var targetConfig = configs.Find(c => c is T);
        
        if (targetConfig != null)
        {
            Selection.activeObject = targetConfig;
            EditorGUIUtility.PingObject(targetConfig);
        }
        else
        {
            Debug.LogWarning($"No {typeof(T).Name} found. Creating new one...");
            CreateConfig<T>();
        }
    }

    private static void CreateConfig<T>() where T : BaseConfig
    {
        var config = ScriptableObject.CreateInstance<T>();
        var path = $"Assets/_JAM/Resources/Configs/{typeof(T).Name}.asset";
        
        // Ensure directory exists
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
    }
} 