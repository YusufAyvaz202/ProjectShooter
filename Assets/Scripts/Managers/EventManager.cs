using System;
using UnityEngine;
namespace Managers
{
    public static class EventManager
    {
        // Event for player's move and rotation input.
        public static Action<Vector2> OnMovePerformed;
        public static Action<Vector2> OnLookPerformed;
        public static Action OnJumpPerformed;
    }
}
