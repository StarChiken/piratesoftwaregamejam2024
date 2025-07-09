# Config Editor Tools (UI Toolkit)

This directory contains designer-friendly tools for working with the game's ScriptableObject configuration system, built with Unity's modern UI Toolkit for better performance and maintainability.

## 🎯 Overview

The config system provides a centralized way to manage all game settings through ScriptableObjects. These editor tools make it easy for designers to create, edit, validate, and manage configs without touching code.

## 🛠️ Available Tools

### 1. Config Editor Window
**Access:** `Tools > Game Configs`

A comprehensive window that provides:
- **Centralized Management**: View all configs in one place
- **Search & Filter**: Find configs by name or category
- **Quick Actions**: Edit, duplicate, and create new configs
- **Category Organization**: Filter by config type (Gameplay, City, Player, etc.)

### 2. Quick Access Toolbar
**Access:** `Tools > Configs > [Config Type]`

Provides instant access to specific config types:
- City Config
- Player Config  
- Devotion Config
- Building Config
- Gameplay Config
- Audio Config
- Camera Config
- Random Events Config
- Save Load Manager Config

### 3. Config Validator
**Access:** `Tools > Configs > Validate All Configs`

Automatically checks all configs for common issues:
- **Missing References**: Object references that are null
- **Empty Values**: String fields that are empty
- **Negative Values**: Numbers that should be positive
- **Empty Arrays**: Lists that have no items

### 4. Custom Config Inspector
**Access:** Select any config asset in the Project window

Modern UI Toolkit-based inspector with:
- **Validation Section**: Shows issues with the current config
- **Debug Info**: Runtime loading status and asset details
- **Quick Actions**: Reset to defaults, duplicate, find references
- **Better Organization**: Cleaner layout with helpful information

## 📋 How to Use

### Creating New Configs

1. **Via Editor Window:**
   - Open `Tools > Game Configs`
   - Click "Create New Config"
   - Select the config type from the menu

2. **Via Toolbar:**
   - Go to `Tools > Configs > [Config Type]`
   - If the config doesn't exist, it will be created automatically

3. **Via Project Window:**
   - Right-click in Project window
   - `Create > Game > Config > [Config Type]`

### Editing Configs

1. **Select the config** in the Project window
2. **Use the enhanced inspector** for better organization
3. **Check the Validation section** for any issues
4. **Use the Debug section** to see runtime status

### Validating Configs

1. **Open the validator:** `Tools > Configs > Validate All Configs`
2. **Review issues** by type (Errors, Warnings, Info)
3. **Click "Fix"** on individual issues or "Fix All" for everything
4. **Refresh** to see updated results

### Finding Configs

1. **Use the Editor Window** for browsing and searching
2. **Use the toolbar** for quick access to specific types
3. **Use the Project window** with the "t:BaseConfig" filter

## 🎨 Designer-Friendly Features

### Modern UI Toolkit Benefits
- **Better Performance**: More efficient than IMGUI
- **Cleaner Code**: Declarative UI structure
- **CSS Styling**: Easy to customize appearance
- **Responsive Design**: Adapts to window resizing
- **Search and Filter**: Real-time filtering capabilities

### Validation & Safety
- **Automatic validation** of common issues
- **One-click fixes** for simple problems
- **Clear error messages** with helpful context
- **Property path information** for easy location

### Quick Actions
- **Duplicate configs** with one click
- **Reset to defaults** when needed
- **Find references** to see what uses a config
- **Generate all configs** for fresh setup

### Accessibility
- **Keyboard shortcuts** for common actions
- **Context menus** for quick access
- **Tooltips** with helpful information
- **Consistent UI** across all tools

## 🔧 Technical Details

### Config Categories
- **Gameplay**: Core game mechanics and settings
- **City**: City generation and management
- **Player**: Player-related settings and stats
- **Building**: Building types, materials, and prefabs
- **Audio**: Sound settings and music
- **Camera**: Camera behavior and settings
- **Events**: Random events and triggers

### File Structure
```
Assets/_JAM/Scripts/Core/Config/
├── Editor/                    # Editor tools
│   ├── ConfigEditorWindow.cs  # Main editor window (UI Toolkit)
│   ├── ConfigEditorWindow.uss # Styles for editor window
│   ├── ConfigValidator.cs     # Validation tool (UI Toolkit)
│   ├── ConfigValidator.uss    # Styles for validator
│   ├── BaseConfigInspector.cs # Custom inspector (UI Toolkit)
│   ├── ConfigToolbar.cs       # Quick access toolbar
│   └── README.md             # This file
├── [Config Classes].cs        # Config ScriptableObjects
└── ConfigManager.cs          # Runtime config manager
```

### Runtime Integration
- **ConfigManager** loads all configs at runtime
- **Automatic validation** on load
- **Type-safe access** through generic methods
- **Fallback values** for missing configs

## 🚀 Best Practices

### For Designers
1. **Use the Editor Window** for daily config management
2. **Run validation regularly** to catch issues early
3. **Use descriptive names** for config assets
4. **Test changes** in play mode to verify effects
5. **Backup important configs** before major changes

### For Developers
1. **Add validation rules** to new config types
2. **Use the ConfigManager** for runtime access
3. **Document new config properties** with tooltips
4. **Test config loading** in different scenarios
5. **Keep configs organized** by category

### For Teams
1. **Version control** all config assets
2. **Review config changes** before committing
3. **Use consistent naming** conventions
4. **Document config dependencies** clearly
5. **Test config changes** in different environments

## 🐛 Troubleshooting

### Common Issues

**"Config not found" errors:**
- Check that config assets exist in Resources/Configs/
- Verify ConfigManager is initialized
- Run validation to check for missing references

**"Validation errors":**
- Use the Config Validator to identify issues
- Check for null references in object fields
- Verify numeric values are within expected ranges

**"Editor tools not showing":**
- Ensure scripts are in the Editor folder
- Check that assembly definitions include Editor references
- Restart Unity if tools don't appear

### Getting Help
1. **Check the validation tool** for specific issues
2. **Review the debug section** in config inspectors
3. **Use the search function** in the editor window
4. **Check the console** for error messages
5. **Verify file paths** and asset references

## 📈 Future Enhancements

Planned improvements:
- **Config templates** for common setups
- **Batch editing** for multiple configs
- **Config comparison** tools
- **Import/export** functionality
- **Config presets** for different game modes
- **Real-time validation** as you type
- **Config dependency** visualization
- **Performance profiling** for config loading

---

*These tools are designed to make config management as painless as possible for designers while maintaining the flexibility and power that developers need.* 