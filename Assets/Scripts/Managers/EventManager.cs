using System;
using UnityEngine;
namespace Managers
{
    public static class EventManager
    {
        // Event for player's move input.
        public static Action<Vector2> OnMovePerformed;
    }
}
