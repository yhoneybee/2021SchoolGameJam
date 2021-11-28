using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtnLinker : MonoBehaviour
{
    public Text txtCount;
    public Text txtLevel;
    public Text txtCost;
    public DiceData diceData;

    private void Update()
    {
        txtCount.text = $"{diceData.Count}";
        txtLevel.text = $"LV.{diceData.stat.Level + 1}";
        txtCost.text = $"{Player.strArrCost[diceData.stat.Level]}";
    }
}
