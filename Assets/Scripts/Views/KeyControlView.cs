using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Views
{
    public class KeyControlView : View
    { 
        public readonly Signal<KeyCode> keyPressedSignal = new Signal<KeyCode>();

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W))
                keyPressedSignal.Dispatch(KeyCode.W);
        
            if (Input.GetKey(KeyCode.A))
                keyPressedSignal.Dispatch(KeyCode.A);
        
            if (Input.GetKey(KeyCode.S))
                keyPressedSignal.Dispatch(KeyCode.S);
        
            if (Input.GetKey(KeyCode.D))
                keyPressedSignal.Dispatch(KeyCode.D);
        
        }
    }
}
