using Data;
using Services.api;
using UnityEngine;

namespace Services
{
    public class DataService : IDataService
    {
        private readonly SettingsProvider _provider;

        public DataService()
        {
            _provider = Object.FindObjectOfType<SettingsProvider>();
        }

        public int GridWidth => _provider.gameSettings.GridWidth;
        public int GridHeight => _provider.gameSettings.GridHeight;
        public Vector3 PositionSpawn => _provider.gameSettings.PositionSpawn;
        public float PreviewScale => _provider.gameSettings.PreviewScale;
        public int CostOneLevelInLines => _provider.gameSettings.CostOneLevelInLines;
        public float FallDelayMin => _provider.gameSettings.FallDelayMin;
        public float FallDelayModifier => _provider.gameSettings.FallDelayModifier;
        public float FallBoostModifier => _provider.gameSettings.FallBoostModifier;
        public float MoveDelay => _provider.gameSettings.MoveDelay;
        public float MoveStep => _provider.gameSettings.MoveStep;
        public GameObject GetRandomTetromino() => _provider.gameSettings.GetRandomTetromino();
    }
}