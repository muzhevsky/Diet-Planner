using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightInputButton : Answer
{
    [SerializeField] InputField _heightInput;

    public void OnClick()
    {
        _testScreen.SetHeight(int.Parse(_heightInput.text));
        _testScreen.LoadNextQuestion();
    }
}
