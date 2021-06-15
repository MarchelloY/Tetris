using Models.api;
using Signals;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class TetrominoMediator : Mediator
    {
        [Inject]
        private readonly TetrominoView _view = null;
        [Inject]
        private readonly IGameStateModel _gameStateModel = null;
        [Inject]
        private readonly FellSignal _fellSignal = null;

        public override void OnRegister()
        {
            base.OnRegister();

            _view.Init(_gameStateModel);
            _view.Fell += OnFell;
        }

        public override void OnRemove()
        {
            base.OnRemove();

            _view.Fell -= OnFell;
        }

        private void OnFell()
        {
            _fellSignal.Dispatch();
        }
    }
}