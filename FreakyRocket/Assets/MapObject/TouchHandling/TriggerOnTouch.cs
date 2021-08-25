using Assets.MapObject.TriggerHandling;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public class TriggerOnTouch : TouchHandler
    {
        public List<Component> triggerHandlers;

        void Start()
        {
            if (triggerHandlers is null)
                triggerHandlers = new List<Component>();
        }
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            foreach(Component triggerHandler in triggerHandlers)
            {
                (triggerHandler as ITriggerHandler)?.Trigger(thisObject, otherObject);
            }
        }

        public override void TouchReleased(MonoBehaviour thisObject, Collider2D otherObject)
        {
            foreach (Component triggerHandler in triggerHandlers)
            {
                (triggerHandler as ITriggerReleasedHandler)?.TriggerReleased(thisObject, otherObject);
            }
        }
    }
}
