using UnityEngine;

namespace Assets.MapObject.TriggerHandling
{
    public interface ITriggerHandler
    {
        void Trigger(MonoBehaviour detector, Collider2D triggeredBy);
    }
}
