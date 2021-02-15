using Assets.MapObject.TouchHandling;
using UnityEngine;

namespace Assets.MapObject.Meteor
{
    public class MeteorBehaviour : MonoBehaviour
    {
        public TriggerOnTouch touchHandler;

        private Hunter hunter;
        private IPositionWard positionWard;

        private void Start()
        {
            if (gameObject.GetComponent<IPositionWard>() == null)
            {
                gameObject.AddComponent<Stationary>();
            }
            positionWard = gameObject.GetComponent<IPositionWard>();

            if (gameObject.GetComponent<Hunter>() == null)
            {
                gameObject.AddComponent<Hunter>();
            }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collision Detected!");
            touchHandler.TouchDetected(this, collision);
        }
    }
}
