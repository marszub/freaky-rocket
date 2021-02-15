using Assets.MapObject.Vehicle;
using System.Collections;
using UnityEngine;

namespace Assets.MapObject
{
    public interface IExplosive
    {
        IEnumerator Explode(PlayerBehaviour explodingObject);
    }
}
