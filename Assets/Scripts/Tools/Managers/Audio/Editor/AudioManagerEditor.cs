using System;
using UnityEditor;
using UnityEngine;

namespace Tools.Managers.Audio.Editor
{
    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : UnityEditor.Editor
    {
        private int _clipsCount;
        private AudioManager _audioManager;

        private void OnEnable()
        {
            _audioManager = (AudioManager) target;
            _clipsCount = Enum.GetNames(typeof(SoundType)).Length;

            if (_audioManager.AudioClips == null)
            {
                _audioManager.AudioClips = new AudioClip[_clipsCount];
            }
            else if (_audioManager.AudioClips.Length != _clipsCount)
            {
                var temp = _audioManager.AudioClips;
                _audioManager.AudioClips = new AudioClip[_clipsCount];
                var length = _clipsCount > temp.Length ? temp.Length : _clipsCount;
                Array.Copy(temp, 0, _audioManager.AudioClips, 0, length);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("Sounds:");

            var sounds = Enum.GetNames(typeof(SoundType));

            for (var i = 0; i < sounds.Length; i++)
            {
                var type = (SoundType) i;

                EditorGUILayout.BeginHorizontal();

                var label = $" {i + 1}) {type.ToString().ToLower()} clip";

                _audioManager.AudioClips[i] = (AudioClip) EditorGUILayout.ObjectField(label,
                    _audioManager.AudioClips[i], typeof(AudioClip), false);

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}