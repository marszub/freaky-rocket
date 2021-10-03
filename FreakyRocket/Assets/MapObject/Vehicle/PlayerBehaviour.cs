using Assets.GameManagement;
using UnityEngine;
using System;
using UnityEditor;

namespace Assets.MapObject.Vehicle
{
    public class PlayerBehaviour : MonoBehaviour, IKillable
    {
        public Vehicle vehicle;
        private float velocity;
        private bool canRotate;
        private bool pointerInside;

        private enum PlayerState
        {
            Preparing,
            Running,
            Stop,
            Off
        }
        private PlayerState playerState;

        void Start()
        {
            vehicle.Load(this);

            pointerInside = false;
            velocity = 0.0f;
            canRotate = true;
            playerState = PlayerState.Preparing;

            GameplayController.Play += StartVehicle;
            GameplayController.Stop += StopVehicle;
        }

        void Update()
        {
            if (canRotate)
                transform.up = GameplayController.instance.GetMoveDirection(transform.position);
            float deltaDistance = Time.unscaledDeltaTime * velocity;
            transform.position = (Vector2)transform.position + (Vector2)transform.up.normalized * deltaDistance;
        }

        private void OnMouseEnter()
        {
            pointerInside = true;
            if (playerState == PlayerState.Running)
                TurnOffEngines();
        }

        private void OnMouseExit()
        {
            pointerInside = false;
        }

        private void OnDestroy()
        {
            GameplayController.Play -= StartVehicle;
            GameplayController.Stop -= StopVehicle;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (EditorApplication.isPlaying)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1);
        }
#endif

        public void StartVehicle()
        {
            switch (playerState)
            {
                case PlayerState.Preparing:
                    if (pointerInside)
                        break;
                    velocity = vehicle.maxVelocity;
                    canRotate = true;
                    playerState = PlayerState.Running;
                    StartCoroutine(vehicle.TurnOn(this));
                    break;

                case PlayerState.Off:
                    if (pointerInside)
                    {
                        GameplayController.instance.PlayerOff();
                        break;
                    }
                    TurnOnEngines();
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
            if(playerState == PlayerState.Running)
                StartCoroutine(vehicle.TurnOff(this));
            playerState = PlayerState.Stop;
        }

        public void TurnOffEngines()
        {
            canRotate = false;
            StartCoroutine(vehicle.TurnOff(this));
            GameplayController.instance.PlayerOff();
            playerState = PlayerState.Off;
        }

        public void TurnOnEngines()
        {
            canRotate = true;
            StartCoroutine(vehicle.TurnOn(this));
            playerState = PlayerState.Running;
        }

        public void Die()
        {
            Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
            StopVehicle();
            StartCoroutine(vehicle.explosion.Explode(gameObject));
            GameplayController.instance.EndScreen(false);
        }
    }
}