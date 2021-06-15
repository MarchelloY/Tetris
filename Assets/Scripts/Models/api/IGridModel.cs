using UnityEngine;

namespace Models.api
{
    public interface IGridModel
    {
        Transform [,] Grid { get; }
        void Create(int width, int height);
        bool CheckIsValidPosition(Transform tetromino);
        bool CheckIsAboveGrid(Transform tetromino);
        void UpdateGrid(Transform tetromino);
        void TryDeleteRows();
    }
}