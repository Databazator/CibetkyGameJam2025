using UnityEngine;
using UnityEngine.UIElements;

public class IntroScreen : UIScreen
{
    Button _playButton;
    public override void Initialize()
    {
        base.Initialize();

        _playButton = _root.Q("PlayButton") as Button;
        _playButton.clicked += OnPlayClicked;
    }

    private void OnPlayClicked()
    {
        Debug.Log("Play button clicked");
        UIEvents.IntroClosed?.Invoke();
    }
}
