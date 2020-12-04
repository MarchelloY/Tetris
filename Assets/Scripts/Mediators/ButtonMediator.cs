using Signals;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class ButtonMediator : Mediator
    {
        [Inject] public ButtonView ButtonsView { get; set; }
    
        [Inject] public ButtonClickedSignal ButtonClickedSignal { get; set; }

        public override void OnRegister()
        {
            ButtonsView.buttonClickedSignal.AddListener(OnButtonClicked);
        }

        public override void OnRemove()
        {
            ButtonsView.buttonClickedSignal.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked(string name)
        {
            ButtonClickedSignal.Dispatch(name);
        }
    }
}
