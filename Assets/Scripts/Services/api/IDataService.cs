using UnityEngine;

namespace Services.api
{
    public interface IDataService
    {
        int GridHeight { get; }
        int GridWidth { get; }
        AudioClip DropClip { get; }
        AudioClip MoveClip { get; }
        AudioClip RotateClip { get; }
        AudioClip NewGameClip { get; }
        AudioClip PauseClip { get; }
        AudioClip ResumeClip { get; }
        Vector3 PositionSpawn { get; }
        char[] TetrominoIds { get; }
        string TetrominoPath { get; }
        float PreviewScale { get; }
        int CostOneLevelInLines { get; }
        float FallTimeMin { get; }
        float FallTimeModifier { get; }
    }
}