using Models.api;
using Services.api;
using UnityEngine;
using Views;
using Vector3 = UnityEngine.Vector3;

namespace Models
{
    public class TetrominoModel : ITetrominoModel
    {
        [Inject] public IAudioModel AudioModel { get; set; }
        [Inject] public IDataService DataService { get; set; }
        [Inject] public IScoreModel ScoreModel { get; set; }
        [Inject] public IGameStateModel GameStateModel { get; set; }
        [Inject] public IGridModel GridModel { get; set; }
        
        private const float MoveTime = 0.1f;
        private float _prevMoveTime;
        
        public bool IsBoost { get; set; }
        
        public void Update()
        {
            if (GameStateModel == null) return;
            if (GameStateModel.IsPause || GameStateModel.IsGameOver) return;
            MoveDown();
        }
    
        public void MoveLeft()
        {
            var currentTransform = GameStateModel.NextTetromino.transform;
            var currentView = currentTransform.GetComponent<TetrominoView>();
            
            if (!(Time.time - _prevMoveTime > MoveTime)) return;
            
            _prevMoveTime = Time.time;
            currentTransform.position += Vector3.left;
            if (!CheckIsValidPosition())
                currentTransform.position += Vector3.right;
            else
            {
                GridModel.UpdateGrid(currentView);
                AudioModel.ShotAudio(DataService.MoveClip);
            }
        }

        public void MoveRight()
        {
            var currentTransform = GameStateModel.NextTetromino.transform;
            var currentView = currentTransform.GetComponent<TetrominoView>();
            
            if (!(Time.time - _prevMoveTime > MoveTime)) return;
            
            _prevMoveTime = Time.time;
            currentTransform.position += Vector3.right;
            if (!CheckIsValidPosition())
                currentTransform.position += Vector3.left;
            else
            {
                GridModel.UpdateGrid(currentView);
                AudioModel.ShotAudio(DataService.MoveClip);
            }
        }

        public void Rotate()
        {
            var currentTransform = GameStateModel.NextTetromino.transform;
            var currentView = currentTransform.GetComponent<TetrominoView>();
            currentTransform.RotateAround(
                currentTransform.TransformPoint(currentView.rotationPosition), Vector3.forward, -90);
            if (!CheckIsValidPosition())
                currentTransform.RotateAround(
                    currentTransform.TransformPoint(currentView.rotationPosition), Vector3.forward, 90);
            else
            {
                GridModel.UpdateGrid(currentView);
                AudioModel.ShotAudio(DataService.RotateClip);
            }
        }

        private void MoveDown()
        {
            var currentTransform = GameStateModel.NextTetromino.transform;
            var currentView = currentTransform.GetComponent<TetrominoView>();
            
            currentTransform.position += Vector3.down;
        
            if (IsBoost) ScoreModel.BoostScore();
        
            if (!CheckIsValidPosition())
            {
                currentTransform.position += Vector3.up;
                GridModel.DeleteRow();
                if (GridModel.CheckIsAboveGrid(currentView))
                    GameStateModel.GameOver();
                currentView.enabled = false;
                if (!GameStateModel.IsGameOver)
                {
                    AudioModel.ShotAudio(DataService.DropClip);
                    GameStateModel.SpawnNextTetromino();
                }
            }
            else
            {
                var current = GameStateModel.NextTetromino.GetComponent<TetrominoView>();
                GridModel.UpdateGrid(current);
                AudioModel.ShotAudio(DataService.MoveClip);
            }
            IsBoost = false;
        }
    
        private bool CheckIsValidPosition()
        {
            var current = GameStateModel.NextTetromino.transform;
            foreach (Transform mino in current)
            {
                if (GridModel == null) return false;
                var pos = GridModel.Round(mino.position);
                if (!GridModel.CheckIsInsideBoard(pos)) return false;
                if (GridModel.GetTransformAtGridPosition(pos) != null &&
                    GridModel.GetTransformAtGridPosition(pos).parent != current) return false;
            }
            return true;
        }
    }
}