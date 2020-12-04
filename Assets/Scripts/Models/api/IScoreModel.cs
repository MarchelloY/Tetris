namespace Models.api
{
    public interface IScoreModel
    {
        void Update(int lines);
        void InitCombo();
        void BoostScore();
        float FallTime { get; set; }
        void TestScore(int lines);
    }
}