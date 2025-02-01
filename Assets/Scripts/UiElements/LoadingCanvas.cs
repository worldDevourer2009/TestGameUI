using Signals;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class LoadingCanvas : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject loadingCanvas;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<LoadingCanvasEnableSignal>(HandleCanvas);
        }

        private void HandleCanvas(LoadingCanvasEnableSignal evt)
        {
            var enable = evt.Enable;
            loadingCanvas.gameObject.SetActive(enable);
        }
    }
}