# STRUCTURE MAPPING (excerpt)

## Organized Folder Structure

### **Gameplay/Systems/** (Core gameplay logic)
- **Devotion.cs**: Pure C# devotion system, tracks current/max devotion, exposes gain/spend/events.
- **Player.cs**: Player/deity logic, manages followers and miracle casting.
- **City.cs**: City management, generates districts and citizens.
- **District.cs**: District logic, holds citizens with read-only access.
- **Citizen.cs**: Citizen behavior and state management.
- **GameManager.cs**: Game orchestration, coordinates all core systems.
- **GameBootstrapper.cs**: Unity entry point, handles DI wiring.

### **Gameplay/Managers/** (System managers and factories)
- **ICitizenFactory.cs**: Abstract factory interface for citizen creation.
- **CitizenFactory.cs**: Concrete citizen factory implementation.

### **Core/Enums/** (Type definitions)
- **MiracleType.cs**: Miracle type enumeration.
- **TraitType.cs**: Trait type enumeration.

### **Core/Utilities/** (Shared utilities)
- **TraitMiracleMatcher.cs**: Utility for matching traits to miracles.

### **Core/Config/** (Canonical config system)
- **DataBootstrapper.cs**: Provides all ScriptableObject configs to DI system.
- **PlayerConfig.cs**: ScriptableObject config for player settings (FaithAttractionThreshold, StartingFollowers).
- **CityConfig.cs**: ScriptableObject config for city generation (NumberOfDistricts, CitizensPerDistrict).
- **DevotionConfig.cs**: ScriptableObject config for devotion values (InitialDevotion, MaxDevotion).

## Canonical Config System
- **Location**: `Assets/_JAM/JAM-V2/Core/Config/`
- **Pattern**: Simple ScriptableObject configs with [Tooltip] documentation and proper C# naming conventions
- **Loading**: DataBootstrapper loads from `Resources/ScriptableObjects/` if not assigned in Inspector
- **Namespace**: `PirateGame.JAMV2`
- **Creation**: Via Unity menu `JAM-V2/Config/ConfigName`
- **Naming**: Public properties use PascalCase (e.g., `FaithAttractionThreshold`), private fields use camelCase with m_ prefix

## Devotion System
- **Devotion.cs**: Pure C# class, tracks current/max devotion, exposes gain/spend/events.
- **DevotionConfig.cs**: ScriptableObject config, located in `Assets/_JAM/JAM-V2/Core/Config/`.
  - Properties: `InitialDevotion`, `MaxDevotion` (Inspector-editable, with tooltips).
  - Created via Unity menu: `JAM-V2/Config/DevotionConfig`.
- **GameBootstrapper**: Injects `DevotionConfig` (via Inspector or DataBootstrapper), constructs `Devotion` using config values, and provides it via `[Provide]` for DI.

## Dependency Injection & Config Pattern
- All core systems receive configs (e.g., DevotionConfig) via DI.
- ScriptableObject configs are placed in `Assets/Resources/ScriptableObjects/` for auto-loading.
- Inspector assignment overrides auto-loading if set.
- No hardcoded values; all tunable data is data-driven via configs.
- Config properties follow C# naming conventions (PascalCase for public APIs).

## File Organization Principles
- **Systems/**: Core gameplay logic and state management
- **Managers/**: Factory patterns and system managers
- **Core/Enums/**: Type definitions and enumerations
- **Core/Utilities/**: Shared utility classes and helpers
- **Core/Config/**: Canonical configuration system
- **UI/**: User interface components (MonoBehaviours only)
- **Editor/**: Custom editor scripts and tools
- **Tests/**: Unit tests and test utilities

This pattern is now canonical for JAM-V2 and documented in all relevant README files. 