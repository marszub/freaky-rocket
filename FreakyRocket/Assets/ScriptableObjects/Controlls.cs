using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.ScriptableObjects
{
    public class Controlls : ScriptableObject
    {
        public Vector2 GetMoveDirection(Vector2 currentPosition)
        {
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - currentPosition;
            return direction.normalized;
        }
    }
}