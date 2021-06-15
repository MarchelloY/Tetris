using Data;
using Services.api;
using Signals;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class KeyControlMediator : Mediator
    {
        [Inject]
        private readonly KeyControlView _view = null;
        [Inject]
        private readonly IDataService _dataService = null;
        [Inject]
        private readonly PlayerActionHappenedSignal _playerActionHappenedSignal = null;

        public override void OnRegister()
        {
            base.OnRegister();

            _view.Init(_dataService.MoveDelay);
            _view.KeyPressed += OnKeyPressed;
        }

        public override void OnRemove()
        {
            base.OnRemove();

            _view.KeyPressed += OnKeyPressed;
        }
    
        private void OnKeyPressed(PlayerAction playerAction)
        {
            _playerActionHappenedSignal.Dispatch(playerAction);
        }
    }
}