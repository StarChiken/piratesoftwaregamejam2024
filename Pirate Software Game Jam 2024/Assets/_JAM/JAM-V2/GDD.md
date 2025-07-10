# Game Design Document (GDD) - JAM-V2

## Core Elements

### Player
- Represents a deity.
- Can cast miracles on the city and its citizens.
- Accumulates Devotion Points (main win condition).
- Can gain followers (citizens who are converted).

### City
- Composed of multiple Districts.
- Each District contains a population of Citizens.

### Citizens
- Populate the city.
- Each has a name, happiness, faith attraction, and a trait.
- Some citizens like certain types of miracles (trait-miracle affinity).
- Can become followers if their faith attraction is high enough.

### Miracles
- Player can cast miracles (of various types) on citizens or districts.
- Miracles affect citizens' faith attraction and happiness.
- Some miracles are more effective on citizens with matching traits.

### Devotion
- Player's main resource/score.
- Increased by converting citizens to followers and other events.
- Certain milestones in devotion unlock new abilities or phases.

### Game Flow
- Player input triggers actions (miracles, events, etc.).
- GameOrchestrator and Gameplay scripts handle main flow and UI.
- State machine: Start -> Player Turn -> Calculate City -> Get Commandment -> Repeat.

## Win Condition
- Reach a target amount of Devotion Points by converting citizens and managing the city.

## Main Loop
1. Player casts miracles or takes actions.
2. Citizens react, possibly becoming followers.
3. Devotion is recalculated.
4. Game state advances (phases, events, etc.). 

---

## Technical Note
- All ScriptableObject configs must be placed in `Assets/Resources/ScriptableObjects/` for automatic loading by DataBootstrapper. This is the canonical pattern for all config/data in JAM-V2. 

## Devotion System
- Devotion is the core resource for the player, used to cast miracles and progress.
- Initial and maximum devotion values are now data-driven, set in the `DevotionConfig` ScriptableObject (editable in Inspector).
- This enables rapid tuning and balancing of devotion-related gameplay. 