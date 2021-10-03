using Assets.GameManagement;
using UnityEngine;

namespace Assets.MapObject.TouchHandling
{
    public class WinOnTouch : TouchHandler
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            if(otherObject.gameObject.tag == "Player")
                GameplayController.instance.EndScreen(true);
        }
    }
}