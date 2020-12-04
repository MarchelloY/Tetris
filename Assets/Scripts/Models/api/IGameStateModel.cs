using UnityEngine;

namespace Models.api
{
    public interface IGameStateModel
    {
        bool IsGameOver { get; set; }
        bool IsPause { get; set; }
        void GameOver();
        void Init();
        void ShowPausePanel();
        void HidePausePanel();
        void SpawnNextTetromino();
        GameObject NextTetromino { get; set; }
    }
}
