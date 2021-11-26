using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProportionInfo
{
    [Header("잃은 체력 비례")]
    public bool lostHp;
    [Header("남은 체력 비례")]
    public bool remainHp;
    [Header("최대 체력 비례")]
    public bool maxHp;
}

[CreateAssetMenu(fileName = "DiceData", menuName = "Datas/DiceData", order = 0)]
public class DiceData : ScriptableObject
{
    public Stat stat; // 주사위의 스탯
    public ProportionInfo proportionInfo;
    public CombineSkillData combineSkillData; // 합칠때 스킬 
    public RuntimeAnimatorController animatorController; //애니메이터 컨트롤러
    public bool isTargetRand; //무작위 공격 할껀지 안할껀지
}
