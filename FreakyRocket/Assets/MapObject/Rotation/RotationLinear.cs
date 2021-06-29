using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapObject.Rotation
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Rotation/Linear")]
    class RotationLinear : RotationScript
    {
        public float startAngle;
        public float speed;
        public override float GetRotation(float t)
        {
            return t * speed + startAngle;
        }
    }
}
