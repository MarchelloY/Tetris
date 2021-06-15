namespace Models.api
{
    public interface IGameStateModel
    {
        bool IsGameOver { get; }
        bool IsPause { get; }
        bool IsBoost { get; set; }
        float FallDelay { get; set; }
        void Init();
        void Pause();
        void Continue();
        void GameOver();
    }
}