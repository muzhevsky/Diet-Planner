using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScreenButton : MonoBehaviour
{
    [SerializeField] Screen openingScreen;
    [SerializeField] GlobalUiController uiController;

    public void LoadScreen()
    {
        uiController.ShowScreen(openingScreen);
    }
}
