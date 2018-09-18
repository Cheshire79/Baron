using System;

namespace Baron.View
{
    public interface IView
    {
        event Action BackButtonClicked;
        void Show();
        void Hide();
        void Show(int zIndex);
        void Close();
        void Invalidate();
        bool IsShown();
    }
}
