using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance {  get; private set; }

    public List<List<Dice>> diceGrid = new List<List<Dice>>();

    private void Awake()
    {
        Instance = this;
    }

    public List<Dice> GetRow(int idx) => diceGrid[idx];

    public List<Dice> GetColumn(int idx)
    {
        List<Dice> temp = new List<Dice>();
        diceGrid.ForEach(dices => temp.Add(dices[idx]));
        return temp;
    }
}
