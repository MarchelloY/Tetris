using Data;
using Diagnostics;
using Models.api;
using Services.api;
using Signals;
using strange.extensions.command.impl;
using Tools.Diagnostics;
using Tools.Managers.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Commands
{
    public class ButtonClickedCommand : Command
    {
        private readonly string _buttonName;
        private readonly PauseUIUpdatedSignal _pauseUIUpdatedSignal;
        private readonly IGameStateModel _gameStateModel;
        private readonly ISaveService _saveService;
        private readonly SetActivePanelSignal _setActivePanelSignal;
        private readonly PlayerActionHappenedSignal _playerActionHappenedSignal;

        public ButtonClickedCommand(string buttonName, PauseUIUpdatedSignal pauseUIUpdatedSignal,
            IGameStateModel gameStateModel, ISaveService saveService, SetActivePanelSignal setActivePanelSignal,
            PlayerActionHappenedSignal playerActionHappenedSignal)
        {
            _buttonName = buttonName;
            _pauseUIUpdatedSignal = pauseUIUpdatedSignal;
            _gameStateModel = gameStateModel;
            _saveService = saveService;
            _setActivePanelSignal = setActivePanelSignal;
            _playerActionHappenedSignal = playerActionHappenedSignal;
        }
    
        public override void Execute()
        {
            switch (_buttonName)
            {
                case StringCollection.REPLAY_BUTTON_NAME:
                    ReplayPressed();
                    break;
                case StringCollection.CONTINUE_BUTTON_NAME:
                    ContinuePressed();
                    break;
                case StringCollection.PAUSE_BUTTON_NAME:
                    PausePressed();
                    break;
                case StringCollection.EXIT_BUTTON_NAME:
                    Application.Quit();
                    break;
                case StringCollection.SETTINGS_BUTTON_NAME:
                    _setActivePanelSignal.Dispatch(StringCollection.SETTINGS_PANEL_NAME, true);
                    break;
                case StringCollection.SETTINGS_CLOSE_BUTTON_NAME:
                    _setActivePanelSignal.Dispatch(StringCollection.SETTINGS_PANEL_NAME, false);
                    break;
                case StringCollection.LEFT_MOVE_BUTTON_NAME:
                    _playerActionHappenedSignal.Dispatch(PlayerAction.MOVE_LEFT);
                    break;
                case StringCollection.RIGHT_MOVE_BUTTON_NAME:
                    _playerActionHappenedSignal.Dispatch(PlayerAction.MOVE_RIGHT);
                    break;
                case StringCollection.ROTATE_BUTTON_NAME:
                    _playerActionHappenedSignal.Dispatch(PlayerAction.ROTATE);
                    break;
                case StringCollection.DOWN_MOVE_BUTTON_NAME:
                    _playerActionHappenedSignal.Dispatch(PlayerAction.MOVE_DOWN);
                    break;
                default:
                    Debugger.LogError(LogEntryCategory.Input, $"Button name {_buttonName} is not supported");
                    break;
            }
        }

        private static void ReplayPressed()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }

        private void ContinuePressed()
        {
            _gameStateModel.Continue();
            AudioManager.Instance.PlayFx(SoundType.RESUME);
        }

        private void PausePressed()
        {
            _gameStateModel.Pause();
            AudioManager.Instance.PlayFx(SoundType.PAUSE);

            UpdatePauseUI();
        }

        private void UpdatePauseUI()
        {
            var saveData = (SaveData) _saveService.ReadData<SaveData>();

            if (saveData != null)
            {
                var gameData = new GameData()
                {
                    Score = saveData.HighScore,
                    Lines = saveData.HighLines,
                };

                _pauseUIUpdatedSignal.Dispatch(gameData);
            }
        }
    }
}