using Assets.Classes;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ScriptableObjects.Explosions
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Explosion/Instant")]
    public class InstantExplosion : Explosion, IExplosive
    {
        public GameObject explosionPrefab;
        public float secondsToDisappear;
        private GameObject explosionObject = null;

        public override IEnumerator Explode(GameObject explodingObject)
        {
            if(explosionObject == null)
                explosionObject = Instantiate(explosionPrefab, explodingObject.transform);
            foreach(ParticleSystem particleEmitter in explosionObject.GetComponentsInChildren<ParticleSystem>())
            {
                particleEmitter.Play();
            }

            if(explodingObject.TryGetComponent<PlayerBehaviour>(out var player))
                player.StopVehicle();

            while (true)
            {
                yield return new WaitForSeconds(secondsToDisappear);
                explodingObject.SetActive(false);
            }
        }
    }
}