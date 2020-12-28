using Assets.ScriptableObjects.Explosions;
using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Vehicle")]
    public class Vehicle : ScriptableObject
    {
        public float maxVelocity;
        public GameObject vehicleObject;
        public Explosion explosion;
    }
}