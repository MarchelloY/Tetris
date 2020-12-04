using DG.Tweening;
using Models.api;
using Services;
using Services.api;
using Signals;
using strange.extensions.command.impl;
using TMPro;
using UnityEngine.SceneManagement;

namespace Commands
{
    public class ButtonClickedCommand : Command
    {
        [Inject] public PauseUIUpdatedSignal PauseUIUpdatedSignal { get; set; }
        [Inject] public string Name { get; set; }
        [Inject] public IGameStateModel GameStateModel { get; set; }
        [Inject] public IAudioModel AudioModel { get; set; }
        [Inject] public IDataService DataService { get; set; }
        [Inject("Save")] public ISaveService SaveService { get; set; }

        private TMP_Text _highScoreText;
        private TMP_Text _highLinesText;
    
        public override void Execute()
        {
            switch (Name)
            {
                case "ReplayButton":
                    ReplayPressed();
                    break;
                case "PauseButton":
                    PausePressed();
                    break;
                case "ContinueButton":
                    ContinuePressed();
                    break;
            }
        }

        private static void ReplayPressed()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }

        private void PausePressed()
        {
            GameStateModel.ShowPausePanel();
            AudioModel.ShotAudio(DataService.PauseClip);
            var saveData = (SaveData) SaveService.ReadData<SaveData>();
            if (saveData == null) return;
            PauseUIUpdatedSignal.Dispatch(new[] {saveData.highScore, saveData.highLines});
        }

        private void ContinuePressed()
        {
            GameStateModel.HidePausePanel();
            AudioModel.ShotAudio(DataService.ResumeClip);
        }
    }
}
