using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Datas/Skill", order = 0)] // 스킬쓸때 이걸 가져다 쓰는데 ""안에 skill 이라 적혀있는곳에 내가 만들고 싶은
//스킬 쓰기.
public class CombineSkillData : ScriptableObject
{
    public virtual void OnCombine(Dice dice)
    {
    }
}
