using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    //public List<DiceView> diceGrid = new List<DiceView>();
    public DiceView[,] diceGrid = new DiceView[5, 5];
    public List<DiceData> deck = new List<DiceData>();
    public List<int> posIndex = new List<int>();
    public RectTransform rtrnGridParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var diceView = rtrnGridParent.GetComponentsInChildren<DiceView>();

        for (int i = 0; i < 25; i++)
        {
            posIndex.Add(i);
            diceGrid[i % 5, i / 5] = diceView[i];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            DiceMove(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            DiceMove(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            DiceMove(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            DiceMove(Vector2Int.left);
    }

    public void DiceMove(Vector2Int dir)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                int ay = Mathf.Abs(y - 4);
                int ax = Mathf.Abs(x - 4);
                if (dir == Vector2Int.up)
                {
                    if (ay != 4)
                    {
                        diceGrid[x, ay].Dice.Combine(diceGrid[x, ay - 1].Dice);
                    }
                }
                else if (dir == Vector2Int.down)
                {
                    if (y != 4)
                    {
                        diceGrid[x, y].Dice.Combine(diceGrid[x, y + 1].Dice);
                    }
                }
                else if (dir == Vector2Int.right)
                {
                    if (x != 4)
                    {
                        diceGrid[x, y].Dice.Combine(diceGrid[x + 1, y].Dice);
                    }
                }
                else if (dir == Vector2Int.left)
                {
                    if (ax != 4)
                    {
                        diceGrid[x, ax].Dice.Combine(diceGrid[ax, y].Dice);
                    }
                }
            }
        }
    }

    //public void DiceAllMoveRight()
    //{
    //    for (int i = 5 - 1; i >= 0; i--)
    //    {
    //        if (i != 4)
    //        {
    //            diceGrid[i].Dice.Combine(diceGrid[i + 1].Dice);
    //            diceGrid[i + 5].Dice.Combine(diceGrid[i + 5 + 1].Dice);
    //            diceGrid[i + 10].Dice.Combine(diceGrid[i + 10 + 1].Dice);
    //            diceGrid[i + 15].Dice.Combine(diceGrid[i + 15 + 1].Dice);
    //            diceGrid[i + 20].Dice.Combine(diceGrid[i + 20 + 1].Dice);
    //        }
    //    }
    //}
    //public void DiceAllMoveLeft()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (i != 0)
    //        {
    //            diceGrid[i].Dice.Combine(diceGrid[i - 1].Dice);
    //            diceGrid[i + 5].Dice.Combine(diceGrid[i + 5 - 1].Dice);
    //            diceGrid[i + 10].Dice.Combine(diceGrid[i + 10 - 1].Dice);
    //            diceGrid[i + 15].Dice.Combine(diceGrid[i + 15 - 1].Dice);
    //            diceGrid[i + 20].Dice.Combine(diceGrid[i + 20 - 1].Dice);
    //        }
    //    }
    //}
    //public void DiceAllMoveDown()
    //{
    //    for (int i = 25 - 1; i >= 0; i -= 5)
    //    {
    //        if (i != 4)
    //        {
    //            diceGrid[i - 5].Dice.Combine(diceGrid[i].Dice);
    //            diceGrid[i - 1 - 5].Dice.Combine(diceGrid[i - 1].Dice);
    //            diceGrid[i - 2 - 5].Dice.Combine(diceGrid[i - 2].Dice);
    //            diceGrid[i - 3 - 5].Dice.Combine(diceGrid[i - 3].Dice);
    //            diceGrid[i - 4 - 5].Dice.Combine(diceGrid[i - 4].Dice);
    //        }
    //    }
    //}
    //public void DiceAllMoveUp()
    //{
    //    for (int i = 0; i < 25; i += 5)
    //    {
    //        if (i != 0)
    //        {
    //            diceGrid[i].Dice.Combine(diceGrid[i - 5].Dice);
    //            diceGrid[i + 1].Dice.Combine(diceGrid[i + 1 - 5].Dice);
    //            diceGrid[i + 2].Dice.Combine(diceGrid[i + 2 - 5].Dice);
    //            diceGrid[i + 3].Dice.Combine(diceGrid[i + 3 - 5].Dice);
    //            diceGrid[i + 4].Dice.Combine(diceGrid[i + 4 - 5].Dice);
    //        }
    //    }
    //}


    public void SpawnDice()
    {
        if (posIndex.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, posIndex.Count);
            var idx = posIndex[rand];
            posIndex.RemoveAt(rand);

            var dice = Instantiate(Resources.Load<Dice>("Dice"), GameObject.Find("Dices").GetComponent<RectTransform>(), false);
            dice.diceData = deck[UnityEngine.Random.Range(0, deck.Count)];

            dice.idx = new Vector2Int(idx % 5, idx / 5);

            print($"idx({idx}) = {idx % 5} + {idx / 5}");

            diceGrid[idx % 5, idx / 5].Dice = dice;
            diceGrid[idx % 5, idx / 5].Dice.GetComponent<RectTransform>().position = diceGrid[idx % 5, idx / 5].GetComponent<RectTransform>().position;
        }
    }
}
