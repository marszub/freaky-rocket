using UnityEngine;

namespace Assets.GameManagement
{
    public class AspectRatioWard : MonoBehaviour
    {
        private const float ratio = 16f/9f;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            UpdateRatio();
        }

        private void UpdateRatio()
        {
            float currentRatio = (float)Screen.width / Screen.height;
            if (currentRatio > ratio)
                Screen.SetResolution((int)(Screen.height * ratio), Screen.height, Screen.fullScreen);
            else if (currentRatio < ratio)
                Screen.SetResolution(Screen.width, (int)(Screen.width / ratio), Screen.fullScreen);
        }
    }
}