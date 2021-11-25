using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            else if (value == 0)
                onDie();
        }
    }
    public float MaxHP;
    public float AD;
    public float CP;
    public float CD;
    public float MS;

    private float hp;
}

[RequireComponent(typeof(Animator))]
public abstract class Dice : MonoBehaviour
{
    public List<DiceEye> diceEyes = new List<DiceEye>();
    public Stat stat;
    public bool isTargetRand;

    private void Start()
    {
        stat.onDie = () => { };
    }

    // ÇÕÄ¡±â
    public void Combine(Dice combineTo)
    {
        if (CheckCombine(combineTo))
        {
            OnCombine();
        }
    }
    protected abstract void OnCombine();

    public bool CheckCombine(Dice combineTo) => combineTo.diceEyes.Count == diceEyes.Count;

    public void Attack()
    {
        var temp = isTargetRand ? ObjPool.GetRandEnemy() : ObjPool.GetFrontEnemy();
        if (!temp)
        {
            float cp = UnityEngine.Random.Range(0.0f, 100.0f);
            float damage = stat.AD;
            if (cp > stat.CP) damage *= stat.CD;
            temp.stat.HP -= damage;
        }
    }
}
