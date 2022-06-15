using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] protected GameObject _content;
    [SerializeField] protected GlobalUiController _uiController;
    [SerializeField] protected GlobalController _controller;
    public virtual void Hide()
    {
        _content.SetActive(false);
    }
    public virtual void Show()
    {
        _content.SetActive(true);
    }
}
