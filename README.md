# ANGRY BIRDS ELEMENTAL CHAOS

- **Angry Birds Elemental Chaos** was developed as part of the course **"Game Development and Design"** at Sofia University, FMI. The project is a physics-based puzzle game inspired by the mechanics of `Angry Birds`. The player launches birds from a cannon in order to eliminate enemy boars and destroy structures built from various materials. The game relies on a physics simulation in which objects interact through forces, gravity, and collisions. Players must carefully aim their shots and utilize the unique abilities of different birds to eliminate all boars in each level. The game currently contains:
  - `16` main playable levels;
  - `12` tutorial levels;
- A structured menu system including:
  - `Main Menu`;
  - `Level Select`;
  - `Credits`;
- The tutorial levels gradually introduce the game's mechanics, birds, enemy types, environmental hazards and basic gameplay strategies.

### Slingshot System
- The central gameplay mechanic revolves around launching birds using a cannon (functionally equivalent to the slingshot in the original Angry Birds);
- The player clicks and drags a bird away from the cannon to determine the launch angle and power. Upon release, the bird is launched toward the enemy structures following a physics-based ballistic trajectory;
- During flight, birds interact with the environment through collisions and physical forces. The objective of each level is to eliminate all boars using a limited number of birds.

### Bird Types:
- Bird abilities are implemented as triggerable actions during flight. Once launched, the player can activate a bird’s special ability, which modifies the bird’s behavior or creates additional objects;
- The game features six playable bird types, each with a unique ability that can be activated during flight:
  - `Chickie the Chick`:
    - The basic bird with no special ability;
    - Travels in a predictable ballistic trajectory;
    - Relies solely on impact force to destroy blocks and boars;
    - Serves as an introduction to the game's basic mechanics.
  - `Doug the Duck`:
    - Has the ability to split into five birds mid-flight;
    - The split birds spread slightly apart, allowing them to cover a wider horizontal area;
    - Effective for damaging multiple targets or large structures.
  - `Punky the Peacock`:
    - Can activate a forward speed boost during flight;
    - The acceleration increases kinetic energy and impact damage;
    - Particularly effective for long horizontal distances and in levels with wind, as the speed helps counteract wind resistance.
  - `Chicka the Chicken`:
    - Can drop an egg-bomb projectile while in flight;
    - The egg explodes upon impact with the ground or objects;
    - After dropping the egg, the bird is slightly propelled upward;
    - Useful for attacking structures from above or targeting confined spaces.
  - `Crowley the Crow`:
    - Has the ability to spit Blue projectiles with randomized effects:
      - 50% chance – Blue Straw;
      - 45% chance – Blue Rock;
      - 5% chance – Blue Bomb;
    - The straw and rock deal direct damage to boars and blocks;
    - The bomb explodes on impact but has a smaller blast radius than Chicka’s egg-bomb.
  - `Peyo the Pigeon`:
    - Can expand in size during flight, increasing its collision bounds;
    - This ability makes it effective in tight spaces and when pushing structures;
    - However, it becomes significantly more affected by wind, reducing its effectiveness in windy levels.

### Enemy Types:
- The game features four types of enemy boars, each with increasing durability:
  - `Normal Boar`:
    - The standard enemy type;
    - Appears in early levels and has the lowest health;
  - `Commando Boar`:
    - Possesses a larger health pool than the Normal Boar;
    - Uses an alternative sprite resembling a firefighter.
  - `Foreman Boar`:
    - Has an even larger health pool;
    - Also features an alternative sprite - the Wizard;
    - Functions as a mini-boss enemy.
  - `King Boar`:
    - The main boss enemy, appearing in Levels `8` and `16`;
    - Possesses a very large health pool and is difficult to defeat.
 
### Block Types
- Enemy structures are built using `3` primary block materials, `2` special blocks and `2` environmental blocks:
  - `Glass Block`: Very fragile and easy to break;
  - `Wooden Block`: Moderately durable. Stronger than Glass but weaker than Stone;
  - `Stone Block`: Highly durable and difficult to destroy;
  - `TNT Block`: Explodes upon impact. Damages nearby blocks and boars;
  - `Bird Cage Block`: When destroyed, releases one Chickie the Chick, which is added to the end of the player's bird roster for the current level;
  - `Hail Blocks` and `Raindrop Block`.
 
### Environmental Hazards:
- Environmental hazards are a defining feature of the game and represent elemental or natural disasters. Each hazard can either hinder or assist the player depending on how it is used:
  - `Wind`:
    - Applies a constant force opposite to the birds' flight direction;
    - Makes accurate shots more difficult;
    - Can also destabilize structures, potentially assisting the player.
  - `Earthquake`:
    - Occurs periodically;
    - Causes structures to shake violently, potentially damaging them;
    - The camera shifts toward the structures during the event, temporarily disorienting the player.
  - `Hailstorm`:
    - Occurs at intervals;
    - Hail blocks fall from the sky, damaging structures and boars;
    - Can also obstruct the birds’ trajectory.
  - `Meteor Shower`:
    - A visual variant of the Hailstorm mechanic;
    - Functionally identical, but with meteor visuals.
    - Lightning strikes occur randomly across the map, dealing severe damage to nearby objects;
    - Lightning strikes are accompanied by a bright white flash, which can momentarily blind the player.
  - `Tutorial Levels`:
    - The game begins with `12` tutorial levels designed to introduce the core mechanics step by step. Each tutorial level focuses on a specific gameplay element, allowing the player to learn through practical interaction rather than explicit instruction.
    - The tutorials gradually introduce:
      - Basic cannon launching mechanics;
      - Different bird abilities;
      - Enemy boar types;
      - Environmental hazards;
  - `Main Levels`:
    - The game contains `16` primary levels that challenge the player to apply previously learned mechanics;
      - `Environmental interaction`: Environmental hazards are incorporated to add unpredictability and dynamic gameplay situations;
      - `Resource management`: Players must eliminate all boars using a limited number of birds;
      - Boss encounters occur in Level `8` and Level `16`, where the King Boar appears. These levels feature more durable structures and require careful planning and efficient use of abilities.
  - `Level Management`:
    - Enemy structures are built using combinations of glass, wooden, stone and the two special blocks;
    - Designing structures with different materials allows the game to emphasize physics-based destruction, where structural weak points can be exploited to collapse entire buildings;
    - Certain levels intentionally include vulnerable structural supports, encouraging players to aim strategically rather than relying solely on brute force.
  - `Environmental Integration`:
    - These elements add variability and require players to adapt their strategies to changing conditions;
    - Some levels feature different combinations of the previously stated environmental hazards. For example, wind + thunderstorm.
  - `Camera System`:
    - The camera dynamically follows the active bird during flight. Certain environmental events, such as earthquakes, temporarily redirect the camera toward key areas of the level to emphasize the event.

### Art, Audio and Music:
- Visual Assets:
  - Most game sprites were generated using Sora;
  - Due to generation limits, many sprites were manually edited in Photoshop:
    - Converting idle boar sprites into hurt animations;
    - Transforming idle bird sprites into launched or ability-activated versions;
  - The font used in the game is a free version inspired by the original Angry Birds font;
  - The Chickie the Chick sprite was sourced from publicly available online assets.
- Sound Design:
  - All in-game sounds were custom recorded;
  - Voice actors:
    - Ivailo Palushev — Boars and Peyo the Pigeon;
    - Dimitar Gagashev — Punky the Peacock and King Boar;
    - Petar Balulov — Crowley the Crow;
    - Remaining sound effects were recorded and produced by the developer.
  - The only non-original sounds are the environmental sounds and the ambient sounds, such as the bird chirping present in each level.
- Music:
  - The game features a custom soundtrack composed by Martin Milchov, created specifically for this project.

### Menu Interaction
- The Main Menu contains interactive character elements:
  - Clicking on a bird changes its sprite to the launched version and plays the corresponding sound;
  - Clicking on a boar switches it to its alternative costume and triggers its voice line;
- This adds a small interactive element to the menu while showcasing the characters.

### Literature:
- [The Slides From This GitHub Repo](https://github.com/Ivan-Vankov/GameDevCourse/tree/gh-pages);
- [Game Development with Unity at FMI](https://www.youtube.com/channel/UCsBZtgJpHY6mISHcyCXRnOA);
- [How to make an Angry Birds replica in Unity (Livestream Tutorial)](https://www.youtube.com/watch?v=QM8M0RainRI&t=54s).
      - A level is considered complete when all boars are eliminated. If the player runs out of birds before completing the objective, the level is failed and can be restarted;
      - Whether the player completes the level or not, one can go to the next level. This decision was made as to remove any negative emotions towards the game as a whole.
  - `Structural Design`:
    - The design philosophy behind these levels focuses on:
      - `Increasing structural complexity`: Later levels feature larger and more intricate structures composed of multiple block materials;
      - `Strategic bird usage`: Certain levels require the player to use specific bird abilities effectively;

### Level Design
- The level design of **Angry Birds Elemental Chaos** is structured to gradually introduce gameplay mechanics and increase the difficulty as the player progresses.

