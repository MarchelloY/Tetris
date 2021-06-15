using UnityEngine;

namespace Services.api
{
    public interface IDataService
    {
        int GridHeight { get; }
        int GridWidth { get; }
        Vector3 PositionSpawn { get; }
        float PreviewScale { get; }
        int CostOneLevelInLines { get; }
        float FallDelayMin { get; }
        float FallDelayModifier { get; }
        float FallBoostModifier { get; }
        float MoveDelay { get; }
        float MoveStep { get; }
        GameObject GetRandomTetromino();
    }
}