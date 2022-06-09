using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    public Text Text;
    public Image Image;

    private void Start()
    {
        Image = GetComponent<Image>();
    }
}
