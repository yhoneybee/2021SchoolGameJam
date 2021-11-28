using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static int life;
    public static int spawnCount;

    private static int money;
}
