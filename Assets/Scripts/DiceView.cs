using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DiceView : MonoBehaviour
{
    public Dice Dice
    {
        get { return dice; }
        set
        {
            dice = value;
            if (dice)
            {
                animator.runtimeAnimatorController = dice.diceData.animatorController;
                InvokeRepeating(nameof(Attack), 0, 1 / Dice.diceData.stat.AS);
            }
        }
    }

    private Dice dice;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        Dice.Attack();
    }
}
