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
        for(int i = 0; i < _graphicInfo.LastWeights[i];i++) _graphicInfo.LastWeights[i] = null;
        _graphicInfo.MonthNumber = GlobalController.Month - 1;
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
