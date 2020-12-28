using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects.TouchHandling
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Touch Handling/Nothing On Touch")]
    public class NothingOnTouch : TouchHandler
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            return;
        }
    }
}