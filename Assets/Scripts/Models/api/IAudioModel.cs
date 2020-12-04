using UnityEngine;

namespace Models.api
{
    public interface IAudioModel
    {
        void Init();
        void ShotAudio(AudioClip audioClip);
    }
}
