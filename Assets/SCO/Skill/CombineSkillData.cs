using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Datas/Skill", order = 0)] // ��ų���� �̰� ������ ���µ� ""�ȿ� skill �̶� �����ִ°��� ���� ����� ����
//��ų ����.
public class CombineSkillData : ScriptableObject
{
    protected Dice dice;

    public void OnDice(Dice dice) => this.dice = dice;

    public virtual void OnCombine()
    {
    }
    
    public virtual void OnAttack()
    {

    }
}
