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
    public Button btnUpgrade;

    private void Start()
    {
        btnUpgrade.onClick.AddListener(() =>
        {
            SoundManager.Instance.Play("FixPop", SoundType.BUTTON);
            if (diceData.stat.Level < 4)
            {
                if (Player.ArrCost[diceData.stat.Level] <= Player.Money)
                {
                    Player.Money -= Player.ArrCost[diceData.stat.Level];
                    diceData.stat.Level++;
                }
            }
        });
    }

    private void Update()
    {
        txtCount.text = $"{diceData.Count}";
        txtLevel.text = $"LV.{diceData.stat.Level + 1}";
        if (diceData.stat.Level < 4) txtCost.text = $"{Player.ArrCost[diceData.stat.Level]}";
        else txtCost.text = "MAX";
    }
}
