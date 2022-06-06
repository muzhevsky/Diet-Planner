using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUiController : MonoBehaviour
{
    [SerializeField] Screen ActiveScreen;
    public void ShowScreen(Screen openingScreen)
    {
        ActiveScreen?.Hide();
        ActiveScreen = openingScreen;
        ActiveScreen.Show();
    }
}
