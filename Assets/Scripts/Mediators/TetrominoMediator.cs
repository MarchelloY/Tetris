using Models.api;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class TetrominoMediator : Mediator
    {
        [Inject] public TetrominoView TetrominoView { get; set; }
        [Inject] public ITetrominoModel TetrominoModel { get; set; }
        [Inject] public IScoreModel ScoreModel { get; set; }

        public override void OnRegister()
        {
            TetrominoView.Init(ScoreModel.FallTime);
            TetrominoView.updateFallSignal.AddListener(UpdateFall);
        }

        public override void OnRemove()
        {
            TetrominoView.updateFallSignal.RemoveListener(UpdateFall);
        }

        private void UpdateFall()
        {
            TetrominoModel.Update();
        }
    }
}
