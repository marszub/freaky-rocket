using Assets.MapObject.Explosions;
using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Vehicle
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Vehicle")]
    public class Vehicle : ScriptableObject
    {
        public float maxVelocity;
        public GameObject vehicleObject;
        public Explosion explosion;
        public float onOfftransitionTime;
        [Range(0, 1)] public float volume;

        public void Load(PlayerBehaviour runner)
        {
            if (runner.transform.Find(vehicleObject.name + "(Clone)"))
                Debug.LogError("Trying to load again a vehicle! " + vehicleObject.name + "(Clone)");
            else
            {
                GameObject vehicle = Instantiate(vehicleObject, runner.transform);
                vehicle.GetComponent<AudioSource>().volume = 0.0f;
            }
        }

        public void Unload(PlayerBehaviour runner)
        {
            var objectToDestroy = runner.transform.Find(vehicleObject.name + "(Clone)").gameObject;
            if (objectToDestroy)
                Destroy(objectToDestroy);
        }


        public IEnumerator TurnOn(PlayerBehaviour runner)
        {
            GameObject vehicle = runner.transform.Find(vehicleObject.name + "(Clone)").gameObject;
            if (vehicle != null)
            {
                if (vehicle.TryGetComponent(out Animator animator))
                    animator.SetBool("IsRunning", true);
                else
                    Debug.LogError("No animator!");

                if (vehicle.TryGetComponent(out AudioSource audio))
                {
                    float currentTime = 0;

                    while (currentTime < onOfftransitionTime)
                    {
                        audio.volume = (currentTime / onOfftransitionTime) * volume;
                        yield return null;
                        currentTime += Time.deltaTime;
                    }
                    audio.volume = volume;
                }
                else
                    Debug.LogError("No audio source!");
            }
            else
                Debug.LogError("No vehicle instance!");
            yield break;
        }

        public IEnumerator TurnOff(MonoBehaviour runner)
        {
            GameObject vehicle = runner.transform.Find(vehicleObject.name + "(Clone)").gameObject;
            if (vehicle != null)
            {
                if (vehicle.TryGetComponent(out Animator animator))
                    animator.SetBool("IsRunning", false);
                else
                    Debug.LogError("No animator!");

                if (vehicle.TryGetComponent(out AudioSource audio))
                {
                    float currentTime = onOfftransitionTime;

                    while (currentTime > 0)
                    {
                        audio.volume = (currentTime / onOfftransitionTime) * volume;
                        yield return null;
                        currentTime -= Time.deltaTime;
                    }
                    audio.volume = 0.0f;
                }
                else
                    Debug.LogError("No audio source!");
            }
            else
                Debug.LogError("No vehicle instance!");
            yield break;
        }
    }
}