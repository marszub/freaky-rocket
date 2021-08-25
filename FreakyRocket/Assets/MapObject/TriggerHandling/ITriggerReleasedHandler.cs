using UnityEngine;

namespace Assets.MapObject.TriggerHandling
{
    public interface ITriggerReleasedHandler
    {
        void TriggerReleased(MonoBehaviour detector, Collider2D triggeredBy);
    }
}
