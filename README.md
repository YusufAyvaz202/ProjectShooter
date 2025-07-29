# Unity Shooter Game Project

A 3D action game developed in Unity featuring Player movement, enemy AI, combat system, and object pooling for performance optimization.

**Note**: This is a work-in-progress project developed for learning and portfolio purposes. Some features may be incomplete or require further optimization.

## 🎮 Game Features

- **Third-person player movement** with WASD controls and mouse look
- **Combat system** with multiple weapon types (Fireball Staff...)
- **Enemy AI** with NavMesh pathfinding and attack behaviors
- **Object pooling** system for bullets and enemies
- **Event-driven architecture** for decoupled communication
- **Animation integration** for player and enemy characters

## 🏗️ Architecture Overview

### Core Systems

- **Abstract Base Classes**: Modular design with `BaseEnemy`, `BaseGun`, and `BaseAmmunition`
- **Interface-based Design**: `IAttacker`, `IAttackable`, `IPoolable` for flexible interactions
- **Event Management**: Centralized event system for input handling and game state
- **Object Pooling**: Performance-optimized spawning system for projectiles and enemies
- **ScriptableObject Data**: Data-driven configuration for weapons and enemies

## 🎯 Gameplay

### Player Controls
- **Movement**: WASD keys
- **Camera**: Mouse look
- **Jump**: Spacebar
- **Attack**: Left mouse click
- **Throw Gun**: F key
- **Take Gun**: Go on the gun collider 

### Enemies
- **Skeleton Mage**: Ranged attacker using fireball projectiles
- **Skeleton Warrior**: Melee attacker

### Combat System
- Health-based damage system
- Multiple ammunition types (Bullets, Fireballs)
- Attack cooldowns and range-based engagement

## 📋 Requirements

- **Unity Version**: 6000.0.34f1 or higher
- **Dependencies**:
  - Unity Input System
  - NavMesh Component

## 🚀 Setup Instructions

1. **Clone the repository**
   ```bash
   git clone [https://github.com/YusufAyvaz202/ProjectShooter]
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the project folder
   - Open with Unity 6000.0.34f1 LTS or higher

3. **Scene Setup**
   - Ensure NavMesh is baked for enemy pathfinding
   - Configure player spawn point
   - Set up enemy spawn positions

## 🎨 Used Assets

- [Kaykit Skeletons](https://kaylousberg.itch.io/kaykit-skeletons)
- [Kaykit Hallowen Kits](https://kaylousberg.itch.io/halloween-bits)
- [Kaykit Adventurers](https://kaylousberg.itch.io/kaykit-adventurers)
- [Avionx](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

## ⚙️ Configuration

### ScriptableObjects
Create the following ScriptableObject assets:
- `EnemyDataSO`: Enemy stats (health, speed, attack range)
- `GunDataSO`: Weapon configuration (ammunition prefab, pool size)
- `AmmunitionDataSO`: Projectile properties (speed, damage, lifetime)

## 🐛 Known Issues

- Character shakes when moving left or right.

## 📊 Performance Notes

The project implements object pooling for frequently spawned objects (bullets, enemies) to minimize garbage collection. The event-driven architecture ensures loose coupling between systems for better maintainability.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---
