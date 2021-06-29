using Assets.MapObject;
using Assets.MapObject.Vehicle;
using Assets.MapObject.Vehicle.Explosions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MapObject.Vehicle.Explosions
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Explosion/Instant")]
    public class InstantExplosion : Explosion, IExplosive
    {
        public GameObject explosionPrefab;
        public float secondsToDisappear;
        private GameObject explosionObject = null;

        public override IEnumerator Explode(PlayerBehaviour runner)
        {
            if (explosionObject == null)
                explosionObject = Instantiate(explosionPrefab, runner.transform);
            runner.StopVehicle();
            foreach (ParticleSystem particleEmitter in explosionObject.GetComponentsInChildren<ParticleSystem>())
            {
                particleEmitter.Play();
            }

            if (runner.TryGetComponent<PlayerBehaviour>(out var player))
                player.StopVehicle();

            while (true)
            {
                yield return new WaitForSeconds(secondsToDisappear);
                SpriteRenderer[] sprites =
                    runner.
                    transform
                    .Find(runner.vehicle.vehicleObject.name + "(Clone)")
                    .gameObject
                    .GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in sprites)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }
    }
}