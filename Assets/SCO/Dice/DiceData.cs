using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProportionInfo
{
    [Header("ÀÒÀº Ã¼·Â ºñ·Ê")]
    public bool lostHp;
    [Header("³²Àº Ã¼·Â ºñ·Ê")]
    public bool remainHp;
    [Header("ÃÖ´ë Ã¼·Â ºñ·Ê")]
    public bool maxHp;
}

[CreateAssetMenu(fileName = "DiceData", menuName = "Datas/DiceData", order = 0)]
public class DiceData : ScriptableObject
{
    public Stat stat;
    public ProportionInfo proportionInfo;
    public CombineSkillData combineSkillData;
    public RuntimeAnimatorController animatorController;
    public bool isTargetRand;
}
