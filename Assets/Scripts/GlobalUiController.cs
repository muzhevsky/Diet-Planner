using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUiController : MonoBehaviour
{
    [Header("Screens")] 
    public RegistrationScreen RegistrationScreen;
    public LoginScreen LoginScreen;
    public MainScreen MainScreen;
    public TestScreen TestScreen;
    public ProfileScreen ProfileScreen;
    public EditAdditionalDataScreen EditScreen;
    public GraphicScreen GraphicScreen;
    public DietChoosingScreen DietChoosingScreen;
    public Screen ActiveScreen;

    Screen _prevScreen;
    private void Start()
    {
        ShowScreen(LoginScreen);
    }
    public void ShowScreen(Screen openingScreen)
    {
        ActiveScreen?.Hide();
        _prevScreen = ActiveScreen;
        ActiveScreen = openingScreen;
        ActiveScreen.Show();
    }
    public void ShowPreviousScreen()
    {
        ActiveScreen?.Hide();
        ActiveScreen = _prevScreen;
        ActiveScreen.Show();
    }
}
