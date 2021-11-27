using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BackhoSkill", menuName = "Datas/Backhoskill", order = 0)]
public class Backho : CombineSkillData
{
    public override void OnCombine()
    {
        base.OnCombine();
        foreach (var item in DiceManager.Instance.diceGrid)
        {
            item.Dice.diceData.stat.CP += 5;
        }
    }
    
}