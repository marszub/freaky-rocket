using Assets.GameManagement;
using PathCreation;
using UnityEngine;

namespace Assets.MapObject.Motion
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour, IPositionWard
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float fullCycleTime; // in game segments
        [Range(0, 1)]  public float offset;

        public Vector2 GetPosition()
        {
            return pathCreator.path.GetPointAtTime(GameplayController.instance.GetMapTime()/fullCycleTime + offset, endOfPathInstruction);
        }
    }
}