using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Header("Tetrominoes configs")]
        [SerializeField] private char[] tetrominoIds = {'I', 'J', 'L', 'S', 'Z', 'O', 'T'};
        [SerializeField] private string tetrominoPath = "Tetrominoes/";
        [SerializeField] private Vector3 positionSpawn = new Vector3(4,18,0);
        [SerializeField] private float previewScale = 1.5f;
        
        [Header("Score configs")]
        [SerializeField] private int costOneLevelInLines = 5;
        [SerializeField] private float fallTimeModifier = 0.01f;
        [SerializeField] private float fallTimeMin = 0.2f;

        [Header("Grid configs")]
        [SerializeField] private int gridWidth = 10;
        [SerializeField] private int gridHeight = 20;

        [Header("AudioClips")]
        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioClip moveClop;
        [SerializeField] private AudioClip rotateClip;
        [SerializeField] private AudioClip newGameClip;
        [SerializeField] private AudioClip pauseClip;
        [SerializeField] private AudioClip resumeClip;
        
        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        public AudioClip DropClip => dropClip;
        public AudioClip MoveClop => moveClop;
        public AudioClip RotateClip => rotateClip;
        public AudioClip NewGameClip => newGameClip;
        public AudioClip PauseClip => pauseClip;
        public AudioClip ResumeClip => resumeClip;
        public Vector3 PositionSpawn => positionSpawn;
        public char[] TetrominoIds => tetrominoIds;
        public string TetrominoPath => tetrominoPath;
        public float PreviewScale => previewScale;
        public int CostOneLevelInLines => costOneLevelInLines;
        public float FallTimeMin => fallTimeMin;
        public float FallTimeModifier => fallTimeModifier;
    }
}
