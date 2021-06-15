using Data;
using Models.api;
using strange.extensions.command.impl;

namespace Commands
{
    public class LinesCollectedCommand : Command
    {
        private readonly GameData _gameData;
        private readonly IScoreModel _scoreModel;

        public LinesCollectedCommand(GameData gameData, IScoreModel scoreModel)
        {
            _gameData = gameData;
            _scoreModel = scoreModel;
        }

        public override void Execute()
        {
            var lines = _gameData.Lines;

            _scoreModel.UpdateScoreByLines(lines);
        }
    }
}