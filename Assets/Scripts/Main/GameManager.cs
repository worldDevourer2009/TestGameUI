using SavesManagement;
using UnityEngine;
using Zenject;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        private ISaveSystem _saveLoadSystem;

        [Inject]
        public void Construct(ISaveSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
            _saveLoadSystem.LoadGameData("New Game");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                SaveAllData();
        }

        private void SaveAllData()
        {
            Debug.Log("Saving");
            
            if (_saveLoadSystem != null)
                _saveLoadSystem.SaveGameData();
            else
                Debug.Log("SaveLoadSystem is not initialized!");
        }
    }
}