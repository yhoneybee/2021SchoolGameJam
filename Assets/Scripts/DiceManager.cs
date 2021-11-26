using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public List<DiceView> diceGrid = new List<DiceView>();
    public List<DiceData> deck = new List<DiceData>();
    public Vector2Int cellCount;
    public List<int> posIndex = new List<int>()
    {
        0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SpawnDice();
    }

    public List<DiceView> GetRow(int idx)
    {
        List<DiceView> temp = new List<DiceView>();
        for (int i = idx; i < idx + 5; i++)
            temp.Add(diceGrid[i]);
        return temp;
    }

    public List<DiceView> GetColumn(int idx)
    {
        List<DiceView> temp = new List<DiceView>();
        for (int i = idx; i < 5; i += 5)
            temp.Add(diceGrid[i]);
        return temp;
    }

    public void DiceAllMoveRight()
    {
        for (int i = 5 - 1; i >= 0; i--)
        {
            if (i != 4)
            {
                diceGrid[i].Dice.Combine(diceGrid[i + 1].Dice);
                diceGrid[i + 5].Dice.Combine(diceGrid[i + 5 + 1].Dice);
                diceGrid[i + 10].Dice.Combine(diceGrid[i + 10 + 1].Dice);
                diceGrid[i + 15].Dice.Combine(diceGrid[i + 15 + 1].Dice);
                diceGrid[i + 20].Dice.Combine(diceGrid[i + 20 + 1].Dice);
            }
        }
    }
    public void DiceAllMoveLeft()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i != 0)
            {
                diceGrid[i].Dice.Combine(diceGrid[i - 1].Dice);
                diceGrid[i + 5].Dice.Combine(diceGrid[i + 5 - 1].Dice);
                diceGrid[i + 10].Dice.Combine(diceGrid[i + 10 - 1].Dice);
                diceGrid[i + 15].Dice.Combine(diceGrid[i + 15 - 1].Dice);
                diceGrid[i + 20].Dice.Combine(diceGrid[i + 20 - 1].Dice);
            }
        }
    }
    public void DiceAllMoveDown()
    {
        for (int i = 25 - 1; i >= 0; i -= 5)
        {
            if (i != 4)
            {
                diceGrid[i - 5].Dice.Combine(diceGrid[i].Dice);
                diceGrid[i - 1 - 5].Dice.Combine(diceGrid[i - 1].Dice);
                diceGrid[i - 2 - 5].Dice.Combine(diceGrid[i - 2].Dice);
                diceGrid[i - 3 - 5].Dice.Combine(diceGrid[i - 3].Dice);
                diceGrid[i - 4 - 5].Dice.Combine(diceGrid[i - 4].Dice);
            }
        }
    }
    public void DiceAllMoveUp()
    {
        for (int i = 0; i < 25; i += 5)
        {
            if (i != 0)
            {
                diceGrid[i].Dice.Combine(diceGrid[i - 5].Dice);
                diceGrid[i + 1].Dice.Combine(diceGrid[i + 1 - 5].Dice);
                diceGrid[i + 2].Dice.Combine(diceGrid[i + 2 - 5].Dice);
                diceGrid[i + 3].Dice.Combine(diceGrid[i + 3 - 5].Dice);
                diceGrid[i + 4].Dice.Combine(diceGrid[i + 4 - 5].Dice);
            }
        }
    }


    public void SpawnDice()
    {
        if (posIndex.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, posIndex.Count);
            int idx = posIndex[rand];

            var dice = Instantiate(Resources.Load<Dice>("Dice"), GameObject.Find("Dices").GetComponent<RectTransform>(), false);
            dice.diceData = deck[UnityEngine.Random.Range(0, deck.Count)];

            dice.idx = idx;
            diceGrid[idx].Dice = dice;
            diceGrid[idx].Dice.GetComponent<RectTransform>().position = diceGrid[idx].GetComponent<RectTransform>().position;

            posIndex.RemoveAt(rand);
        }
    }
}
