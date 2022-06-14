using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicScreen : Screen
{
    [SerializeField] Graphic _graphic;
    [SerializeField] GlobalController _controller;
    GraphicInfo _graphicInfo;
    private void Start()
    {
        _graphicInfo = new GraphicInfo();
    }
    public override void Show()
    {
        base.Show();
        _graphicInfo.LastWeights = _controller.LastWeights;
        _graphicInfo.MonthNumber = _controller.Month - 1;
        _graphic.DrawGraph(_graphicInfo);
    }
}
