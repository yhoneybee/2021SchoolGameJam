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

[RequireComponent(typeof(Animator))]
public class Dice : MonoBehaviour
{
    public DiceData DiceData
    {
        get { return diceData; }
        set
        {
            if (value)
            {
                if (!diceData) DiceEyesCount = 1;
                animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = value.animatorController;
                InvokeRepeating(nameof(Attack), 0, 1 / value.stat.AS);
            }
            else
            {
                DiceEyesCount = 0;
                animator = null;
                CancelInvoke(nameof(Attack));
            }
            diceData = value;
        }
    }
    public int DiceEyesCount
    {
        get { return diceEyesCount; }
        set
        {
            diceEyesCount = value;
            txtDiceEyesCount.gameObject.SetActive(diceEyesCount != 0);
            txtDiceEyesCount.text = $"{diceEyesCount}";
        }
    }
    public Vector2Int PosIndex
    {
        get { return posIndex; }
        set
        {
            // 들어온 위치값이 유효한지 확인
            if (-1 < value.x && value.x < 5 &&
                -1 < value.y && value.y < 5)
            {
                // 유효한 주사위인지 확인
                if (DiceData)
                {
                    // 움직일 위치 = value,
                    var target = DiceManager.Instance.diceGrid[value.x, value.y];
                    // 움직일 위치에 주사위가 있는지 확인
                    // 주사위가 있다면 눈금이 같은지 확인
                    if (target.DiceData)
                    {
                        print("if");
                        // 같다면 합쳐버리기
                        if (target.DiceEyesCount == DiceEyesCount)
                            DiceManager.Instance.Combine(this, target);
                        // 움직이는 위치에 합쳐졌기에 위치 정보 갱신 안함
                    }
                    // 주사위가 없다면
                    else
                    {
                        print("else");
                        // 없는 위치인 value위치에 현재 정보를 넘김
                        DiceManager.Instance.diceGrid[value.x, value.y].DiceData = DiceData;
                        DiceManager.Instance.diceGrid[value.x, value.y].DiceEyesCount = DiceEyesCount;
                        // 현재 정보 없음
                        DiceData = null;
                        return;
                    }
                }
                else
                    posIndex = value;
            }
        }
    }
    public Text txtDiceEyesCount;
    public Action onAttack = () => { };
    public bool isMerge;

    [SerializeField] private int diceEyesCount;
    private Animator animator;
    [SerializeField] private DiceData diceData;
    [SerializeField] private Vector2Int posIndex;

    private void Start()
    {
        DiceEyesCount = 0;
    }

    private void Update()
    {

    }

    public void Attack()
    {
        var temp = DiceData.isTargetRand ? ObjPool.GetRandEnemy() : ObjPool.GetFrontEnemy();
        if (temp && temp.gameObject.activeSelf)
        {
            float cp = UnityEngine.Random.Range(0.0f, 100.0f);
            float damage = DiceData.stat.AD;
            int losthp = (int)((temp.stat.MaxHP - temp.stat.HP) / temp.stat.MaxHP * 100);
            int remain = (int)(temp.stat.HP / temp.stat.MaxHP * 100);
            int max = (int)(temp.stat.MaxHP / 100) * 100;

            losthp = losthp == 0 ? 1 : losthp;
            remain = remain == 0 ? 1 : remain;
            max = max == 0 ? 1 : max;

            if (DiceData.proportionInfo.lostHp)
                damage *= losthp;
            if (DiceData.proportionInfo.remainHp)
                damage *= remain;
            if (DiceData.proportionInfo.maxHp)
                damage *= max;

            if (DiceData.stat.CD > 0 && cp < DiceData.stat.CP)
                damage *= DiceData.stat.CD;

            temp.stat.HP -= damage;
            onAttack();
        }
    }
}
