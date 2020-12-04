namespace Services.api
{
    public interface ISaveService
    {
        object ReadData<T>();
        void WriteSaveInFile(int highScore, int highLines);
    }
}