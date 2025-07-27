# TacticsGame

**Tactics RPG** is a 3D turn-based strategy game featuring magical skills and strategic combat.

Inspired by _Final Fantasy Tactics Advance_, this demo showcases core mechanics that define the tactics genre.

As a longtime fan of tactical RPGs, I wanted to implement the foundational systems that make these games so satisfying.

Try it: [weizh2.itch.io/tactics-rpg](https://weizh2.itch.io/tactics-rpg)


## Features

### Character Movement
- Uses **A\*** pathfinding to calculate optimal routes between tiles.
- Characters rotate to face the next tile for smooth directional movement.

### Enemy AI
- Decision-making based on a **weighted scoring system**:
  - **Movement**:
    - Prefers tiles near healing items when low on health.
    - Avoids enemies when weak; seeks proximity when strong.
    - Stays near allies for combo attacks.
  - **Skills**:
    - Scores based on damage, number of targets, kill potential, and healing value (with over-heal penalty).

### Skills
- Uses **A\*** for skill range detection.
- Skills trigger particle effects and character animations.
- Configured via **ScriptableObjects** for flexibility.

### Skill Patterns
- Defined using **text files** for easy customization.
- Supports various shapes (e.g., lines, AoEs) without changing code.

 
## Screenshots

![image1](Images/img1.png)
![image2](Images/img2.png)
![image3](Images/img3.png)
![image4](Images/img4.png)
