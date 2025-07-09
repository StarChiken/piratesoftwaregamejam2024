# Configuration System

This directory contains the ScriptableObject-based configuration system for the game. All configuration data has been refactored from hardcoded values to ScriptableObjects that can be loaded from the Resources folder.

## Architecture

### Base Classes
- **`BaseConfig`**: Abstract base class for all configuration ScriptableObjects
- **`ConfigManager`**: Singleton manager that handles loading and caching of configs
- **`ConfigInitializer`**: MonoBehaviour for initializing the config system

### Configuration Classes
- **`CityConfig`**: City generation settings, district names, faction names
- **`PlayerConfig`**: Player starting values and names
- **`DevotionConfig`**: Devotion system settings and miracle/commandment types
- **`SaveLoadManagerConfig`**: Save/load system default settings
- **`RandomEventsConfig`**: Random event thresholds and messages
- **`GameplayConfig`**: Gameplay mechanics, grid settings, citizen agent settings
- **`BuildingConfig`**: Building prefabs, materials, and types
- **`AudioConfig`**: Audio clips, VFX, and sound settings
- **`CameraConfig`**: Camera movement, zoom, and positioning settings

## Usage

### Loading Configs
```csharp
// Get a config from the ConfigManager
var cityConfig = ConfigManager.Instance.GetConfig<CityConfig>();

// Or load directly from Resources
var playerConfig = BaseConfig.LoadConfig<PlayerConfig>();
```

### Initialization
Add a `ConfigInitializer` component to a GameObject in your scene, or manually initialize:

```csharp
// Initialize the config system
ConfigManager.Instance.PreloadAllConfigs();
```

### Creating New Configs
1. Create a new class inheriting from `BaseConfig`
2. Add `[CreateAssetMenu]` attribute
3. Define serialized fields for configuration data
4. Add public properties for backward compatibility
5. Override `ConfigFileName` property

Example:
```csharp
[CreateAssetMenu(fileName = "MyConfig", menuName = "Game/Config/My Config")]
public class MyConfig : BaseConfig
{
    [SerializeField] private int myValue = 10;
    
    public int MyValue => myValue;
    
    protected override string ConfigFileName => "MyConfig";
}
```

## File Structure
```
Assets/_JAM/Resources/Configs/
├── CityConfig.asset
├── PlayerConfig.asset
├── DevotionConfig.asset
├── SaveLoadManagerConfig.asset
├── RandomEventsConfig.asset
├── GameplayConfig.asset
├── BuildingConfig.asset
├── AudioConfig.asset
└── CameraConfig.asset
```

## Benefits
- **Editor-friendly**: All configs can be edited in the Unity Inspector
- **Runtime loading**: Configs are loaded from Resources folder at runtime
- **Caching**: ConfigManager caches loaded configs for performance
- **Type safety**: Strongly typed configuration access
- **Modularity**: Each system has its own config class
- **Extensibility**: Easy to add new configuration types

## Migration Notes
- All hardcoded configuration values have been moved to ScriptableObjects
- Managers now load configs from ConfigManager instead of creating new instances
- Serialized fields in MonoBehaviour components have been replaced with config properties
- Backward compatibility is maintained through public properties

## Tools
- **ConfigAssetGenerator**: Unity Editor tool to generate all config assets
- **ConfigInitializer**: MonoBehaviour for automatic initialization
- Use "Tools > Generate Config Assets" in Unity to create all config files 