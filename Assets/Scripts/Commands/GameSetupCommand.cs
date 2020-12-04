using Models.api;
using Services.api;
using strange.extensions.command.impl;

namespace Commands
{
    public class GameSetupCommand : Command
    {
        [Inject] public IDataService DataService { get; set; }
        [Inject] public IGridModel GridModel { get; set; }
        [Inject] public IGameStateModel GameStateModel { get; set; }
        [Inject] public IScoreModel ScoreModel { get; set; }
        [Inject] public IAudioModel AudioModel { get; set; }

        public override void Execute()
        {
            var gridWidth = DataService.GridWidth;
            var gridHeight = DataService.GridHeight;
            GridModel.Create(gridWidth, gridHeight);

            GameStateModel.Init();
            ScoreModel.InitCombo();
            AudioModel.Init();
            
            GameStateModel.SpawnNextTetromino();
        }
    }
}