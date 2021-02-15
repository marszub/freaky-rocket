using PathCreation;
using UnityEngine;

namespace Assets.MapObject.Meteor
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour, IPositionWard
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(GetPosition());
        }

        public Vector2 GetPosition()
        {
            return pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
}