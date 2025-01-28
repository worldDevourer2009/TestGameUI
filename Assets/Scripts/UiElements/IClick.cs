using System;

namespace UiElements
{
    public interface IClick
    {
        event Action OnClick;
        void Click();
    }
}