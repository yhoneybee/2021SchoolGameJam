using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    public EnemyData EnemyData
    {
        get => enemyData;
        set
        {
            enemyData = value;
            stat = enemyData.stat;
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
                pos = 0;
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
        stat.onDie = OnDie;
    }

    private void Update()
    {
        var targetPos = GameManager.Instance.poss[Pos].anchoredPosition;
        var rtrn = GetComponent<RectTransform>();
        if (Vector3.Distance(rtrn.anchoredPosition, targetPos) < 0.1f)
            Pos++;
        rtrn.anchoredPosition = Vector3.MoveTowards(rtrn.anchoredPosition, targetPos, Time.deltaTime * stat.MS);
    }

    protected virtual void OnDie()
    {
        ObjPool.ReturnEnemy(this);
    }
}
