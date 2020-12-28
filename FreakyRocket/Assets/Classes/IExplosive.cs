using System.Collections;
using UnityEngine;

namespace Assets.Classes
{
    public interface IExplosive
    {
        IEnumerator Explode(GameObject explodingObject);
    }
}
