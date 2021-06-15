namespace Models.api
{
    public interface IScoreModel
    {
        void Init();
        void UpdateScoreByLines(int lines);
        void UpdateScoreByBoost();
    }
}