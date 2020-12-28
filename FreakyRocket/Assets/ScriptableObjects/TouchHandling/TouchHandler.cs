using UnityEngine;

namespace Assets.ScriptableObjects.TouchHandling
{
    public abstract class TouchHandler : ScriptableObject
    {
        public abstract void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject);
    }
}