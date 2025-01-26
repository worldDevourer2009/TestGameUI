using System;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class InputManager : IInput, ITickable
    {
        public event Action<Vector3> MousePos;
        private Vector3 _mousePos;
        
        public void Tick()
        {
            _mousePos = Input.mousePosition;
            if (!Input.GetMouseButtonDown(0)) return;
            MousePos?.Invoke(_mousePos);
        }
    }
}