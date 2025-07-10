# JAM-V2 Dependency Injection (DI) System

## Injector
- The `Injector` MonoBehaviour is the core of the DI system.
- It scans all MonoBehaviours in the scene for `[Provide]` and `[Inject]` attributes.
- It wires up dependencies at runtime, enabling clean, decoupled architecture.

## Usage
1. Add a GameObject named `Injector` to your scene and attach the `Injector` script.
2. Add `DataBootstrapper` and `GameBootstrapper` GameObjects as described in the Core/Config README.
3. All ScriptableObject configs must be placed in `Assets/Resources/ScriptableObjects/` for automatic loading by DataBootstrapper.
4. Use `[Provide]` on any field/property you want to make available for injection.
5. Use `[Inject]` on any field/property you want the DI system to supply.

## Best Practices
- Never use `new` for dependencies that should be injected.
- Use DI for all configs, managers, and services to maximize testability and maintainability. 