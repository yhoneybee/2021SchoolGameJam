using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Datas/Skill", order = 0)] // ��ų���� �̰� ������ ���µ� ""�ȿ� skill �̶� �����ִ°��� ���� ����� ����
//��ų ����.
public class CombineSkillData : ScriptableObject
{
    public virtual void OnCombine(Dice dice)
    {
    }
}
