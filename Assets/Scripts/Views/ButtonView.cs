using System;
using Diagnostics;
using strange.extensions.mediation.impl;
using Tools.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Views
{
    public class ButtonView : View,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
    {
        [SerializeField]
        private string _buttonName;
        [SerializeField]
        private Vector3 _scale;

        public event Action<string> ButtonClicked;

        protected override void OnEnable()
        {
            transform.localScale = Vector3.one;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one + _scale;

            Debugger.Log(LogEntryCategory.Input, $"Button has increased {_scale.x} times");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;

            Debugger.Log(LogEntryCategory.Input, $"Button has decreased {_scale.x} times");
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            ButtonClicked.SafeInvoke(_buttonName);

            Debugger.Log(LogEntryCategory.Input, $"The {_buttonName} button was pressed");
        }
    }
}