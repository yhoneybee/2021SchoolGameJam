using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : CombineSkillData
{    
    public override void OnCombine(Dice dice)
    {
        int a = Random.Range(0, 25);
        int x = a % 5;
        int y = a / 5;

        DiceManager.Instance.diceGrid[x, y].DiceData.stat.AS += 5;


        base.OnCombine(dice);
        
    }
}
