using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NomalSkill", menuName = "Datas/NomalSkill", order = 0)]
public class Nomal : CombineSkillData
{
    public override void OnCombine(Dice dice)
    {
        base.OnCombine(dice);
        foreach (var item in DiceManager.Instance.diceGrid)
        {
            item.DiceData.stat.AS += 0.1f;
        }
    }
}
