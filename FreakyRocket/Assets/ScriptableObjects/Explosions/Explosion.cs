using Assets.Classes;
using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects.Explosions
{
    public abstract class Explosion : ScriptableObject, IExplosive
    {
        public abstract IEnumerator Explode(GameObject explodingObject);
    }
}