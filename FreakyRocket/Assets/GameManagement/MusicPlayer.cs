using System.Collections;
using UnityEngine;

namespace Assets.GameManagement
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private new AudioSource audio;
        [SerializeField] private AudioClip loopTrack;
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            audio.Play();
        }

        private void Update()
        {
            if (!audio.isPlaying)
            {
                audio.clip = loopTrack;
                audio.loop = true;
                audio.Play();
            }
        }
    }
}