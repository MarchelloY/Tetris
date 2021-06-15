using Data;
using Models.api;
using strange.extensions.command.impl;
using Tools.Managers.Audio;

namespace Commands
{
    public class PlayerActionHappenedCommand : Command
    {
        private readonly PlayerAction _playerAction;
        private readonly IGameStateModel _gameStateModel;
        private readonly ITetrominoModel _tetrominoModel;
        private readonly IGridModel _gridModel;

        public PlayerActionHappenedCommand(PlayerAction playerAction, IGameStateModel gameStateModel,
            ITetrominoModel tetrominoModel, IGridModel gridModel)
        {
            _playerAction = playerAction;
            _gameStateModel = gameStateModel;
            _tetrominoModel = tetrominoModel;
            _gridModel = gridModel;
        }
    
        public override void Execute()
        {
            if (!_gameStateModel.IsPause && !_gameStateModel.IsGameOver)
            {
                var currentTransform = _tetrominoModel.CurrentTetromino.transform;

                switch (_playerAction)
                {
                    case PlayerAction.MOVE_LEFT:
                    {
                        if (!_tetrominoModel.MoveLeft())
                        {
                            _gridModel.UpdateGrid(currentTransform);
                            AudioManager.Instance.PlayFx(SoundType.MOVE);
                        }
                        break;
                    }
                    case PlayerAction.MOVE_RIGHT:
                    {
                        if (!_tetrominoModel.MoveRight())
                        {
                            _gridModel.UpdateGrid(currentTransform);
                            AudioManager.Instance.PlayFx(SoundType.MOVE);
                        }
                        break;
                    }
                    case PlayerAction.MOVE_DOWN:
                        _gameStateModel.IsBoost = true;
                        break;
                    case PlayerAction.ROTATE:
                        if (!_tetrominoModel.Rotate())
                        {
                            _gridModel.UpdateGrid(currentTransform);
                            AudioManager.Instance.PlayFx(SoundType.ROTATE);
                        }
                        break;
                }
            }
        }
    }
}