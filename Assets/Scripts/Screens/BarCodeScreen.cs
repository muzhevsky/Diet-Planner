using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCodeScreen : Screen
{
    [SerializeField] BarCodeScanner _scanner;

    public override void Show()
    {
        base.Show();
        _scanner.ClickStart();
    }

    public override void Hide()
    {
        _scanner.ClickStop();
        base.Hide();
    }
}
