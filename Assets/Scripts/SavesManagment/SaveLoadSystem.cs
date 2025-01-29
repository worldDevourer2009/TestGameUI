using System.Collections.Generic;
using Services.SavesManagement.SavePersistentDataManager;
using UnityEngine;
using Zenject;

namespace SavesManagement
{
    public class SaveLoadSystem : ISaveSystem
    {
        private readonly ISaveLoadNewGame _saveLoadNewGame;
        private List<ISaveable> _saveables;
        
        private GameData _gameData;

        [Inject]
        public SaveLoadSystem(
            ISaveLoadNewGame saveLoadNewGame,
            [Inject(Source = InjectSources.Any)] List<ISaveable> saveables)
        {
            _saveLoadNewGame = saveLoadNewGame;
            _saveables = saveables;
            Debug.Log("Constructor save load system");
        }

        public void NewGame()
        {
            _gameData = new GameData("New Game");
            SaveGameData();
        }

        public void SaveGameData()
        {
            if (_gameData == null)
            {
                Debug.LogError("Can't save, no game data");
                return;
            }
            
            foreach (var saveable in _saveables)
            {
                Debug.Log($"Savebale name {saveable.SaveId}");
            }
            
            foreach (var saveable in _saveables)
            {
                try
                {
                    saveable.SaveData(_gameData);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Eror to save {saveable.GetType()}: {e.Message}");
                }
            }

            _saveLoadNewGame.Save(_gameData);
        }

        public void LoadGameData(string gameName)
        {
            Debug.Log($"Loading data with Name {gameName}");
            gameName = "";
            _gameData = _saveLoadNewGame.Load(gameName);

            if (_gameData == null)
            {
                Debug.Log("null data passed");
                return;
            }
            
            foreach (var saveable in _saveables)
            {
                Debug.Log($"Savebale name {saveable.SaveId}");
            }
            
            foreach (var saveable in _saveables)
            {
                saveable.LoadData(_gameData);
            }
        }
    }
}