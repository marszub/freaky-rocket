using Assets.GameManagement;
using Assets.MapObject.Vehicle;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public class DieOnTouch : TouchHandler
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            if (otherObject.transform.parent && otherObject.transform.parent.TryGetComponent(out IKillable killable))
            {
                Debug.Log("Die");
                killable.Die();
            }
        }
    }
}