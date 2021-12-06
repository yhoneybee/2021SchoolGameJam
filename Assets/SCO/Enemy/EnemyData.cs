using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName = "Datas/EnemyData", order = 0)]//È¦¸® ¸ô¸® ¾Æ´Ï À¸¾Ç
public class EnemyData : ScriptableObject
{
    public Stat stat;
    public RuntimeAnimatorController animatorController;
    public float Multi = 1;
    [Header("ÇöÀç Round(wave)¿¡ °öÇØÁ®¼­ ¾ò¾îÁü")]public int Money;
}
