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
                stat = value.stat;
                animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = value.animatorController;
                CAttack = StartCoroutine(EAttack());
            }
            else
            {
                DiceEyesCount = 0;
                animator = null;
                StopCoroutine(CAttack);
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
            // ���� ��ġ���� ��ȿ���� Ȯ��
            if (-1 < value.x && value.x < 5 &&
                -1 < value.y && value.y < 5)
            {
                // ��ȿ�� �ֻ������� Ȯ��
                if (DiceData)
                {
                    // ������ ��ġ = value,
                    var target = DiceManager.Instance.diceGrid[value.x, value.y];
                    // ������ ��ġ�� �ֻ����� �ִ��� Ȯ��
                    // �ֻ����� �ִٸ� ������ ������ Ȯ��
                    if (target.DiceData)
                    {
                        // ���ٸ� ���Ĺ�����
                        if (target.DiceEyesCount == DiceEyesCount)
                            DiceManager.Instance.Combine(this, target);
                        // �����̴� ��ġ�� �������⿡ ��ġ ���� ���� ����
                    }
                    // �ֻ����� ���ٸ�
                    else
                    {
                        // ���� ��ġ�� value��ġ�� ���� ������ �ѱ�
                        DiceManager.Instance.diceGrid[value.x, value.y].DiceData = DiceData;
                        DiceManager.Instance.diceGrid[value.x, value.y].DiceEyesCount = DiceEyesCount;
                        // ���� ���� ����
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
    
    //uint�� +�� ����ϴ� ����
    private int backhoAttackCount;
    private int diceEyesCount;
    public bool isMerge;
    public Stat stat;
    public Stat buffStat;

    [SerializeField] private int diceEyesCount;
    private Animator animator;
    [SerializeField] private DiceData diceData;
    [SerializeField] private Vector2Int posIndex;
    private Coroutine CAttack;

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

        if (diceData.proportionInfo.lostHp)
            damage *= losthp;
        if (diceData.proportionInfo.remainHp)
            damage *= remain;
        if (diceData.proportionInfo.maxHp)
            damage *= max;

        if (diceData.stat.CD > 0 && cp < diceData.stat.CP)
            damage *= diceData.stat.CD;

        temp.stat.HP -= damage;
        onAttack();



    }

    IEnumerator EAttack()
    {
        yield return new WaitForSeconds(1 / (stat.AS + buffStat.AS));
        Attack();
    }
}
