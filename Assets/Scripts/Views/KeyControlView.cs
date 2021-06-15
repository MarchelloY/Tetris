using System;
using Data;
using Diagnostics;
using strange.extensions.mediation.impl;
using Tools.Diagnostics;
using UnityEngine;
using Utils;

namespace Views
{
    public class KeyControlView : View
    {
        public event Action<PlayerAction> KeyPressed;

        private float _prevMoveTime;
        private float _moveDelay;

        public void Init(float moveDelay)
        {
            _moveDelay = moveDelay;
            _prevMoveTime = Time.time;
        }

        private void LateUpdate()
        {
            if (Time.time - _prevMoveTime > _moveDelay)
            {
                _prevMoveTime = Time.time;

                if (Input.GetKey(KeyCode.A))
                {
                    KeyPressed.SafeInvoke(PlayerAction.MOVE_LEFT);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    KeyPressed.SafeInvoke(PlayerAction.MOVE_RIGHT);
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                KeyPressed.SafeInvoke(PlayerAction.ROTATE);
            }

            if (Input.GetKey(KeyCode.S))
            {
                KeyPressed.SafeInvoke(PlayerAction.MOVE_DOWN);
            }

#if KEY_DEBUG
            if (!string.IsNullOrEmpty(Input.inputString))
            {
                Debugger.Log(LogEntryCategory.Input, $"The {Input.inputString.ToUpper()} key was pressed");
            }
#endif
        }
    }
}