using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GoldhoSkill", menuName = "Datas/GoldhoSkill", order = 0)]
public class Goldho : CombineSkillData
{
    public override void OnCombine()
    {
        base.OnCombine();
        foreach (var item in DiceManager.Instance.diceGrid)
        {
            item.Dice.diceData.stat.AD += 5;
        }
    }
}
