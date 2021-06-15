using Tools.Managers.Audio;
using UnityEngine;

namespace Tools.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private const string FX_AUDIO_VOLUME_KEY = "fxAudioVolume";
        private const string MUSIC_VOLUME_KEY = "musicVolume";

        public static void LoadPlayerPrefs()
        {
            AudioManager.Instance.FxAudioVolume = PlayerPrefs.GetFloat(FX_AUDIO_VOLUME_KEY,
                AudioManager.Instance.DefaultVolume);
            AudioManager.Instance.MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY,
                AudioManager.Instance.DefaultVolume);
        }

        public static void SavePlayerPrefs()
        {
            PlayerPrefs.SetFloat(FX_AUDIO_VOLUME_KEY, AudioManager.Instance.FxAudioVolume);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, AudioManager.Instance.MusicVolume);
            PlayerPrefs.Save();
        }
    }
}