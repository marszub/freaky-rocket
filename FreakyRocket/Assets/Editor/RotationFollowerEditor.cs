using UnityEditor;
using UnityEngine;

namespace Assets.MapObject.Rotation
{
    [CustomEditor(typeof(RotationFollower))]
    public class RotationFollowerEditor : Editor
    {
        private void OnSceneGUI()
        {
            var rotationFollower = target as RotationFollower;
            if (rotationFollower.rotationScript != null)
            {
                rotationFollower.transform.eulerAngles = new Vector3(0, 0, rotationFollower.rotationScript.GetRotation(0.0f));
            }
        }
    }
}