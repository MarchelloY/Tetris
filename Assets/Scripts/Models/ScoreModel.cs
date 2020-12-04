using Models.api;
using Services.api;
using Signals;
using UnityEngine;

namespace Models
{
    public class ScoreModel : IScoreModel
    {
        [Inject("Save")] public ISaveService SaveService { get; set; }
        [Inject] public MainUIUpdatedSignal MainUIUpdatedSignal { get; set; }
        [Inject] public IDataService DataService { get; set; }


        private int[] ComboScore { get; set; }
        
        private int _currentScore;
        private int _currentLines;
        private int _currentLevel;

        public float FallTime { get; set; } = 1f;
        
        public void InitCombo()
        {
            ComboScore = new int[4];
            for (var i = 0; i < ComboScore.Length; i++)
                ComboScore[i] = (int) (100 * (Mathf.Pow(2,i + 1) - 1));
        }

        public void Update(int lines)
        {
            UpdateLines(lines);
            UpdateLevels();
            UpdateScore(lines);
            
            SaveService.WriteSaveInFile(_currentScore, _currentLines);
            MainUIUpdatedSignal.Dispatch(new[] {_currentScore, _currentLines, _currentLevel});
        }

        public void BoostScore()
        {
            _currentScore++;
            
            SaveService.WriteSaveInFile(_currentScore, _currentLines);
            MainUIUpdatedSignal.Dispatch(new[] {_currentScore, _currentLines, _currentLevel});
        }

        public void TestScore(int lines)
        {
            InitCombo();
            UpdateScore(lines);
            UpdateLines(lines);
            MainUIUpdatedSignal.Dispatch(new[] {_currentScore, _currentLines, 0});
        }

        private void UpdateScore(int lines)
        {
            for (var i = 0; i < ComboScore.Length; i++)
                if (lines == i + 1)
                    _currentScore += ComboScore[i] + _currentLevel * (i + 1) * 2;
        }

        private void UpdateLines(int lines)
        {
            _currentLines += lines;
        }

        private void UpdateLevels()
        {
            _currentLevel = _currentLines / DataService.CostOneLevelInLines;
            var temp = 1f - _currentLevel * DataService.FallTimeModifier;
            if (temp < DataService.FallTimeMin) temp = DataService.FallTimeMin;
            FallTime = temp;
        }
    }
}
