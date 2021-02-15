using Assets.MapObject.Meteor;
using Assets.MapObject.TouchHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MapObject.TriggerHandling
{
    public class TriggerZone : MonoBehaviour
    {
        public TriggerOnTouch touchHandler;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger Activated!");
            touchHandler.TouchDetected(this, collision);
        }
    }
}
