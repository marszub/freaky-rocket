using Assets.GameManagement;
using Assets.MapObject.Explosions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapObject.Goal
{
    class GoalBehaviour : MonoBehaviour, IKillable
    {
        [SerializeField] private Explosion explosion;
        public void Die()
        {
            Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
            GameplayController.instance.EndScreen(false);
            StartCoroutine(explosion.Explode(gameObject));
        }
    }
}
