using Models.api;
using Services.api;
using UnityEngine;
using Views;

namespace Models
{
    public class GameStateModel : IGameStateModel
    {
        [Inject] public IDataService DataService { get; set; }
        [Inject] public IAudioModel AudioModel { get; set; }

        private bool IsStarted { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsPause { get; set; }

        private GameObject PausePanel { get; set; }
        private GameObject GameOverPanel { get; set; }
        
        private GameObject _previewTetromino;
        public GameObject NextTetromino { get; set; }
    
        public void GameOver()
        {
            IsGameOver = true;
            GameOverPanel.SetActive(true);
        }

        public void Init()
        {
            PausePanel = GameObject.Find("Pause");
            PausePanel.SetActive(false);
        
            GameOverPanel = GameObject.Find("GameOver");
            GameOverPanel.SetActive(false);
        }

        public void ShowPausePanel()
        {
            IsPause = true;
            PausePanel.SetActive(true);
        }

        public void HidePausePanel()
        {
            IsPause = false;
            PausePanel.SetActive(false);
        }

        public void SpawnNextTetromino()
        {
            if (!IsStarted)
            {
                AudioModel.ShotAudio(DataService.NewGameClip);
                IsStarted = true;
                NextTetromino = Object.Instantiate(
                    Resources.Load<GameObject>(GetRandomTetrominoPath()),
                    DataService.PositionSpawn,
                    Quaternion.identity);
            }
            else
            {
                _previewTetromino.transform.localPosition = DataService.PositionSpawn;
                _previewTetromino.transform.localScale /= DataService.PreviewScale;
                NextTetromino = _previewTetromino;
                NextTetromino.GetComponent<TetrominoView>().enabled = true;
            }

            var previewObj = Resources.Load<GameObject>(GetRandomTetrominoPath());
            var previewPos = previewObj.GetComponent<TetrominoView>().previewPosition;
            _previewTetromino = Object.Instantiate(previewObj, previewPos, Quaternion.identity);
            _previewTetromino.transform.localScale *= DataService.PreviewScale;
            _previewTetromino.GetComponent<TetrominoView>().enabled = false;
        }
        
        private string GetRandomTetrominoPath()
        {
            var tetrominoPath = DataService.TetrominoPath;
            var figuresChar = DataService.TetrominoIds;
            var randomValue = Random.Range(0, figuresChar.Length);
            for (var i = 0; i < figuresChar.Length; i++)
                if (randomValue == i)
                    tetrominoPath += figuresChar[i];
        
            return tetrominoPath;
        }
    }
}
