using Assets.ScriptableObjects.TouchHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MeteorBehaviour : MonoBehaviour
    {
        public TouchHandler touchHandler;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collision Detected!");
            touchHandler.TouchDetected(this, collision);
        }
    }
}
