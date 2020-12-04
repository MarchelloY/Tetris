
using UnityEngine;
using Views;

namespace Models.api
{
    public interface IGridModel
    {
        void Create(int width, int height);
        bool CheckIsAboveGrid(TetrominoView tetromino);
        Vector2 Round(Vector2 pos);
        Transform GetTransformAtGridPosition(Vector2 pos);
        bool CheckIsInsideBoard(Vector2 pos);
        void UpdateGrid(TetrominoView tetromino);
        void DeleteRow();
    }
}