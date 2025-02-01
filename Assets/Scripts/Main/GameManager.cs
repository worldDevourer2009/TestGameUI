using SavesManagement;
using Signals;
using UnityEngine;
using Zenject;

namespace Main
{
    public class GameManager : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject gameOverView;
        [SerializeField] private GameObject youWonView;
        private ISaveSystem _saveLoadSystem;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(ISaveSystem saveLoadSystem, SignalBus signalBus)
        {
            _saveLoadSystem = saveLoadSystem;
            _signalBus = signalBus;
            _saveLoadSystem.LoadGameData("New Game");
        }
        
        public void Initialize()
        {
            gameOverView.gameObject.SetActive(false);
            youWonView.gameObject.SetActive(false);
            _signalBus.Subscribe<PlayerDeathSignal>(HandlePlayerDeath);
            _signalBus.Subscribe<EnemyDeathSignal>(HandleEnemyDeath);
        }

        private void HandleEnemyDeath(EnemyDeathSignal evt)
        {
            var isDead = evt.IsDead;
            youWonView.gameObject.SetActive(isDead);
            SaveAllData();
        }

        private void HandlePlayerDeath(PlayerDeathSignal evt)
        {
            var isDead = evt.IsDead;
            gameOverView.gameObject.SetActive(isDead);
        }

        private void SaveAllData()
        {
            Debug.Log("Saving");
            
            if (_saveLoadSystem != null)
                _saveLoadSystem.SaveGameData();
            else
                Debug.Log("Save is not init");
        }
    }
}