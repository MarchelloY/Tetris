using System.IO;
using Data;
using Services.api;
using UnityEngine;

namespace Services
{
    public class SaveService : ISaveService
    {
        private static readonly string SAVE_PATH = Path.Combine(Application.persistentDataPath, "saves.json");

        public object ReadData<T>()
        {
            if (File.Exists(SAVE_PATH))
            {
                var data = File.ReadAllText(SAVE_PATH);

                if (data.Length != 0)
                {
                    var t = JsonUtility.FromJson<T>(data);

                    return t;
                }
            }

            return null;
        }

        public void WriteSaveInFile(GameData gameData)
        {
            var saveData = (SaveData) ReadData<SaveData>();

            var data = new SaveData
            {
                HighScore = gameData.Score,
                HighLines = gameData.Lines,
            };

            if (saveData == null || (saveData.HighScore < gameData.Score || saveData.HighLines < gameData.Lines))
            {
                SaveData(data);
            }
        }

        private void SaveData<T>(T t)
        {
            var jsonData = JsonUtility.ToJson(t);

            if (!File.Exists(SAVE_PATH))
            {
                File.Create(SAVE_PATH).Close();
            }

            File.WriteAllText(SAVE_PATH, jsonData);
        }
    }
}