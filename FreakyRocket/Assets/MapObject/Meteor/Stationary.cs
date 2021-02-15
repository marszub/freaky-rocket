using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Meteor
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