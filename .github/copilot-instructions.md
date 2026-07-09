# Copilot instructions for ProyectoEscupitajo

## Project baseline

- Unity version: **2022.3.62f2** (`ProjectSettings/ProjectVersion.txt`).
- This is a Unity game project (`Assets/`, `Packages/`, `ProjectSettings/`), not a .NET solution with standalone build scripts.

## Build, test, and lint commands

Use Unity CLI from the project root.

### Run automated tests

- EditMode tests:
  - `Unity.exe -batchmode -projectPath . -runTests -testPlatform EditMode -testResults TestResults\editmode-results.xml -quit`
- PlayMode tests:
  - `Unity.exe -batchmode -projectPath . -runTests -testPlatform PlayMode -testResults TestResults\playmode-results.xml -quit`
- Single test (EditMode or PlayMode): add `-testFilter "<FullyQualifiedTestName>"`, for example:
  - `Unity.exe -batchmode -projectPath . -runTests -testPlatform EditMode -testFilter "Namespace.ClassName.TestMethod" -testResults TestResults\single-test.xml -quit`

### Build

- No scripted CLI build entry point is committed in this repository (no dedicated build method/class in the inspected files). If you add one, keep it in source control and call it via Unity `-executeMethod`.

### Lint

- No dedicated linter command is configured in this repository.

## High-level architecture

- **Gameplay core is state-machine driven**:
  - `Core` + `StateMachine` + `State` (`Assets/Scripts/StateMachine/`).
  - Player behavior is composed from serialized child state components (`IdleState`, `MoveState`, `WallState`, `WallChargeState`, `JumpState`) under `Assets/Scripts/StateMachine/States/PlayerStates/`.
  - `PlayerController` (`Assets/Scripts/Player/PlayerController.cs`) chooses active states based on Rewired inputs and ground/wall flags.

- **Trick/combo system is a central gameplay loop**:
  - `TrickManager` (`Assets/Scripts/Player/Tricks/TrickManager.cs`) manages available tricks, combo timing windows, perfect/great timing, wall trick variants, and emits events for scoring/feedback.
  - `Trick` assets (`Assets/Scripts/Player/Tricks/Trick.cs`) carry combo graph and scoring metadata.
  - `ScoreManager` consumes trick events and updates score/multiplier.

- **Event-driven communication uses ScriptableObject events**:
  - `GameEvent` + `GameEventListener` in `Assets/Scripts/EventSystem/`.
  - Systems publish/subscribe through `Raise(Component sender, object data)` to decouple gameplay, UI, audio, and managers.

- **Progression/persistence is centralized and scene-agnostic**:
  - `DataPersistenceManager` (`Assets/Scripts/DataPersistance/DataPersistenceManager.cs`) is a `DontDestroyOnLoad` singleton.
  - It discovers all scene objects implementing `IDataPersistence`, then loads/saves `GameData` via `FileDataHandler` (JSON in `Application.persistentDataPath`).
  - Typical persistent systems: `GoldManager`, `UnlockablesManager`, `TutorialManager`, `DeathCountText`.

- **Level flow and spawning**:
  - `ChunkManager` + poolers in `Assets/Scripts/Level/` drive endless/progressive chunk spawning, level transitions, and demo end behavior.

- **Input/audio stack**:
  - Input is Rewired-based (`ReInput.players.GetPlayer`, action names like `BodyTrick`, `SkateTrick`, `KeepTrick`, `Confirm`).
  - Audio is FMOD-based (`AudioManager`, buses/snapshots/event references).

- **Scenes in build settings** (`ProjectSettings/EditorBuildSettings.asset`):
  - Enabled: `Assets/Scenes/0_NewMenu.unity`, `Assets/Scenes/2_UIGameplayTest.unity`
  - Disabled but present: `Assets/Scenes/1_Gym.unity`, `Assets/Scenes/5_MainMenu.unity`

## Key conventions in this codebase

- **Prefer wiring in Inspector over runtime discovery for gameplay references**:
  - Most gameplay/state scripts rely on `[SerializeField]` references to clips, events, sprites, and managers.
  - When adding behavior, follow serialized-field wiring patterns already used across Player, State, Tutorial, and UI scripts.

- **Keep event payload contracts explicit and type-safe at call sites**:
  - `GameEvent` payloads are `object`, but handlers assume specific payload types (`bool`, `int`, `Trick`, etc.).
  - Match existing sender/data contracts exactly when adding new event producers/consumers.

- **Persistence contract is interface-based and auto-discovered**:
  - To persist new data, implement `IDataPersistence` and map fields into `GameData`; `DataPersistenceManager` will pick it up automatically on scene load.
  - Persisted save filename is `gameData.json`.

- **Singleton managers follow one shared pattern**:
  - `Instance` static + `DontDestroyOnLoad` + duplicate self-destroy in `Awake`.
  - Reuse this pattern for global managers to stay consistent.

- **Use existing naming and folder conventions, including established spellings**:
  - Persistence folder is intentionally named `DataPersistance` in this repo.
  - Keep current folder/namespace style unless doing an intentional, repo-wide refactor.

- **Unity text serialization is enabled** (`ProjectSettings/EditorSettings.asset` has `m_SerializationMode: 2`):
  - Avoid workflows that switch assets/scenes to binary serialization.
