using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public abstract class TriggerOnTouch : ScriptableObject
    {
        public abstract void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject);
    }
}