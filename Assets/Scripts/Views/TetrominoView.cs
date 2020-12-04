using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Views
{
    public class TetrominoView : View
    {
        public readonly Signal updateFallSignal = new Signal();

        [SerializeField] public Vector3 rotationPosition;
        [SerializeField] public Vector3 previewPosition;
        public bool IsBoost { get; set; }

        private float FallTime { get; set; }
        private float _prevFallTime;

        public void Init(float value)
        {
            FallTime = value;
            _prevFallTime = Time.time;
        }

        private void LateUpdate()
        {
            if (Time.time - _prevFallTime > (IsBoost ? FallTime / 20 : FallTime))
            {
                _prevFallTime = Time.time;
                updateFallSignal.Dispatch();
                IsBoost = false;
            }
        }
    }
}