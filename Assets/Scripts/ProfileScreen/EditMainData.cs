using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMainData : MonoBehaviour
{
    [SerializeField] GameObject _window;
    public void ShowEditWindow()
    {
        _window.SetActive(true);
    }
}
