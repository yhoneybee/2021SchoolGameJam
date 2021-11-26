using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProportionInfo
{
    [Header("���� ü�� ���")]
    public bool lostHp;
    [Header("���� ü�� ���")]
    public bool remainHp;
    [Header("�ִ� ü�� ���")]
    public bool maxHp;
}

[CreateAssetMenu(fileName = "DiceData", menuName = "Datas/DiceData", order = 0)]
public class DiceData : ScriptableObject
{
    public Stat stat; // �ֻ����� ����
    public ProportionInfo proportionInfo;
    public CombineSkillData combineSkillData; // ��ĥ�� ��ų 
    public RuntimeAnimatorController animatorController; //�ִϸ����� ��Ʈ�ѷ�
    public bool isTargetRand; //������ ���� �Ҳ��� ���Ҳ���
}
