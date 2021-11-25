using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private float hp;
}

public abstract class Dice : MonoBehaviour
{
    public List<DiceEye> diceEyes = new List<DiceEye>();
    public Stat Stat;

    private void Start()
    {
        Stat.onDie = () => { };
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
}
