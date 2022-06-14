using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Image _image;


    public void Highlight(bool isOn)
    {
        if (isOn)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
            _text.color = Color.white;
        } 
        else
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
            _text.color = Color.black;
        } 
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
