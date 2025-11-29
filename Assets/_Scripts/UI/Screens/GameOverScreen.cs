using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreen : UIScreen
{
    Button _menuButton;
    public override void Initialize()
    {
        base.Initialize();
    
        _menuButton = _root.Q("MenuButton") as Button;
        _menuButton.clicked += OnMenuClicked;
    }

    private void OnMenuClicked()
    {
        UIEvents.GameOverClosed.Invoke();
        GameEvents.ExitToMenu.Invoke();
    }
}
