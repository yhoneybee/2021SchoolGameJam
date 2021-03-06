using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;

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
            else if (MaxHP < value)
                hp = MaxHP;
            else if (value <= 0)
                onDie();
        }
    }
    public float MaxHP;
    public float AD
    {
        get => ad;
        set
        {
            if (value > 0)
                ad = value;
        }
    }
    public float AS
    {
        get => ats;
        set
        {
            if (value > 0)
                ats = value;
        }
    }
    public float CP;
    public float CD;
    public float MS;
    public int Level;

    [SerializeField] private float hp;
    [SerializeField] private float ad;
    [SerializeField] private float ats;

    public void Assign(Stat stat)
    {
        hp = stat.HP;
        MaxHP = stat.MaxHP;
        AD = stat.AD;
        AS = stat.AS;
        CP = stat.CP;
        CD = stat.CD;
        MS = stat.MS;
    }

    public static Stat operator +(Stat op1, Stat op2)
    {
        return new Stat
        {
            hp = op1.HP + op2.HP,
            MaxHP = op1.MaxHP + op2.MaxHP,
            AD = op1.AD + op2.AD,
            AS = op1.AS + op2.AS,
            CP = op1.CP + op2.CP,
            CD = op1.CD + op2.CD,
            MS = op1.MS + op2.MS,
            onDie = op1.onDie,
        };
    }
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
                if (value.combineSkillData) value.combineSkillData.OnDice(this);
                stat.Assign(value.stat);
                stat += buffStat;
                animator = GetComponent<Animator>();
                GetComponent<Image>().DOFade(1, 1);
                animator.runtimeAnimatorController = value.animatorController;
                CAttack = StartCoroutine(EAttack());
            }
            else
            {
                GetComponent<Image>().DOFade(0, 1);
                StopCoroutine(CAttack);
                DiceEyesCount = 0;
                animator = null;
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
            for (int i = 0; i < 8; i++)
            {
                imgHanjaHos[i].gameObject.SetActive(diceEyesCount >= i + 1);
            }
            //txtDiceEyesCount.gameObject.SetActive(diceEyesCount != 0);
            //txtDiceEyesCount.text = $"{diceEyesCount}";
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
                        // 같다면 합쳐버리기
                        if (target.DiceEyesCount == DiceEyesCount && target.DiceEyesCount < 9 && DiceEyesCount < 9)
                            DiceManager.Instance.Combine(this, target);
                        // 움직이는 위치에 합쳐졌기에 위치 정보 갱신 안함
                    }
                    // 주사위가 없다면
                    else
                    {
                        // 없는 위치인 value위치에 현재 정보를 넘김
                        DiceManager.Instance.diceGrid[value.x, value.y].DiceData = DiceData;
                        DiceManager.Instance.diceGrid[value.x, value.y].DiceEyesCount = DiceEyesCount;
                        // 현재 정보 없음
                        DiceData = null;
                    }
                }
                else
                    posIndex = value;
            }
        }
    }
    public Action onAttack = () => { };


    public bool isMerge;
    public Stat stat;
    public Stat buffStat;
    public List<Image> imgHanjaHos;

    [SerializeField] private int diceEyesCount;
    private Animator animator;
    [SerializeField] private DiceData diceData;
    [SerializeField] private Vector2Int posIndex;
    private Coroutine CAttack;

    private void Start()
    {
        DiceEyesCount = 0;
        stat.onDie = () => { };
        buffStat.onDie = () => { };
    }

    private void Update()
    {

    }

    public void Attack()
    {
        var temp = DiceData.isTargetRand ? ObjPool.GetRandEnemy() : ObjPool.GetFrontEnemy();
        if (!temp || !temp.gameObject.activeSelf) return;

        float cp = UnityEngine.Random.Range(0.0f, 100.0f);
        float damage = stat.AD + buffStat.AD * ((diceData.stat.Level + 1) * 2);
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

        if ((stat.CD + buffStat.CD) > 0 && cp < (stat.CP + buffStat.CP))
            damage *= stat.CD + buffStat.CD;

        temp.stat.HP -= damage;
        animator.SetTrigger("isAttack");
        SoundManager.Instance.Play("Attack", SoundType.EFFECT);
        if (DiceData.combineSkillData) DiceData.combineSkillData.OnAttack();
    }

    IEnumerator EAttack()
    {
        while (true)
        {
            float attackSpeed = 1 / (stat.AS + buffStat.AS);
            if (DiceData) attackSpeed += ((diceData.stat.Level + 1) * 0.2f);
            yield return new WaitForSeconds(attackSpeed);
            if (DiceData) Attack();
        }
    }
}
