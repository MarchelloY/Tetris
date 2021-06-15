using Models.api;
using strange.extensions.command.impl;
using Tools.Managers.Audio;

namespace Commands
{
    public class FellCommand : Command
    {
        private readonly ITetrominoModel _tetrominoModel;
        private readonly IGridModel _gridModel;
        private readonly IGameStateModel _gameStateModel;
        private readonly IScoreModel _scoreModel;

        public FellCommand(ITetrominoModel tetrominoModel, IGridModel gridModel, IGameStateModel gameStateModel,
            IScoreModel scoreModel)
        {
            _tetrominoModel = tetrominoModel;
            _gridModel = gridModel;
            _gameStateModel = gameStateModel;
            _scoreModel = scoreModel;
        }

        public override void Execute()
        {
            if (!_gameStateModel.IsPause && !_gameStateModel.IsGameOver)
            {
                var currentTransform = _tetrominoModel.CurrentTetromino.transform;

                if (_tetrominoModel.MoveDown())
                {
                    _gridModel.TryDeleteRows();

                    if (_gridModel.CheckIsAboveGrid(currentTransform))
                    {
                        _gameStateModel.GameOver();
                    }
                    else
                    {
                        _tetrominoModel.NextStep();
                        AudioManager.Instance.PlayFx(SoundType.DROP);
                    }
                }
                else
                {
                    _gridModel.UpdateGrid(currentTransform);
                    AudioManager.Instance.PlayFx(SoundType.MOVE);
                }

                if (_gameStateModel.IsBoost)
                {
                    _scoreModel.UpdateScoreByBoost();
                }

                _gameStateModel.IsBoost = false;
            }
        }
    }
}