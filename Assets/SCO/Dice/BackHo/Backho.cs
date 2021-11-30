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

    //�������̵�� �����޾Ҵµ� ������ �Ѱ��̴�.
    public override void OnCombine()
    {
        //base�� �θ� ��ü�� ����Ų��.
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
        //���ݼӵ��� ���� �����϶� ���ݼӵ��� ������ ���ݷ��� ������Ų��
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


        //isFastAS�� ������ ���� isFastAS�� �����Ѵ�.
        isFastAS = !isFastAS; 
    }
    
}