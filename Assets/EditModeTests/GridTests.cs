using Models;
using Models.api;
using NUnit.Framework;
using UnityEngine;
using Views;

public class GridTests
{
    private IGridModel _gridModel;
    private IGameStateModel _gameStateModel;

    [SetUp]
    public void Setup()
    {
        _gridModel = new GridModel();
        _gameStateModel = new GameStateModel();
    }
    
    [TestCase(11, 11, ExpectedResult = false)]
    [TestCase(10, 10, ExpectedResult = false)]
    [TestCase(9, 9, ExpectedResult = true)]
    [TestCase(1, 1, ExpectedResult = true)]
    [TestCase(0, 0, ExpectedResult = true)]
    [TestCase(-1, -1, ExpectedResult = false)]
    public bool CheckIsInsideBoardTest(int x, int y)
    {
        _gridModel.Create(10, 10);
        return _gridModel.CheckIsInsideBoard(new Vector2(x, y));
    }
    
    [TestCase(11.1f, 11.9f, 11, 12)]
    [TestCase(11.5f, 11.5f, 12, 12)]
    [TestCase(0, 0, 0, 0)]
    [TestCase(-1.4f, -1.6f, -1, -2)]
    public void CheckIsAboveGridTest(float x, float y, int xOut, int yOut)
    {
        var vector = _gridModel.Round(new Vector2(x, y));
        var result = new Vector2(xOut, yOut);
        Assert.AreEqual(vector, result);
    }
    
    //Why 2 vectors? Because these are 2 points that the figures are guaranteed to hit!
    [TestCase(4, 18, 4, 19, ExpectedResult = true)]
    [TestCase(2, 2, 1, 1, ExpectedResult = false)]
    public bool GetTransformAtGridPositionTest(int x1, int y1, int x2, int y2)
    {
        _gridModel.Create(10, 20);
        _gameStateModel.SpawnNextTetromino();
        _gridModel.UpdateGrid(_gameStateModel.NextTetromino.GetComponent<TetrominoView>());
        var transform1 = _gridModel.GetTransformAtGridPosition(new Vector2(x1, y1));
        var transform2 = _gridModel.GetTransformAtGridPosition(new Vector2(x2, y2));
        return transform1 != null || transform2 != null;
    }
    
    [TestCase(10, 19, ExpectedResult = true)]
    [TestCase(10, 20, ExpectedResult = true)]
    [TestCase(10, 21, ExpectedResult = false)]
    [TestCase(10, 22, ExpectedResult = false)]
    public bool CheckIsAboveGridTest(int x, int y)
    {
        _gridModel.Create(x, y);
        _gameStateModel.SpawnNextTetromino();
        var nextTetromino = _gameStateModel.NextTetromino.GetComponent<TetrominoView>();
        _gridModel.UpdateGrid(nextTetromino);
        return _gridModel.CheckIsAboveGrid(nextTetromino);
    }
}
