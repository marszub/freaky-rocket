using Assets.MapObject.Vehicle.Explosions;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.MapObject.Vehicle
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Vehicle")]
    public class Vehicle : ScriptableObject
    {
        public float maxVelocity;
        public GameObject vehicleObject;
        public Explosion explosion;

        public void Load(PlayerBehaviour runner)
        {
            if (runner.transform.Find(vehicleObject.name + "(Clone)"))
                Debug.LogError("Trying to load again a vehicle! " + vehicleObject.name + "(Clone)");
            else
                Instantiate(vehicleObject, runner.transform);
        }

        public void Unload(PlayerBehaviour runner)
        {
            var objectToDestroy = runner.transform.Find(vehicleObject.name + "(Clone)").gameObject;
            if (objectToDestroy)
                Destroy(objectToDestroy);
        }


        public IEnumerator TurnOn(PlayerBehaviour runner)
        {
            Debug.Log("Turn on rocket");
            if (runner.transform.Find(vehicleObject.name + "(Clone)").TryGetComponent(out Animator animator))
                animator.SetBool("IsRunning", true);
            else
                Debug.LogError("No vehicle instance!");
            yield return null;
        }

        public IEnumerator TurnOff(MonoBehaviour runner)
        {
            if (runner.transform.Find(vehicleObject.name + "(Clone)").TryGetComponent(out Animator animator))
                animator.SetBool("IsRunning", false);
            yield return null;
        }
    }
}