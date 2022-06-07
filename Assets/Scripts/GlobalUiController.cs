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
    public Screen ActiveScreen;
    private void Start()
    {
        ShowScreen(LoginScreen);
    }
    public void ShowScreen(Screen openingScreen)
    {
        ActiveScreen?.Hide();
        ActiveScreen = openingScreen;
        ActiveScreen.Show();
    }
}
