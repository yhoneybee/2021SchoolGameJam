using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public static class Player
{
    public static int Money
    {
        get { return money; }
        set
        {
            money = value;
            TextManager.Instance.txtMoney.text = $"{money}";
        }
    }
    public static int highWave;
    public static int[] ArrCost = { 100, 200, 400, 700 };
    public static int Life
    {
        get { return life; }
        set
        {
            if (value <= 0)
            {
                GameManager.Instance.Fade.DOFade(1, 1).onComplete = () =>
                {
                    SceneManager.LoadScene("GameOver");
                };
            }
            life = value;

        }
    }
    public static int spawnCount;

    private static int money;
    private static int life;
}
