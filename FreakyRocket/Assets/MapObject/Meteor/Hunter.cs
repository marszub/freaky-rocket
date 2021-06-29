using Assets.MapObject.TriggerHandling;
using Assets.MapObject.Vehicle;
using UnityEngine;

namespace Assets.MapObject.Meteor
{
    public class Hunter : MonoBehaviour, ITriggerHandler
    {
        public HuntProperties properties;
        private int currentState;
        private float stateTime;
        private Vector2 previousPosition;
        private Vector2 huntPosition;
        private GameObject victimPlayer;

        void Start()
        {
            currentState = -1;
            stateTime = 0;
        }

        void Update()
        {
            stateTime += Time.deltaTime;
        }

        public void Trigger(MonoBehaviour detector, Collider2D triggeredBy)
        {
            if (currentState != -1)
                return;

            victimPlayer = triggeredBy.gameObject;

            if(victimPlayer.tag == "Player")
            {
                huntPosition = victimPlayer.transform.position;
                currentState = 0;
                stateTime = 0;
            }
        }

        public Vector2 GetPosition(Vector2 estimatedPosition)
        {
            if (currentState == -1)
                return estimatedPosition;

            float stateProgress = stateTime / properties.HuntPhaseTimes[currentState];
            while(stateProgress >=1)
            {
                
                previousPosition = huntPosition;
                huntPosition = victimPlayer.transform.position;
                stateTime -= properties.HuntPhaseTimes[currentState];

                currentState = (currentState + 2) % (properties.HuntPhaseTimes.Count + 1) - 1;

                if (currentState == -1)
                    return estimatedPosition;

                stateProgress = stateTime / properties.HuntPhaseTimes[currentState];
            }

            if(currentState == 0)
                return stateProgress * huntPosition + (1 - stateProgress) * estimatedPosition;

            if(currentState == properties.HuntPhaseTimes.Count-1)
                return stateProgress * estimatedPosition + (1 - stateProgress) * previousPosition;

            return stateProgress * huntPosition + (1 - stateProgress) * previousPosition;
        }
    }
}