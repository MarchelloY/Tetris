using Data;
using Models.api;
using Services.api;
using Signals;
using UnityEngine;
using Utils;

namespace Models
{
    public class ScoreModel : IScoreModel
    {
        private const int MAX_LINES_COUNT = 4;

        [Inject]
        private readonly ISaveService _saveService = null;
        [Inject]
        private readonly MainUIUpdatedSignal _mainUIUpdatedSignal = null;
        [Inject]
        private readonly IDataService _dataService = null;
        [Inject]
        private readonly IGameStateModel _gameStateModel = null;

        private int[] _scoreForClearedRows;
        
        private int _currentScore;
        private int _currentLines;
        private int _currentLevel;

        public void Init()
        {
            _scoreForClearedRows = new int[MAX_LINES_COUNT].GetComboScore();
            _currentLevel = 1;
        }

        public void UpdateScoreByLines(int lines)
        {
            UpdateLines(lines);
            UpdateLevel();
            UpdateScore(lines);
            
            DoDispatchMainUIUpdatedSignal();
        }

        public void UpdateScoreByBoost()
        {
            _currentScore++;
            
            DoDispatchMainUIUpdatedSignal();
        }

        private void UpdateScore(int lines)
        {
            _currentScore += _scoreForClearedRows[lines - 1] + (2 * (lines + 1) * _currentLevel);
        }

        private void UpdateLines(int lines)
        {
            _currentLines += lines;
        }

        private void UpdateLevel()
        {
            _currentLevel = (_currentLines / _dataService.CostOneLevelInLines) + 1;

            UpdateFallDelay();
        }

        private void UpdateFallDelay()
        {
            var temp = Mathf.Max(_dataService.FallDelayMin,
                1f - (_currentLevel - 1) * _dataService.FallDelayModifier);

            if (temp < _dataService.FallDelayMin)
            {
                temp = _dataService.FallDelayMin;
            }

            _gameStateModel.FallDelay = temp;
        }

        private void DoDispatchMainUIUpdatedSignal()
        {
            var gameData = new GameData()
            {
                Score = _currentScore,
                Lines = _currentLines,
                Level = _currentLevel,
            };

            _saveService.WriteSaveInFile(gameData);
            _mainUIUpdatedSignal.Dispatch(gameData);
        }
    }
}