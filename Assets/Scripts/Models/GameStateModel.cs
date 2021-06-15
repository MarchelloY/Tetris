using Data;
using Models.api;
using Services.api;
using Signals;

namespace Models
{
    public class GameStateModel : IGameStateModel
    {
        [Inject]
        private readonly IDataService _dataService = null;
        [Inject]
        private readonly SetActivePanelSignal _setActivePanelSignal = null;

        private float _fallDelay;

        public bool IsGameOver { get; private set; }
        public bool IsPause { get; private set; }

        public bool IsBoost { get; set; }
        public float FallDelay
        {
            get => IsBoost ? _fallDelay * _dataService.FallBoostModifier : _fallDelay;
            set => _fallDelay = value;
        }

        public void Init()
        {
            _fallDelay = 1f;

            IsPause = false;
            IsGameOver = false;
            IsBoost = false;
        }

        public void Pause()
        {
            SetActivePausePanel(true);
        }

        public void Continue()
        {
            SetActivePausePanel(false);
        }

        public void GameOver()
        {
            IsGameOver = true;
            _setActivePanelSignal.Dispatch(StringCollection.GAME_OVER_PANEL_NAME, true);
        }

        private void SetActivePausePanel(bool isPause)
        {
            IsPause = isPause;
            _setActivePanelSignal.Dispatch(StringCollection.PAUSE_PANEL_NAME, isPause);
        }
    }
}