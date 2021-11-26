using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData
    {
        get => enemyData;
        set
        {
            enemyData = value;
            stat = enemyData.stat;
            //stat.MaxHP = GameManager.Instance.RoundCount * 2022;
            //stat.HP = stat.MaxHP;
            stat.onDie = OnDie;
            animator.runtimeAnimatorController = enemyData.animatorController;
        }
    }
    public Stat stat;
    public Animator animator;
    public int Pos
    {
        get => pos;
        set
        {
            if (value >= GameManager.Instance.poss.Count)
            {
                OnDie();
            }
            else
                pos = value;
        }
    }

    private EnemyData enemyData;
    private int pos;

    private void Start()
    {

    }

    private void Update()
    {
        var targetPos = GameManager.Instance.poss[Pos].anchoredPosition;
        var rtrn = GetComponent<RectTransform>();
        if (Vector3.Distance(rtrn.anchoredPosition, targetPos) < 0.1f)
            Pos++;
        rtrn.anchoredPosition = Vector3.MoveTowards(rtrn.anchoredPosition, targetPos, Time.deltaTime * stat.MS);
    }

    protected void OnDie()
    {
        Pos = 0;
        ObjPool.ReturnEnemy(this);
        GameManager.Instance.killCount++;
    }
}
