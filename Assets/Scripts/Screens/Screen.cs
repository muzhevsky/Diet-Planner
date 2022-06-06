using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] protected GameObject _content;

    public void Hide()
    {
        _content.SetActive(false);
    }
    public void Show()
    {
        _content.SetActive(true);
    }
}
