using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName = "Datas/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public Stat Stat;
    public RuntimeAnimatorController AnimatorController;
}
