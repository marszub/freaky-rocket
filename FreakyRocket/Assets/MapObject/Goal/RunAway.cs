using Assets.MapObject.TriggerHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MapObject.Goal
{
    public class RunAway : MonoBehaviour, ITriggerHandler
    {
        [SerializeField] private float runDistance;
        [SerializeField] private float runTime;

        private Vector2 runDirection;
        private float endTime;

        private void Start()
        {
            endTime = 0;
        }

        private void Update()
        {
            if (endTime > Time.time)
                transform.position += (Vector3)(runDirection * Time.deltaTime * runDistance / runTime);
        }

        public void Trigger(MonoBehaviour detector, Collider2D triggeredBy)
        {
            if(triggeredBy.gameObject.tag == "Player")
            {
                runDirection = (transform.position - triggeredBy.transform.position).normalized;
                endTime = Time.time + runTime;
            }
        }
    }
}
