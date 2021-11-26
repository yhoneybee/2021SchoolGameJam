using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

[Serializable]
public struct Stat
{
    public Action onDie;
    public float HP
    {
        get { return hp; }
        set
        {
            if (0 < value && value <= MaxHP)
                hp = value;
            else if (value <= 0)
                onDie();
        }
    }
    public float MaxHP;
    public float AD;
    public float AS;
    public float CP;
    public float CD;
    public float MS;

    [SerializeField] private float hp;
}

public class Dice : MonoBehaviour
{
    //public List<DiceEye> diceEyes = new List<DiceEye>();
    public DiceData diceData;
    public int DiceEyesCount
    {
        get { return diceEyesCount; }
        set
        {
            diceEyesCount = value;
            txtDiceEyesCount.text = $"{diceEyesCount}";
        }
    }
    public int idx;
    public Text txtDiceEyesCount;

    private int diceEyesCount;

    private void Start()
    {
        //InvokeRepeating(nameof(Attack), 0, 1 / diceData.stat.AS);
    }

    // ÇÕÄ¡±â
    public void Combine(Dice combineTo)
    {
        if (combineTo && CheckCombine(combineTo))
        {
            diceData.combineSkillData.OnCombine();
            combineTo.DiceEyesCount++;
            DiceManager.Instance.posIndex.Add(idx);
            Destroy(gameObject);
        }
    }
    public bool CheckCombine(Dice combineTo) => combineTo.DiceEyesCount == DiceEyesCount;

    public void Attack()
    {
        var temp = diceData.isTargetRand ? ObjPool.GetRandEnemy() : ObjPool.GetFrontEnemy();
        print("F");
        if (temp && temp.gameObject.activeSelf)
        {
            print("G");
            float cp = UnityEngine.Random.Range(0.0f, 100.0f);
            float damage = diceData.stat.AD;
            int losthp = (int)((temp.stat.MaxHP - temp.stat.HP) / temp.stat.MaxHP * 100);
            int remain = (int)(temp.stat.HP / temp.stat.MaxHP * 100);
            int max = (int)(temp.stat.MaxHP / 100) * 100;

            losthp = losthp == 0 ? 1 : losthp;
            remain = remain == 0 ? 1 : remain;
            max = max == 0 ? 1 : max;

            if (diceData.proportionInfo.lostHp)
                damage *= losthp;
            if (diceData.proportionInfo.remainHp)
                damage *= remain;
            if (diceData.proportionInfo.maxHp)
                damage *= max;

            if (diceData.stat.CD > 0 && cp < diceData.stat.CP)
                damage *= diceData.stat.CD;

            temp.stat.HP -= damage;
        }
    }
}
