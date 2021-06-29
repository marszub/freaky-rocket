using UnityEditor;

namespace Assets.MapObject.Motion
{
    [CustomEditor(typeof(PathFollower))]
    public class PathFollowerEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (EditorApplication.isPlaying)
                return;
            var pathFollower = target as PathFollower;
            if (pathFollower.pathCreator != null)
            {
                pathFollower.transform.position = pathFollower.pathCreator.path.GetPointAtTime(pathFollower.offset);
            }
        }
    }
}