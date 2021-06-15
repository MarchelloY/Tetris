using Data;
using Diagnostics;
using strange.extensions.mediation.impl;
using TMPro;
using Tools.Diagnostics;
using Tools.Managers;
using Tools.Managers.Audio;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Views
{
    public class UIView : View
    {
        [Header("Text fields")]
        [SerializeField]
        private TMP_Text _scoreText;
        [SerializeField]
        private TMP_Text _linesText;
        [SerializeField]
        private TMP_Text _levelsText;

        [SerializeField]
        private TMP_Text _highScoreText;
        [SerializeField]
        private TMP_Text _highLinesText;

        [Header("Panels")]
        [SerializeField]
        private GameObject _pausePanel;
        [SerializeField]
        private GameObject _gameOverPanel;
        [SerializeField]
        private GameObject _settingsPanel;

        [Header("Settings")]
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private TMP_Text _musicPercentageText;
        [SerializeField]
        private Slider _fxAudioSlider;
        [SerializeField]
        private TMP_Text _fxAudioPercentageText;

        [Header("LightBulbs")]
        [SerializeField]
        private Image[] _lightBulbs;

        protected override void Awake()
        {
            SaveManager.LoadPlayerPrefs();

            var musicVolume = AudioManager.Instance.MusicVolume;
            var fxAudioVolume = AudioManager.Instance.FxAudioVolume;

            UpdateMusicPercentage(musicVolume);
            UpdateMusicPercentage(fxAudioVolume);

            _musicSlider.value = musicVolume;
            _fxAudioSlider.value = fxAudioVolume;

            SetLightBulbsColor(StringCollection.YELLOW_COLOR);
        }

        public void UpdateMainUI(GameData gameData)
        {
            _scoreText.text = gameData.Score.ToString().ConvertScore(8);
            _linesText.text = gameData.Lines.ToString().ConvertScore(3);
            _levelsText.text = gameData.Level.ToString().ConvertScore(3);

            Debugger.Log(LogEntryCategory.UI,
                "The UI fields have been updated: " +
                $"'score' = {gameData.Score}, 'lines' = {gameData.Lines}, 'levels' = {gameData.Level};");
        }

        public void UpdatePauseUI(GameData gameData)
        {
            _highScoreText.text = "high score: " + gameData.Score;
            _highLinesText.text = "high lines: " + gameData.Lines;

            Debugger.Log(LogEntryCategory.UI,
                "The UI fields have been updated: " +
                $"'highScore' = {gameData.Score}, 'highLines' = {gameData.Lines};");
        }

        public void SetActivePanelByName(string panelName, bool isActive)
        {
            switch (panelName)
            {
                case StringCollection.PAUSE_PANEL_NAME:
                    _pausePanel.SetActive(isActive);
                    break;
                case StringCollection.GAME_OVER_PANEL_NAME:
                    _gameOverPanel.SetActive(isActive);
                    break;
                case StringCollection.SETTINGS_PANEL_NAME:
                    _settingsPanel.SetActive(isActive);
                    break;
                default:
                    Debugger.LogError(LogEntryCategory.UI, $"Panel name {panelName} is not supported");
                    break;
            }
        }

        //called from slider
        public void OnMusicVolumeChange()
        {
            var value = _musicSlider.value;
            AudioManager.Instance.MusicVolume = value;
            SaveManager.SavePlayerPrefs();
            UpdateMusicPercentage(value);
        }

        public void SetLightBulbsColor(Color32 color)
        {
            foreach (var lightBulb in _lightBulbs)
            {
                lightBulb.color = color;
            }
        }

        private void UpdateMusicPercentage(float value)
        {
            var percentage = (int) (value * 100);
            _musicPercentageText.text = $"{percentage} %";
        }

        //called from slider
        public void OnFxAudioVolumeChange()
        {
            var value = _fxAudioSlider.value;
            AudioManager.Instance.FxAudioVolume = value;
            SaveManager.SavePlayerPrefs();
            UpdateFxAudioPercentage(value);
        }

        private void UpdateFxAudioPercentage(float value)
        {
            var percentage = (int) (value * 100);
            _fxAudioPercentageText.text = $"{percentage} %";
        }
    }
}