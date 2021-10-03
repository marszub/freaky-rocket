using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MapObject.TriggerHandling
{
    public class Deactivate : MonoBehaviour, ITriggerHandler
    {
        public void Trigger(MonoBehaviour detector, Collider2D triggeredBy)
        {
            if(triggeredBy.gameObject.tag == "Player")
                gameObject.SetActive(false);
        }

        private IEnumerator DeactivateGameObject()
        {
            yield return null;
            gameObject.SetActive(false);
        }
    }
}