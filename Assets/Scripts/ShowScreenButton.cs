using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScreenButton : MonoBehaviour
{
    [SerializeField] Screen _openingScreen;
    [SerializeField] GlobalUiController _uiController;
    public void LoadScreen()
    {
        _uiController.ShowScreen(_openingScreen);
    }
}
