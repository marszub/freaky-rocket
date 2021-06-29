using Assets.GameManagement;
using UnityEngine;
using System;

namespace Assets.MapObject.Vehicle
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Vehicle vehicle;
        private float velocity;
        private bool canRotate;

        private enum PlayerState
        {
            Preparing,
            Running
        }
        private PlayerState playerState;

        private void Start()
        {
            vehicle.Load(this);

            velocity = 0.0f;
            canRotate = true;
            playerState = PlayerState.Preparing;

            GameplayController.Play += StartVehicle;
        }

        public void StartVehicle()
        {
            switch (playerState)
            {
                case PlayerState.Preparing:
                    velocity = vehicle.maxVelocity;
                    canRotate = true;
                    playerState = PlayerState.Running;
                    StartCoroutine(vehicle.TurnOn(this));
                    break;

                case PlayerState.Running:
                    break;

                default:
                    throw new NotImplementedException("Start Vehicle case.");
            }
            
        }

        public void StopVehicle()
        {
            canRotate = false;
            velocity = 0.0f;
            StartCoroutine(vehicle.TurnOff(this));
        }

        public void TurnOffEngines()
        {
            canRotate = false;
            StartCoroutine(vehicle.TurnOff(this));
        }

        public void Update()
        {
            Vector2 direction = GameplayController.instance.GetMoveDirection(transform.position);
            if (canRotate)
                transform.up = direction;
            float deltaDistance = Time.unscaledDeltaTime * velocity;
            transform.position = (Vector2)transform.position + (Vector2)transform.up.normalized * deltaDistance;
        }

        public void Die()
        {
            Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
            StartCoroutine(vehicle.explosion.Explode(this));
            GameplayController.instance.EndScreen(false);
        }

        //private void MovePlayer()
        //{
        //    Vector2 direction = GameplayController.GetMoveDirection(transform.position);
        //    float deltaDistance = Time.unscaledDeltaTime * velocity;
        //    transform.up = direction;
        //    transform.position = (Vector2)transform.position + direction * deltaDistance;
        //}
    }
}