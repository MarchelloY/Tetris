using System;
using Diagnostics;
using Models.api;
using strange.extensions.mediation.impl;
using Tools.Diagnostics;
using UnityEngine;
using Utils;

namespace Views
{
    public class TetrominoView : View
    {
        [SerializeField]
        private Vector3 _rotationPosition;
        [SerializeField]
        private Vector3 _previewPosition;

        private float _prevFallDelay;
        private IGameStateModel _gameStateModel;

        public event Action Fell;

        public Vector3 RotationPosition => _rotationPosition;
        public Vector3 PreviewPosition => _previewPosition;

        public void Init(IGameStateModel gameStateModel)
        {
            _prevFallDelay = Time.time;
            _gameStateModel = gameStateModel;

            Debugger.Log(LogEntryCategory.Tetromino,
                $"{nameof(TetrominoView)} initialization completed successfully");
        }

        private void LateUpdate()
        {
            if (Time.time - _prevFallDelay > _gameStateModel.FallDelay)
            {
                _prevFallDelay = Time.time;
                Fell.SafeInvoke();

                Debugger.Log(LogEntryCategory.Tetromino, $"Figure {gameObject.name} moved down");
            }
        }
    }
}