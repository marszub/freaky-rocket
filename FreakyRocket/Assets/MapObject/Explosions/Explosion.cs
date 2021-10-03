using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Explosions
{
    public abstract class Explosion : ScriptableObject
    {
        public abstract IEnumerator Explode(GameObject runner);
    }
}