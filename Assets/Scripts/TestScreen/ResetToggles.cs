using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetToggles : MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;
    public void OnClick()
    {
        if (GetComponent<Toggle>().isOn)
        {
            foreach(Toggle toggle in toggles)
            {
                toggle.isOn = false;
            }
        }
    }
}
