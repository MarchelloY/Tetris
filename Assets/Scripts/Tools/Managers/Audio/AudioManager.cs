using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Diagnostics;
using Tools.Diagnostics;
using UnityEngine;

namespace Tools.Managers.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private const int SOUND_AUDIO_SOURCES_COUNT = 5;
        private const int MUSIC_AUDIO_SOURCES_COUNT = 1;

        private AudioSource[] _fxAudioSources;
        private AudioSource[] _musicAudioSources;
        private float _fxAudioVolume;
        private float _musicVolume;

        private bool _isInit;

        [SerializeField]
        private AudioClip _mainMusic;

        [HideInInspector]
        public AudioClip[] AudioClips;

        public float FxAudioVolume
        {
            get => _fxAudioVolume;
            set
            {
                _fxAudioVolume = value;
                ChangeFxAudioVolume();
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                ChangeMusicVolume();
            }
        }

        public float DefaultVolume { get; } = 1f;


        public override void Init()
        {
            if (!_isInit)
            {
                InitFxAudioSources();
                InitMusicAudioSources();

                PlayMusic(_mainMusic);
                _isInit = true;
            }
        }

        public void PlayFx(SoundType soundType)
        {
            var audioSource = GetFirstAvailableSource(_fxAudioSources);

            var clip = AudioClips[(int) soundType];

            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                Debugger.LogError(LogEntryCategory.Audio, "There is no available fx audio source");
            }
        }

        private void PlayMusic(AudioClip audioClip)
        {
            var audioSource = GetFirstAvailableSource(_musicAudioSources);

            if (audioSource != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                Debugger.LogError(LogEntryCategory.Audio, "There is no available music audio source");
            }
        }

        private void InitFxAudioSources()
        {
            _fxAudioSources = new AudioSource[SOUND_AUDIO_SOURCES_COUNT];

            for (var i = 0; i < SOUND_AUDIO_SOURCES_COUNT; i++)
            {
                _fxAudioSources[i] = CreateAudioSource($"FX source {i.ToString()}");
            }
        }

        private void InitMusicAudioSources()
        {
            _musicAudioSources = new AudioSource[MUSIC_AUDIO_SOURCES_COUNT];

            for (var i = 0; i < MUSIC_AUDIO_SOURCES_COUNT; i++)
            {
                _musicAudioSources[i] = CreateAudioSource($"Music source {i.ToString()}");
                _musicAudioSources[i].loop = true;
            }
        }

        private AudioSource CreateAudioSource(string gameObjName)
        {
            var audioSource = new GameObject(gameObjName);
            var fxSource = audioSource.AddComponent<AudioSource>();
            audioSource.transform.parent = transform;

            return fxSource;
        }

        private static AudioSource GetFirstAvailableSource(IEnumerable<AudioSource> audioSources)
        {
            return audioSources.FirstOrDefault(audioSource => !audioSource.isPlaying);
        }

        private void ChangeFxAudioVolume()
        {
            foreach (var fxAudioSource in _fxAudioSources)
            {
                fxAudioSource.volume = FxAudioVolume;
            }
        }

        private void ChangeMusicVolume()
        {
            foreach (var musicAudioSource in _musicAudioSources)
            {
                musicAudioSource.volume = MusicVolume;
            }
        }
    }
}