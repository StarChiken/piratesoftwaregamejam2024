# JAM-V2 Unity Scene Setup for Dependency Injection

## Required GameObjects and Components

1. **Injector**
   - Add a GameObject named `Injector` to your scene.
   - Attach the `Injector` MonoBehaviour (from Core/DependencyInjection).
   - Set script execution order to -1000 (if not already).

2. **DataBootstrapper**
   - Add a GameObject named `DataBootstrapper`.
   - Attach the `DataBootstrapper` MonoBehaviour (from Core/Config).
   - All ScriptableObject configs must be placed in `Assets/Resources/ScriptableObjects/` for automatic loading.
   - You may assign configs in the Inspector to override the automatic loading.

3. **GameBootstrapper**
   - Add a GameObject named `GameBootstrapper`.
   - Attach the `GameBootstrapper` MonoBehaviour (from Gameplay/Systems).
   - No manual config assignment needed; all configs are injected by the DI system.

## Execution Order
- Ensure `Injector` executes before all other scripts (set in Project Settings > Script Execution Order).
- `DataBootstrapper` and `GameBootstrapper` can use default order, but must be present in the scene.

## How It Works
- `Injector` scans all MonoBehaviours for `[Provide]` and `[Inject]` attributes.
- `DataBootstrapper` provides all configs via `[Provide]` fields, loading from `Resources/ScriptableObjects/` if not assigned.
- `GameBootstrapper` receives configs via `[Inject]` fields and constructs the `GameManager`.
- Any other MonoBehaviour can use `[Inject]` to receive dependencies provided in the scene.

## Best Practices
- Never use `new` for dependencies that should be injected—let the DI system handle it.
- Use `[Provide]` for any service or config you want to make available to the DI system.
- Use `[Inject]` for any dependency you want the DI system to supply.
- Keep all DI setup scripts in the root of the scene (not as children of other GameObjects).
- Document any custom providers or injectors in this file for future contributors. 