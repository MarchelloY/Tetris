using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Header("Tetrominoes configs")]
        [SerializeField] private char[] _tetrominoIds = {'I', 'J', 'L', 'S', 'Z', 'O', 'T'};
        [SerializeField] private string _tetrominoPath = "Tetrominoes/";
        [SerializeField] private Vector3 _positionSpawn = new Vector3(4,18,0);
        [SerializeField] private float _previewScale = 1.5f;

        [Header("Score configs")]
        [SerializeField] private int _costOneLevelInLines = 10;

        [Header("Fall delay configs")]
        [SerializeField] private float _fallDelayModifier = 0.1f;
        [SerializeField] private float _fallDelayMin = 0.2f;
        [SerializeField] private float _fallBoostModifier = 0.05f;

        [Header("Move configs")]
        [SerializeField] private float _moveDelay = 0.1f;
        [SerializeField] private float _moveStep = 1f;

        [Header("Grid configs")]
        [SerializeField] private int _gridWidth = 16;
        [SerializeField] private int _gridHeight = 20;

        public int GridWidth => _gridWidth;
        public int GridHeight => _gridHeight;
        public Vector3 PositionSpawn => _positionSpawn;
        public float PreviewScale => _previewScale;
        public int CostOneLevelInLines => _costOneLevelInLines;
        public float FallDelayMin => _fallDelayMin;
        public float FallDelayModifier => _fallDelayModifier;
        public float FallBoostModifier => _fallBoostModifier;
        public float MoveDelay => _moveDelay;
        public float MoveStep => _moveStep;

        public GameObject GetRandomTetromino()
        {
            var tetrominoPath = _tetrominoPath;
            var figuresChar = _tetrominoIds;
            var randomValue = Random.Range(0, figuresChar.Length);

            for (var i = 0; i < figuresChar.Length; i++)
            {
                if (randomValue == i)
                {
                    tetrominoPath += figuresChar[i];
                }
            }

            return Resources.Load<GameObject>(tetrominoPath);
        }
    }
}