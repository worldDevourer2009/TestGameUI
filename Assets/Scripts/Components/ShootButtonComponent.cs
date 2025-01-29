using System;
using UiElements;
using UnityEngine;

namespace Components
{
    public class ShootButtonComponent : MonoBehaviour, IClick
    {
        public event Action OnClick;
        
        public void Click()
        {
            throw new NotImplementedException();
        }
    }
}