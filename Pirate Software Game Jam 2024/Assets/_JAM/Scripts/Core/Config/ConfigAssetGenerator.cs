using UnityEngine;
using UnityEditor;

namespace Base.Core.Config
{
    /// <summary>
    /// Utility script to generate all configuration ScriptableObject assets.
    /// </summary>
    public static class ConfigAssetGenerator
    {
        [MenuItem("Tools/Generate Config Assets")]
        public static void GenerateAllConfigAssets()
        {
            GenerateConfigAsset<CityConfig>("CityConfig");
            GenerateConfigAsset<PlayerConfig>("PlayerConfig");
            GenerateConfigAsset<DevotionConfig>("DevotionConfig");
            GenerateConfigAsset<SaveLoadManagerConfig>("SaveLoadManagerConfig");
            GenerateConfigAsset<RandomEventsConfig>("RandomEventsConfig");
            GenerateConfigAsset<GameplayConfig>("GameplayConfig");
            GenerateConfigAsset<BuildingConfig>("BuildingConfig");
            GenerateConfigAsset<AudioConfig>("AudioConfig");
            GenerateConfigAsset<CameraConfig>("CameraConfig");
            
            AssetDatabase.Refresh();
            Debug.Log("All config assets generated successfully!");
        }

        private static void GenerateConfigAsset<T>(string assetName) where T : ScriptableObject
        {
            string path = $"Assets/_JAM/Resources/Configs/{assetName}.asset";
            
            // Check if asset already exists
            T existingAsset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (existingAsset != null)
            {
                Debug.Log($"Config asset {assetName} already exists at {path}");
                return;
            }

            // Create new asset
            T asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            Debug.Log($"Created config asset: {path}");
        }
    }
} 