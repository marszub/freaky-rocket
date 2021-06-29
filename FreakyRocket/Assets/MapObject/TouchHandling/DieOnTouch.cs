using Assets.MapObject.Vehicle;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public class DieOnTouch : TouchHandler
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            //Debug.Log(thisObject.ToString() + "____" + otherObject.ToString());
            if (otherObject.transform.parent.TryGetComponent(out PlayerBehaviour player))
            {
                Debug.Log("Die");
                player.Die();
            }
        }
    }
}