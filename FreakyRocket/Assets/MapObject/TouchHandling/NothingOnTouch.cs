using System.Collections;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Touch Handling/Nothing On Touch")]
    public class NothingOnTouch : TriggerOnTouch
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            return;
        }
    }
}