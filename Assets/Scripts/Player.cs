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
    public static string[] strArrCost = { "100", "200", "400", "700", "MAX" };
    public static int life;

    private static int money;
}
