using Diagnostics;
using Models.api;
using Services.api;
using strange.extensions.command.impl;
using Tools.Diagnostics;
using Tools.Managers.Audio;

namespace Commands
{
    public class GameSetupCommand : Command
    {
        private readonly IDataService _dataService;
        private readonly IGridModel _gridModel;
        private readonly IGameStateModel _gameStateModel;
        private readonly IScoreModel _scoreModel;
        private readonly ITetrominoModel _tetrominoModel;

        public GameSetupCommand(IDataService dataService, IGridModel gridModel, IGameStateModel gameStateModel,
            IScoreModel scoreModel, ITetrominoModel tetrominoModel)
        {
            _dataService = dataService;
            _gridModel = gridModel;
            _gameStateModel = gameStateModel;
            _scoreModel = scoreModel;
            _tetrominoModel = tetrominoModel;
        }

        public override void Execute()
        {
            var gridWidth = _dataService.GridWidth;
            var gridHeight = _dataService.GridHeight;

            _gridModel.Create(gridWidth, gridHeight);
            _gameStateModel.Init();
            _scoreModel.Init();
            _tetrominoModel.Init();

            AudioManager.Instance.PlayFx(SoundType.NEW_GAME);

            Debugger.Log(LogEntryCategory.General, "Game setup was successful!");
        }
    }
}