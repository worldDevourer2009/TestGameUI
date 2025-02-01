using System;
using Main;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class ReloadExitButton : MonoBehaviour, IClick
    {
        [SerializeField] private ButtonExitReloadType buttonType;
        private ILoadable _loadable;
        public event Action OnClick;

        [Inject]
        public void Construct(ILoadable loadable)
        {
            _loadable = loadable;
        }
       
        public void Click()
        {
            switch (buttonType)
            {
                case ButtonExitReloadType.None:
                    break;
                case ButtonExitReloadType.Exit:
                    _loadable.QuitApplication();
                    break;
                case ButtonExitReloadType.Reload:
                    _loadable.ReloadScene();
                    break;
                default:
                    break;
            }
        }
    }
}

namespace UiElements
{
    public enum ButtonExitReloadType
    {
        None,
        Reload,
        Exit
    }
}