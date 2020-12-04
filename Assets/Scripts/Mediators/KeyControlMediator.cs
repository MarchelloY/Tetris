using Signals;
using strange.extensions.mediation.impl;
using UnityEngine;
using Views;

namespace Mediators
{
    public class KeyControlMediator : Mediator
    {
        [Inject] public KeyPressedSignal KeyPressedSignal { get; set; }
        [Inject] public KeyControlView ButtonControlView { get; set; }

        public override void OnRegister()
        {
            ButtonControlView.keyPressedSignal.AddListener(OnKeyPressed);
        }

        public override void OnRemove()
        {
            ButtonControlView.keyPressedSignal.RemoveListener(OnKeyPressed);
        }
    
        private void OnKeyPressed(KeyCode key)
        {
            KeyPressedSignal.Dispatch(key);
        }
    }
}
