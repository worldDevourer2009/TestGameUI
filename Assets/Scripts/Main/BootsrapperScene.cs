using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class BootsrapperScene : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void LoadScene()
        {
            await SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }
}