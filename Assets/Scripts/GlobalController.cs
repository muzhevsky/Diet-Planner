using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    [SerializeField] GlobalUiController uiController;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("user_id"))
        {
            uiController.ShowScreen(uiController.MainScreen);
        }
    }
}
