using System;
using System.IO;
using Services.api;
using UnityEngine;

namespace Services
{
    public class SaveService : ISaveService
    {
        private readonly string _path;
    
        public SaveService(string path)
        {
            _path = Path.Combine(Application.persistentDataPath, path);
        }
    
        private void SaveData<T>(T t)
        {
            var jsonData = JsonUtility.ToJson(t);
            if (!File.Exists(_path)) File.Create(_path).Close();
            File.WriteAllText(_path, jsonData);
        }

        public object ReadData<T>()
        {
            if (!File.Exists(_path)) return null;
            var data = File.ReadAllText(_path);
            if (data.Length == 0) return null;
            var t = JsonUtility.FromJson<T>(data);
            return t;
        }

        public void WriteSaveInFile(int currentScore, int currentLines)
        {
            var saveData = (SaveData) ReadData<SaveData>();

            if (saveData == null)
            {
                SaveData(new SaveData {
                    highLines = currentLines,
                    highScore = currentScore
                });
                return;
            }

            if (saveData.highScore < currentScore || saveData.highLines < currentLines) 
                SaveData(new SaveData {
                    highLines = currentLines,
                    highScore = currentScore
                });
        }
    
    }

    [Serializable]
    public class SaveData
    {
        public int highScore;
        public int highLines;
    }
}