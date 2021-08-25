﻿using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public abstract class TouchHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger activated");
            TouchDetected(this, collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Trigger left");
            TouchReleased(this, collision);
        }

        public abstract void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject);

        public virtual void TouchReleased(MonoBehaviour thisObject, Collider2D otherObject)
        {
            return;
        }
    }
}