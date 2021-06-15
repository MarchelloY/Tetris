using Data;
using Models.api;
using Signals;
using UnityEngine;

namespace Models
{
    public class GridModel : IGridModel
    {
        [Inject]
        private readonly LinesCollectedSignal _linesCollectedSignal = null;
        [Inject]
        private readonly ChangeLightBulbsColorSignal _changeLightBulbsColorSignal = null;

        private int _width;
        private int _height;
        private Transform[,] _grid;

        public Transform[,] Grid => _grid;

        public void Create(int width, int height)
        {
            _width = width;
            _height = height;
            _grid = new Transform[_width, _height];

            for (var y = _height - 3; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (x < 5 || x > 10)
                    {
                        _grid[x, y] = new GameObject().transform;
                    }
                }
            }
        }

        public bool CheckIsValidPosition(Transform tetromino)
        {
            foreach (Transform mino in tetromino)
            {
                var pos = Round(mino.position);

                if (!CheckIsInsideBoard(pos))
                {
                    return false;
                }

                if (GetTransformAtGridPosition(pos) != null &&
                    GetTransformAtGridPosition(pos).parent != tetromino)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckIsAboveGrid(Transform tetromino)
        {
            for (var x = 0; x < _width; x++)
            {
                foreach (Transform mino in tetromino)
                {
                    var pos = Round(mino.position);

                    if (pos.y > _height - 4)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void UpdateGrid(Transform tetromino)
        {
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (_grid[x, y] == null)
                    {
                        continue;
                    }
                    
                    if (_grid[x, y].parent == tetromino)
                    {
                        _grid[x, y] = null;
                    }
                }
            }

            foreach (Transform mino in tetromino)
            {
                var pos = Round(mino.position);

                _grid[(int) pos.x, (int) pos.y] = mino;

                _changeLightBulbsColorSignal.Dispatch(StringCollection.PURPLE_COLOR);
            }
        }
    
        public void TryDeleteRows()
        {
            var tempLines = 0;

            for (var y = 0; y < _height; y++)
            {
                if (IsFullRowAt(y))
                {
                    DeleteMinoAt(y);
                    MoveAllRowsDown(y + 1);
                    tempLines++;
                    y--;
                }
            }

            if (tempLines > 0)
            {
                var gameData = new GameData()
                {
                    Lines = tempLines,
                };

                _linesCollectedSignal.Dispatch(gameData);
            }
        }

        private Vector2 Round(Vector2 pos)
        {
            return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        }

        private Transform GetTransformAtGridPosition(Vector2 pos)
        {
            return pos.y > _height - 1 ? null : _grid[(int) pos.x, (int) pos.y];
        }

        private bool CheckIsInsideBoard(Vector2 pos)
        {
            return (int) pos.x >= 0 && (int) pos.x < _width && (int) pos.y >= 0 && (int) pos.y < _height;
        }
    
        private void DeleteMinoAt(int y)
        {
            for (var x = 0; x < _width; x++)
            {
                Object.Destroy(_grid[x,y].gameObject);
                _grid[x, y] = null;
            }
        }
    
        private bool IsFullRowAt(int y)
        {
            for (var x = 0; x < _width; x++)
            {
                if (_grid[x, y] == null)
                {
                    return false;
                }
            }

            return true;
        }

        private void MoveAllRowsDown(int i)
        {
            for (var y = i; y < _height - 3; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (_grid[x, y] != null)
                    {
                        _grid[x, y - 1] = _grid[x, y];
                        _grid[x, y] = null;
                        _grid[x, y - 1].position += Vector3.down;
                    }
                }  
            }
        }
    }
}