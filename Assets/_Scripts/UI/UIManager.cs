using System.Diagnostics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class UIManager : MonoBehaviour
{
    public bool skipIntro = false;

    private UIScreen _currentScreen;
    private UIScreen _previousScreen;

    private List<UIScreen> _screens;

    [SerializeField] private UIScreen _introScreen;
    [SerializeField] private UIScreen _gameScreen;
    [SerializeField] private UIScreen _gameOverScreen;
    [SerializeField] private UIScreen _levelEndShopScreen;

    private void Awake()
    {
        Initialize();
        SubscribeToEvents();

        _introScreen?.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void Start()
    {
        if(skipIntro)
        {
            _currentScreen = _gameScreen;
            _currentScreen.Show();
            GameEvents.GameStarted.Invoke();
        }
        else
        {
            _currentScreen = _introScreen;
            _currentScreen.Show();
        }
    }

    void SubscribeToEvents()
    {
        UIEvents.IntroShown += UIEOnIntroShown;
        UIEvents.IntroClosed += UIEOnIntroClosed;
        UIEvents.LevelEndShopShown += UIEOnLevelEndShopShown;
        UIEvents.LevelEndShopClosed += UIEOnLevelEndShopClosed;
        UIEvents.PauseGame += UIEOnPauseGame;
        UIEvents.ResumeGame += UIEOnResumeGame;
        GameEvents.GameOver += UIEOnGameOver;
        UIEvents.GameOverClosed += UIEOnGameOverClosed;
    }

    private void UIEOnLevelEndShopShown()
    {
        SwitchScreens(_levelEndShopScreen);
    }

    private void UIEOnLevelEndShopClosed()
    {
        SwitchScreens(_gameScreen);
    }

    private void UIEOnIntroShown()
    {
        SwitchScreens(_introScreen);
    }

    private void UIEOnIntroClosed()
    {
        SwitchScreens(_gameScreen);
        GameEvents.GameStarted.Invoke();
    }

    
    private void UIEOnPauseGame()
    {
        throw new NotImplementedException();
    }

    private void UIEOnResumeGame()
    {
        throw new NotImplementedException();
    }

    private void UIEOnGameOver()
    {
        SwitchScreens(_gameOverScreen);
    }

    private void UIEOnGameOverClosed()
    {
        SwitchScreens(_introScreen);
    }

    private void SwitchScreens(UIScreen newScreen)
    {
        _currentScreen?.Hide();
        _previousScreen = _currentScreen;
        _currentScreen = newScreen;
        _currentScreen.Show();
    }

    void UnsubscribeFromEvents()
    {
        UIEvents.IntroShown -= UIEOnIntroShown;
        UIEvents.IntroClosed -= UIEOnIntroClosed;
        UIEvents.LevelEndShopShown -= UIEOnLevelEndShopShown;
        UIEvents.LevelEndShopClosed -= UIEOnLevelEndShopClosed;
        UIEvents.PauseGame -= UIEOnPauseGame;
        UIEvents.ResumeGame -= UIEOnResumeGame;
    }

    private void Initialize()
    {
        RegisterScreens();
        InitializeScreens();
        HideScreens();
    }

    private void InitializeScreens()
    {
        foreach (UIScreen screen in _screens)
        {
            screen?.Initialize();
        }
    }

    private void RegisterScreens()
    {
        _screens = new List<UIScreen> {            
            _introScreen,            
            _gameScreen,            
            _gameOverScreen,
            _levelEndShopScreen
        };
    }

    private void HideScreens()
    {
        foreach (UIScreen screen in _screens)
        {
            screen?.Hide();
        }
    }
}
