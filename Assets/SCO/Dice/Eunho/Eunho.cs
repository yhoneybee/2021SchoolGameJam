using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eunho : CombineSkillData
{
    public override void OnCombine(Dice dice)
    {
        base.OnCombine(dice);
        foreach(var Stat in DiceManager.Instance.diceGrid)
            Stat.DiceData.stat.CD += 1;
    }
}
