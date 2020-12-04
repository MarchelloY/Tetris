using Models.api;
using strange.extensions.command.impl;
using UnityEngine;
using Views;

namespace Commands
{
    public class KeyPressedCommand : Command
    {
        [Inject] public KeyCode KeyCode { get; set; }
        [Inject] public IGameStateModel GameStateModel { get; set; }
        [Inject] public ITetrominoModel TetrominoModel { get; set; }
        
        private TetrominoView _currentTetromino;
    
        public override void Execute()
        {
            if(GameStateModel.IsPause || GameStateModel.IsGameOver) return;

            _currentTetromino = GameStateModel.NextTetromino.GetComponent<TetrominoView>();

            switch (KeyCode)
            {
                case KeyCode.A:
                    TetrominoModel.MoveLeft();
                    break;
                case KeyCode.W:
                    TetrominoModel.Rotate();
                    break;
                case KeyCode.S:
                    TetrominoModel.IsBoost = true;
                    _currentTetromino.IsBoost = true;
                    break;
                case KeyCode.D:
                    TetrominoModel.MoveRight();
                    break;
            }
        }
    }
}