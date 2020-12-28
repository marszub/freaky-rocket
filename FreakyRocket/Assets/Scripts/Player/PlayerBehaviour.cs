using Assets.Classes;
using Assets.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Vehicle vehicle;
        private float velocity;
        private GameObject vehicleObject;
        private bool canRotate;

        private void Start()
        {
            LoadVehicle();

            velocity = 0.0f;
            canRotate = true;

            GameplayController.Play += StartVehicle;
        }

        private void LoadVehicle()
        {
            vehicleObject = Instantiate(vehicle.vehicleObject, transform);
        }

        private void UnloadVehicle()
        {
            Destroy(vehicleObject.gameObject);
        }

        public void StartVehicle()
        {
            velocity = vehicle.maxVelocity;
        }

        public void StopVehicle()
        {
            canRotate = false;
            velocity = 0.0f;
        }

        public void Update()
        {
            Vector2 direction = GameplayController.GetMoveDirection(transform.position);
            if (canRotate)
                transform.up = direction;
            float deltaDistance = Time.unscaledDeltaTime * velocity;
            transform.position = (Vector2)transform.position + (Vector2)transform.up.normalized * deltaDistance;
        }

        public void Die() => StartCoroutine(vehicle.explosion.Explode(gameObject));

        //private void MovePlayer()
        //{
        //    Vector2 direction = GameplayController.GetMoveDirection(transform.position);
        //    float deltaDistance = Time.unscaledDeltaTime * velocity;
        //    transform.up = direction;
        //    transform.position = (Vector2)transform.position + direction * deltaDistance;
        //}
    }
}