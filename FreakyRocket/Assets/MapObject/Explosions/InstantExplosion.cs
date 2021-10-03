using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Explosions
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Explosion/Instant")]
    public class InstantExplosion : Explosion
    {
        public GameObject explosionPrefab;
        public float secondsToDisappear;
        private GameObject explosionObject = null;

        public override IEnumerator Explode(GameObject runner)
        {
            if (explosionObject == null)
                explosionObject = Instantiate(explosionPrefab, runner.transform);
            foreach (ParticleSystem particleEmitter in explosionObject.GetComponentsInChildren<ParticleSystem>())
            {
                particleEmitter.Play();
            }

            while (true)
            {
                yield return new WaitForSeconds(secondsToDisappear);
                SpriteRenderer[] sprites = runner.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in sprites)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }
    }
}