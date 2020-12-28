using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.ScriptableObjects.TouchHandling
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Touch Handling/Die On Touch")]
    public class DieOnTouch : TouchHandler
    {
        public override void TouchDetected(MonoBehaviour thisObject, Collider2D otherObject)
        {
            //Debug.Log()
            if(otherObject.transform.parent.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
            {
                Debug.Log("Die");
                player.Die();
            }
        }
    }
}