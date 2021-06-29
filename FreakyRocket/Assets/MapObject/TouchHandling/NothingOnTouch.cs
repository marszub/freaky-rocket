using System.Collections;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public class NothingOnTouch : TouchHandler
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            return;
        }
    }
}