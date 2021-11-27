using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BackhoSkill", menuName = "Datas/Backhoskill", order = 0)]
public class Backho : CombineSkillData
{
    private float deltaValueAS = 10;
    private float deltaValueAD = 50;
    private bool isFastAS = true;

    //�������̵�� �����޾Ҵµ� ������ �Ѱ��̴�.
    public override void OnCombine(Dice dice)
    {
        //base�� �θ� ��ü�� ����Ų��.
        base.OnCombine(dice);
        foreach (var item in DiceManager.Instance.diceGrid) 
        {
            item.DiceData.stat.CP += 5;
        }
    }
    public void ChangeStatWhenAttack5()
    {
        //���ݼӵ��� ���� �����϶� ���ݼӵ��� ������ ���ݷ��� ������Ų��
        if(isFastAS)
        {
            foreach (var item in DiceManager.Instance.diceGrid)
            {
                item.Dice.diceData.stat.AS -= deltaValueAS;
                item.Dice.diceData.stat.AD += deltaValueAD;
            }
            
        }
        else
        {
            foreach (var item in DiceManager.Instance.diceGrid)
            {
                item.Dice.diceData.stat.AS += deltaValueAS;
                item.Dice.diceData.stat.AD -= deltaValueAD;
            }
        }


        //isFastAS�� ������ ���� isFastAS�� �����Ѵ�.
        isFastAS = !isFastAS; 
    }
    
}