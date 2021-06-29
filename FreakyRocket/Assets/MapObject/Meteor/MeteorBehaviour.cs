using Assets.MapObject.Motion;
using Assets.MapObject.TouchHandling;
using UnityEngine;

namespace Assets.MapObject.Meteor
{
    public class MeteorBehaviour : MonoBehaviour
    {
        private Hunter hunter;
        private IPositionWard positionWard;

        private void Start()
        {
            if (gameObject.GetComponent<IPositionWard>() == null)
            {
                gameObject.AddComponent<Stationary>();
            }
            positionWard = gameObject.GetComponent<IPositionWard>();
            
            hunter = gameObject.GetComponent<Hunter>();
        }

        private void Update()
        {
            if (hunter == null)
            {
                transform.position = positionWard.GetPosition();
            }
            else
            {
                transform.position = hunter.GetPosition(positionWard.GetPosition());
            }
        }
    }
}
