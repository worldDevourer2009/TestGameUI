using System.Threading.Tasks;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Main
{
    public class SceneLoadManager : ILoadable, IInitializable
    {
        private readonly SignalBus _signalBus;
        private int _loadedSceneIndex = 1;

        public SceneLoadManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public async void Initialize()
        {
            await LoadScene(1);
        }

        public async Task LoadScene(int index)
        {
            Debug.Log($"Index is {index}");
            var foundedScene = SceneManager.GetSceneByBuildIndex(index);

            if (foundedScene.isLoaded)
            {
                await ReloadScene();
                return;
            }
            
            var op = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

            if (op == null) return;
            _signalBus.Fire(new LoadingCanvasEnableSignal {Enable = true});
            
            while (!op.isDone)
            {
                await Task.Yield();
            }

            _loadedSceneIndex = index;
            
            Debug.Log($"Get active scene {SceneManager.GetActiveScene()}");
            
            var loadedScene = SceneManager.GetSceneByBuildIndex(_loadedSceneIndex);
            
            if (loadedScene.IsValid() && loadedScene.isLoaded)
                SceneManager.SetActiveScene(loadedScene);
        }

        public async Task ReloadScene()
        {
            if (_loadedSceneIndex != 0)
            {
                var sceneToUnload = SceneManager.GetSceneByBuildIndex(_loadedSceneIndex);
                await UnloadScene(sceneToUnload);
            }
            
            var op = SceneManager.LoadSceneAsync(_loadedSceneIndex, LoadSceneMode.Additive);
            if (op == null) return;
            
            _signalBus.Fire(new LoadingCanvasEnableSignal {Enable = true});
            
            
            while (!op.isDone)
            {
                await Task.Yield();
            }

            var loadedScene = SceneManager.GetSceneByBuildIndex(_loadedSceneIndex);
            
            _signalBus.Fire(new LoadingCanvasEnableSignal {Enable = false});
            
            if (loadedScene.IsValid() && loadedScene.isLoaded)
                SceneManager.SetActiveScene(loadedScene);
        }

        public void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private async Task UnloadScene(Scene sceneToUnload)
        {
            _signalBus.Fire(new LoadingCanvasEnableSignal {Enable = true});
            var op =  SceneManager.UnloadSceneAsync(sceneToUnload);

            if (op == null) return;
            
            while (!op.isDone)
            {
                await Task.Yield();
            }
        }
    }
}

namespace Main
{
    public interface ILoadable
    {
        Task LoadScene(int index);
        Task ReloadScene();
        void QuitApplication();
    }
}