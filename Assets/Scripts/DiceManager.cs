using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance {  get; private set; }

    public List<List<Dice>> diceGrid = new List<List<Dice>>();
    public Vector2Int cellCount;

    private List<int> posIndex = new List<int>()
    {
        0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24
    };

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

    public void SpawnDice()
    {

    }
}
