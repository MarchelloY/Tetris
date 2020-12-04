using Services.api;
using UnityEngine;

namespace Services
{
    public class DataService : IDataService
    {
        private readonly Data.Data _data;

        public DataService()
        {
            _data = Object.FindObjectOfType<Data.Data>();
        }

        public int GridWidth => _data.gameSettings.GridWidth;
        public int GridHeight => _data.gameSettings.GridHeight;
        public AudioClip DropClip => _data.gameSettings.DropClip;
        public AudioClip MoveClip => _data.gameSettings.MoveClop;
        public AudioClip RotateClip => _data.gameSettings.RotateClip;    
        public AudioClip ResumeClip => _data.gameSettings.ResumeClip;
        public AudioClip PauseClip => _data.gameSettings.PauseClip;
        public AudioClip NewGameClip => _data.gameSettings.NewGameClip;
        public Vector3 PositionSpawn => _data.gameSettings.PositionSpawn;
        public char[] TetrominoIds => _data.gameSettings.TetrominoIds;
        public string TetrominoPath => _data.gameSettings.TetrominoPath;
        public float PreviewScale => _data.gameSettings.PreviewScale;
        public int CostOneLevelInLines => _data.gameSettings.CostOneLevelInLines;
        public float FallTimeMin => _data.gameSettings.FallTimeMin;
        public float FallTimeModifier => _data.gameSettings.FallTimeModifier;
    }
}
