using System.Collections;
using UnityEngine;

namespace Assets.PlayerControl
{
    public class Controlls : ScriptableObject
    {
        public Vector2 GetMoveDirection(Vector2 currentPosition)
        {
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentPosition;
            return direction.normalized;
        }
    }
}