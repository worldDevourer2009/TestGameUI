using System.Collections.Generic;
using System.IO;
using System.Linq;
using SavesManagement;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Services.SavesManagement.SavePersistentDataManager
{
    public class SaveManager : ISaveLoadNewGame
    {
        private readonly ISerializer _serializer;
        private readonly string _path;
        private readonly string _extension;

        public SaveManager(ISerializer serializer)
        {
            _serializer = serializer;
            _path = Application.persistentDataPath;
            _extension = ".json";
            
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
        }

        public GameData Load(string saveName)
        {
            var filePath = GetSavePath(saveName);
            
            Debug.Log($"File name is {saveName} and path is {filePath}");
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Save in {filePath} with name {saveName} doesn't exist");

            var jsonData = File.ReadAllText(filePath);
            return _serializer.Deserialize<GameData>(jsonData);
        }

        public void Save(GameData data, bool overwrite = true)
        {
            var filePath = GetSavePath(data.saveName);

            if (!overwrite && File.Exists(filePath))
                throw new IOException($"Save {data.saveName} already exists");

            var jsonData = _serializer.Serialize(data);
            File.WriteAllText(filePath, jsonData);
        }

        //перенос с другого проекта
        public void Delete(string saveName)
        {
            var filePath = GetSavePath(saveName);
            
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        //перенос с другого проекта
        public void DeleteAll()
        {
            foreach (var filePath in Directory.GetFiles(_path, $"*{_extension}"))
                File.Delete(filePath);
        }

        //перенос с другого проекта
        public IEnumerable<string> ShowAllSaves()
        {
            return Directory.GetFiles(_path, $"*{_extension}")
                .Select(Path.GetFileNameWithoutExtension);
        }
        
        private string GetSavePath(string saveName)
        {
            return Path.Combine(_path, $"{saveName}{_extension}");
        }
    }
}