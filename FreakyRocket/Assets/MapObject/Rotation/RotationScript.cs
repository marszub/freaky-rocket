using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Rotation
{
    public abstract class RotationScript : ScriptableObject
    {
        public abstract float GetRotation(float t);
    }
}