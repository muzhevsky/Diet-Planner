using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllergenesAnswer : MonoBehaviour
{
    public Toggle Toggle;
    public Allergenes AllergenesId;

    private void Start()
    {
        Toggle = gameObject.GetComponent<Toggle>();
    }
}
