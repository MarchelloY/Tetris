using UnityEngine;

namespace Models.api
{
    public interface ITetrominoModel
    {
        GameObject CurrentTetromino { get; }
        void Init();
        void NextStep();
        bool MoveLeft();
        bool MoveRight();
        bool MoveDown();
        bool Rotate();
    }
}