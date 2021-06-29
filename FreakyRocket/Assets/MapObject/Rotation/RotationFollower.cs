using Assets.GameManagement;
using System.Collections;
using UnityEngine;

namespace Assets.MapObject.Rotation
{
    public class RotationFollower : MonoBehaviour
    {
        public RotationScript rotationScript;

        private void Start()
        {
            transform.eulerAngles = new Vector3(0, 0, rotationScript.GetRotation(0));
        }

        private void FixedUpdate()
        {
            transform.eulerAngles = new Vector3(0, 0, rotationScript.GetRotation(GameplayController.instance.GetMapTime()));
        }
    }
}