namespace Models.api
{
    public interface ITetrominoModel
    {
        void MoveLeft();
        void MoveRight();
        void Rotate();
        void Update();
        bool IsBoost { get; set; }
    }
}