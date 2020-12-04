using Models.api;
using UnityEngine;

namespace Models
{
    public class AudioModel : IAudioModel
    {
        private AudioSource _audioSource;

        public void Init()
        {
            _audioSource = Object.FindObjectOfType<AudioSource>();
        }

        public void ShotAudio(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    
    }
}
