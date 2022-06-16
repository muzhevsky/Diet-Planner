using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewProductButton : MonoBehaviour
{
    [SerializeField] GameObject AddingWindow;
    public void ShowAddingWindow()
    {
        AddingWindow.SetActive(true);
    }
}
