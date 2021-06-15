using Signals;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class ButtonMediator : Mediator
    {
        [Inject]
        private readonly ButtonView _view = null;
        [Inject]
        private readonly ButtonClickedSignal _buttonClickedSignal = null;

        public override void OnRegister()
        {
            base.OnRegister();

            _view.ButtonClicked += OnButtonClicked;
        }

        public override void OnRemove()
        {
            base.OnRemove();

            _view.ButtonClicked -= OnButtonClicked;
        }

        private void OnButtonClicked(string buttonName)
        {
            _buttonClickedSignal.Dispatch(buttonName);
        }
    }
}