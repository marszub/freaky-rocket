using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Vehicle.Explosions
{
    public abstract class Explosion : ScriptableObject, IExplosive
    {
        public abstract IEnumerator Explode(PlayerBehaviour explodingObject);
    }
}