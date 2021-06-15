using Data;

namespace Services.api
{
    public interface ISaveService
    {
        object ReadData<T>();
        void WriteSaveInFile(GameData gameData);
    }
}