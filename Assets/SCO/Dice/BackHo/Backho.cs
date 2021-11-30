using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BackhoSkill", menuName = "Datas/Backhoskill", order = 0)]
public class Backho : CombineSkillData
{
    private float deltaValueAS = 1;
    private float deltaValueAD = 5;
    private bool isFastAS = true;
    private int backhoAttackCount;

    //오버라이드는 물려받았는데 재정의 한것이다.
    public override void OnCombine()
    {
        //base는 부모 개체를 가르킨다.
        base.OnCombine();
        foreach (var item in DiceManager.Instance.diceGrid) 
        {
            item.DiceData.stat.CP += 5;
        }
    }
    public override void OnAttack()
    {
        base.OnAttack();
        backhoAttackCount++;
        if (backhoAttackCount == 5)
        {
            backhoAttackCount = 0;

            if (dice.DiceData.combineSkillData is Backho)
            {
                ((Backho)dice.DiceData.combineSkillData).ChangeStatWhenAttack5();
            }

        }
    }
    public void ChangeStatWhenAttack5()
    {
        //공격속도가 빠른 상태일때 공격속도를 내리고 공격력을 증가시킨다
        if(isFastAS)
        {
            foreach (var item in DiceManager.Instance.diceGrid)
            {
                item.buffStat.AS -= deltaValueAS;
                item.buffStat.AD += deltaValueAD;
            }
            
        }
        else
        {
            foreach (var item in DiceManager.Instance.diceGrid)
            {
                item.buffStat.AS += deltaValueAS;
                item.buffStat.AD -= deltaValueAD;
            }
        }


        //isFastAS가 반전된 값을 isFastAS에 대입한다.
        isFastAS = !isFastAS; 
    }
    
}