# JAM-V2 Core Config

This folder contains ScriptableObject configs and DI setup scripts for the JAM-V2 architecture.

## Dependency Injection (DI) Scene Setup

To use the DI system in your Unity scene, follow the instructions in `README_SceneSetup.md`.

- All ScriptableObject configs must be placed in `Assets/Resources/ScriptableObjects/` for automatic loading by DataBootstrapper.
- You may assign configs in the Inspector to override the automatic loading.
- Add `Injector`, `DataBootstrapper`, and `GameBootstrapper` GameObjects to your scene.
- Use `[Inject]` and `[Provide]` attributes to wire up dependencies automatically. 