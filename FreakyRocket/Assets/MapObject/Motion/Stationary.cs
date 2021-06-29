using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Motion
{
    public class Stationary : MonoBehaviour, IPositionWard
    {
        private Vector2 position; 

        public Vector2 GetPosition()
        {
            return position;
        }

        void Start()
        {
            position = transform.position;
        }
    }
}