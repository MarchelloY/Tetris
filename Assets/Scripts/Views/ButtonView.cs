using DG.Tweening;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views
{
    public class ButtonView : View,
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerClickHandler
    {
        public readonly Signal<string> buttonClickedSignal = new Signal<string>(); 
    
        [SerializeField] private float duration;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one * 1.1f, duration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, duration);
        }
    
        public void OnPointerClick(PointerEventData eventData)
        {
            buttonClickedSignal.Dispatch(eventData.selectedObject.name);
        }
    }
}
