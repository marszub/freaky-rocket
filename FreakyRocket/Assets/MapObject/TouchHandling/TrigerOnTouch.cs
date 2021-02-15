using Assets.MapObject.TriggerHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    class TrigerOnTouch : TriggerOnTouch
    {
        public List<ITriggerHandler> triggerHandlers;
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            foreach(ITriggerHandler triggerHandler in triggerHandlers)
            {
                triggerHandler.Trigger(thisObject, otherObject);
            }
        }
    }
}
