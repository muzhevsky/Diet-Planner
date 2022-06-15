using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicScreen : Screen
{
    [SerializeField] Graphic _graphic;
    GraphicInfo _graphicInfo;
    private void Start()
    {
        _graphicInfo = new GraphicInfo();
        _graphicInfo.LastWeights = new int?[6];
        _graphicInfo.LastWeights[0] = 70;
        _graphicInfo.LastWeights[1] = null;
        _graphicInfo.LastWeights[2] = 77;
        _graphicInfo.LastWeights[3] = 56;
        _graphicInfo.LastWeights[4] = null;
        _graphicInfo.LastWeights[5] = 70;
        _graphicInfo.MonthNumber = _controller.Month - 1;
    }
    public override void Show()
    {
        base.Show();
        _graphic.DrawGraph(_graphicInfo);
    }
    public void UpdateLastWeight(int weight)
    {
        _graphicInfo.LastWeights[_graphicInfo.LastWeights.Length-1] = weight;
    }
}
