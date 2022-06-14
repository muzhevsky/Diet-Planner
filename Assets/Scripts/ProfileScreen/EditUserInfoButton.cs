using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUserInfoButton : MonoBehaviour
{
    [SerializeField] GlobalUiController _uiController;

    public void ShowEditScreen()
    {
        _uiController.ShowScreen(_uiController.EditScreen);
    }
}