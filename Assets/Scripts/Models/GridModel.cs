using Models.api;
using Signals;
using UnityEngine;
using Views;

namespace Models
{
    public class GridModel : IGridModel
    {
        [Inject] public ScoreChangedSignal ScoreChangedSignal { get; set; }

        private int _width;
        private int _height;
        private Transform[,] _grid;
    
        private int _tempLines;

        public void Create(int width, int height)
        {
            _width = width;
            _height = height;
            _grid = new Transform[_width, _height];
        }
    
        public bool CheckIsAboveGrid(TetrominoView tetromino)
        {
            for (var x = 0; x < _width; x++)
            {
                foreach (Transform mino in tetromino.transform)
                {
                    var pos = Round(mino.position);
                    if (pos.y > _height - 2) return true;
                }
            }

            return false;
        }

        public Vector2 Round(Vector2 pos)
        {
            return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        }

        public Transform GetTransformAtGridPosition(Vector2 pos)
        {
            return pos.y > _height - 1 ? null : _grid[(int) pos.x, (int) pos.y];
        }
    
        public bool CheckIsInsideBoard(Vector2 pos)
        {
            return (int) pos.x >= 0 && (int) pos.x < _width && (int) pos.y >= 0 && (int) pos.y < _height;
        }
    
        public void UpdateGrid(TetrominoView tetromino)
        {
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    if (_grid[x, y] == null) continue;
                    
                    if (_grid[x, y].parent == tetromino.transform)
                    {
                        _grid[x, y] = null;
                    }
                }
            }

            foreach (Transform mino in tetromino.transform)
            {
                var pos = Round(mino.position);
                if (pos.y < _height)
                {
                    _grid[(int) pos.x, (int) pos.y] = mino;
                }
            }
        }
    
        public void DeleteRow()
        {
            for (var y = 0; y < _height; y++)
            {
                if (IsFullRowAt(y))
                {
                    DeleteMinoAt(y);
                    MoveAllRowsDown(y + 1);
                    _tempLines++;
                    y--;
                }
            }

            if(_tempLines > 0) 
                ScoreChangedSignal.Dispatch(_tempLines);
        
            _tempLines = 0;
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
            for (int x = 0; x < _width; x++)
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
            for (var y = i; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
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
