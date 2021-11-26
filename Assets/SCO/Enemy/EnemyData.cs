using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName = "Datas/EnemyData", order = 0)]//Ȧ�� ���� �ƴ� ����
public class EnemyData : ScriptableObject
{
    public Stat stat;
    public RuntimeAnimatorController animatorController;
}
