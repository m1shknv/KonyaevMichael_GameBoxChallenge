using System;

public interface IPopup
{
    void Show();
    void Hide();
    bool IsVisible { get; }

    event Action OnPopupHidden; 
}
