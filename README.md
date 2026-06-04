# Deal with the Devil 🍹

A narrative-driven bar management game built with **Unity** where you run a mystical bar and serve customers with divine drinks.

## Overview

Deal with the Devil is a 2D management game where players take on the role of a bartender in a supernatural tavern. Manage customer queues, serve drinks through skill-based mini-games, and navigate customer interactions in a quirky bar setting.

## Features

### 🎮 Core Gameplay
- **NPC Management System**: AI-controlled customers with realistic behaviors (queueing, seating, ordering, exiting)
- **Dynamic Queuing**: Patrons queue naturally and are served in order
- **Interactive Mini-Games**: 
  - Shaking mini-game for drink preparation
  - Mixing challenges for precision gameplay
  - Time-based service mechanics
- **Dialogue System**: Interact with customers through a dialogue management system
- **Animation System**: Smooth character animations for ordering, drinking, and movement
- **NavMesh Pathfinding**: Intelligent NPC navigation throughout the bar

### 🎨 Visual Elements
- Sprite-based 2D graphics with animated characters
- Environmental rendering with sorting layers
- Smooth character transitions and animations
- Menu system with scene navigation

### 📊 Game Systems
- **Main Menu**: Scene selection and game start
- **Main Game Scene**: Core gameplay loop with multiple interactive areas
- **Save/Load Support**: Persisted game state management

## Getting Started

### Prerequisites
- **Unity 2021.3 LTS** or newer (project uses Universal Render Pipeline)
- **C# 9.0+**
- Windows/Mac/Linux

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/ProAssassinXero/Deal-with-the-Devil.git
   cd Deal-with-the-Devil
   ```

2. **Open in Unity**:
   - Open Unity Hub
   - Click "Open Project"
   - Select the cloned folder
   - Wait for the project to load and import assets

3. **Run the Game**:
   - Press `Play` in the Unity Editor or build to your target platform

### Build Instructions

1. Go to `File > Build Settings`
2. Add scenes:
   - `Scenes/Main Menu`
   - `Scenes/Main`
3. Select your target platform (PC, Mac, Linux, WebGL)
4. Click `Build` and choose an output folder

## How to Play

### Controls

| Input | Action |
|-------|--------|
| **WASD** | Move character |
| **Mouse** | UI interaction / Mini-game input |
| **Space** | Interact with objects |
| **ESC** | Pause / Menu |

### Gameplay Loop

1. **Serve Customers**: NPCs enter the bar and queue at the counter
2. **Take Orders**: Interact with customers to receive drink orders
3. **Complete Mini-Games**: 
   - **Shaker Game**: Time your shakes to match the pattern
   - **Mixing Game**: Follow the mixing sequence correctly
4. **Deliver Drinks**: Serve the completed drink to the customer
5. **Customer Satisfaction**: Successfully served customers leave tips and feedback

### Game Tips

- 🔄 Serve customers in queue order to maintain efficiency
- ⚡ Complete mini-games quickly for bonus points
- 💰 Higher satisfaction = better tips
- 🎯 Time management is key during busy hours

## Project Structure

```
Deal-with-the-Devil/
├── Assets/
│   ├── Script/
│   │   ├── AI System Re-attempt/
│   │   │   ├── Customer AI/
│   │   │   │   ├── AIMovement.cs
│   │   │   │   ├── AISeatStorage.cs
│   │   │   │   └── AI_Animation.cs
│   │   │   └── NPC_QueueManager.cs
│   │   ├── Sumfin'/
│   │   │   ├── DialogueManager.cs
│   │   │   ├── NPC_OrderScript.cs
│   │   │   └── Ordering.cs
│   │   └── Mini-Games/
│   │       ├── MiniGame_ShakingScript.cs
│   │       └── MiniGame_MixingScript.cs
│   ├── Scenes/
│   │   ├── Main Menu.unity
│   │   └── Main.unity
│   └── Settings/
│       └── [URP Configuration Files]
├── .gitignore
├── ProjectSettings/
└── README.md
```

### Key Scripts

#### AI System (`Script/AI System Re-attempt/`)
- **AIMovement.cs**: Handles NPC navigation, queuing, and pathing
- **AISeatStorage.cs**: Manages seating logic and customer placement
- **AI_Animation.cs**: Synchronizes animations with NPC state
- **NPC_QueueManager.cs**: Tracks queue order and customer turns

#### Core Gameplay (`Script/Sumfin'/`)
- **DialogueManager.cs**: Manages NPC-Player interactions and dialogue
- **NPC_OrderScript.cs**: Handles drink order creation and tracking
- **Ordering.cs**: Animation state for the ordering process

#### Mini-Games (`Script/Mini-Games/`)
- **MiniGame_ShakingScript.cs**: Shaker mini-game logic
- **MiniGame_MixingScript.cs**: Mixing mini-game logic

## Development

### Team

- **ProAssassinXero** (Faisal Ibrahim) - Lead Developer, AI & Game Systems
- **MCsamMC** (Sam) - Mini-Games, UI, Polish

### Tech Stack

- **Engine**: Unity 2022+ with Universal Render Pipeline (URP)
- **Language**: C#
- **Graphics**: 2D Sprites with NavMesh pathfinding
- **Input**: New Input System

## Known Issues

- ⚠️ Main.unity contains resolved merge conflict markers (non-breaking)
- Some SpriteRenderers disabled in earlier builds—verify all graphics render correctly
- NavMesh components may need rebaking if scene geometry changes significantly

## Future Enhancements

- 📱 Mobile touch controls
- 🔊 Audio system (background music, sound effects)
- 💾 Full save/load implementation
- 🌟 Customer reputation/loyalty system
- 🍸 Expanded drink recipes and ingredients
- 🏆 Leaderboard and score tracking
- 🎭 Additional dialogue options and story progression
- 🎨 Improved UI/UX polish

## Contributing

1. Create a feature branch: `git checkout -b feature/your-feature`
2. Make your changes with clear, descriptive commit messages
3. Push to your branch: `git push origin feature/your-feature`
4. Open a Pull Request with a description of your changes

## License

This project is part of a Final Major Project (FMP) for educational purposes.

## Support & Contact

For questions or issues:
- 📧 Email: Sheybaba101@gmail.com
- 🐙 GitHub Issues: [Open an Issue](https://github.com/ProAssassinXero/Deal-with-the-Devil/issues)

---

**Made with ❤️ by ProAssassinXero & MCsamMC**

*Last Updated: May 2026*
