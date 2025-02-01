using System;
using System.IO;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace SavesManagement
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

            if (Directory.Exists(_path)) return;
            Directory.CreateDirectory(_path);
        }

        public GameData Load(string saveName)
        {
            var filePath = GetSavePath(saveName);
        
            Debug.Log($"trying to load {filePath}");
        
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($" save {saveName} not found path {filePath}");
                return null;
            }

            try
            {
                var jsonData = File.ReadAllText(filePath);
                return _serializer.Deserialize<GameData>(jsonData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Cant load {saveName}: {e}");
                return null;
            }
        }

        public void Save(GameData data, bool overwrite = true)
        {
            var filePath = GetSavePath(data.saveName);

            if (!overwrite && File.Exists(filePath))
                throw new IOException($"Save {data.saveName} already exists");

            var jsonData = _serializer.Serialize(data);
            File.WriteAllText(filePath, jsonData);
        }
        
        private string GetSavePath(string saveName)
        {
            return Path.Combine(_path, $"{saveName}{_extension}");
        }
    }
}