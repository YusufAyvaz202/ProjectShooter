using System;
using Abstracts;
using Bullets;
using UnityEngine;
namespace Managers
{
    public static class EventManager
    {
        // Event for player's move and rotation input.
        public static Action<Vector2> OnMovePerformed;
        public static Action<Vector2> OnLookPerformed;
        public static Action OnJumpPerformed;

        // Event for player's Attack Actions.
        public static Action OnAttackPerformed;
        public static Action<Bullet> OnDeSpawn;

        //Event for player's Animations Actions.
        public static Action<float> PlayerMoveAnimationParameterChanged;
        
        //Event for Enemy Actions.
        public static Action<BaseEnemy> OnEnemyDie;
    }
}