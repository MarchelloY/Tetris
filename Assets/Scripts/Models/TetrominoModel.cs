using Data;
using Models.api;
using Services.api;
using Signals;
using UnityEngine;
using Views;
using Vector3 = UnityEngine.Vector3;

namespace Models
{
    public class TetrominoModel : ITetrominoModel
    {
        [Inject]
        private readonly IDataService _dataService = null;
        [Inject]
        private readonly IGridModel _gridModel = null;
        [Inject]
        private readonly ChangeLightBulbsColorSignal _changeLightBulbsColorSignal = null;

        private GameObject _previewTetromino;

        public GameObject CurrentTetromino { get; private set; }

        public void Init()
        {
            CurrentTetromino = Object.Instantiate(_dataService.GetRandomTetromino(),
                _dataService.PositionSpawn, Quaternion.identity);

            CreatePreviewTetromino();
        }

        public void NextStep()
        {
            _previewTetromino.transform.localPosition = _dataService.PositionSpawn;
            _previewTetromino.transform.localScale /= _dataService.PreviewScale;

            CurrentTetromino.GetComponent<TetrominoView>().enabled = false;
            CurrentTetromino = _previewTetromino;
            CurrentTetromino.GetComponent<TetrominoView>().enabled = true;

            _changeLightBulbsColorSignal.Dispatch(StringCollection.YELLOW_COLOR);

            CreatePreviewTetromino();
        }

        public bool MoveLeft() => Move(Vector3.left);
        public bool MoveRight() => Move(Vector3.right);
        public bool MoveDown() => Move(Vector3.down);

        public bool Rotate()
        {
            var currentTransform = CurrentTetromino.transform;
            var currentView = currentTransform.GetComponent<TetrominoView>();

            currentTransform.RotateAround(
                currentTransform.TransformPoint(currentView.RotationPosition), Vector3.forward, -90);

            if (_gridModel.CheckIsValidPosition(currentTransform))
            {
                return false;
            }

            currentTransform.RotateAround(
                currentTransform.TransformPoint(currentView.RotationPosition), Vector3.forward, 90);

            return true;
        }

        private bool Move(Vector3 direction)
        {
            var currentTransform = CurrentTetromino.transform;

            currentTransform.position += direction * _dataService.MoveStep;

            if (_gridModel.CheckIsValidPosition(currentTransform))
            {
                return false;
            }

            currentTransform.position -= direction * _dataService.MoveStep;

            return true;
        }

        private void CreatePreviewTetromino()
        {
            var previewObject = _dataService.GetRandomTetromino();
            var previewPosition = previewObject.GetComponent<TetrominoView>().PreviewPosition;

            _previewTetromino = Object.Instantiate(previewObject, previewPosition, Quaternion.identity);
            _previewTetromino.transform.localScale *= _dataService.PreviewScale;
            _previewTetromino.GetComponent<TetrominoView>().enabled = false;
        }
    }
}