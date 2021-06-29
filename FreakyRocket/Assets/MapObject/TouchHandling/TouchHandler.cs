using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public abstract class TouchHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger Activated!");
            TouchDetected(this, collision);
        }

        public abstract void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject);
    }
}