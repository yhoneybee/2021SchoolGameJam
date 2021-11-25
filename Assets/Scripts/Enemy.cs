using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    public EnemyData EnemyData
    {
        get => enemyData;
        set
        {
            enemyData = value;
            Stat = enemyData.Stat;
            Animator.runtimeAnimatorController = enemyData.AnimatorController;
        }
    }
    public Stat Stat;
    public Animator Animator;

    private EnemyData enemyData;

    private void Start()
    {
        Stat.onDie = OnDie;
    }

    protected virtual void OnDie()
    {
        ObjPool.ReturnEnemy(this);
    }
}
