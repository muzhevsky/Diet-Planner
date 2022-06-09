using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderAnswer : Answer
{
    [SerializeField] Gender _genderId;

    public void OnClick()
    {
        _testScreen.SetGenderId(_genderId);
        _testScreen.LoadNextQuestion();
    }
}
