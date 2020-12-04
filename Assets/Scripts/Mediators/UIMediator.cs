using Signals;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class UIMediator : Mediator
    {
        [Inject] public UIView UIView { get; set; }
        [Inject] public MainUIUpdatedSignal MainUIUpdatedSignal { get; set; }
        [Inject] public PauseUIUpdatedSignal PauseUIUpdatedSignal { get; set; }

        public override void OnRegister()
        {
            MainUIUpdatedSignal.AddListener(OnMainUIUpdated);
            PauseUIUpdatedSignal.AddListener(OnPauseUIUpdated);
        }
    
        public override void OnRemove()
        {
            MainUIUpdatedSignal.RemoveListener(OnMainUIUpdated);
            PauseUIUpdatedSignal.RemoveListener(OnPauseUIUpdated);
        }

        private void OnMainUIUpdated(int[] values)
        {
            UIView.UpdateMainUI(values);
        }
        private void OnPauseUIUpdated(int[] values)
        {
            UIView.UpdatePauseUI(values);
        }
    }
}