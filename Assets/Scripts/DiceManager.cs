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
        // x,y x,y x,y x,y x,y
        // 0,0 1,0 2,0 3,0 4,0
        // 0,1 1,1 2,1 3,1 4,1
        // 0,2 1,2 2,2 3,2 4,2
        // 0,3 1,3 2,3 3,3 4,3
        // 0,4 1,4 2,4 3,4 4,4
        if (dir == Vector2Int.left)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 1; x < 5; x++)
                {
                    if (diceGrid[x, y].Dice)
                    {
                        if (diceGrid[x - 1, y].Dice)
                        {
                            diceGrid[x, y].Dice.Combine(diceGrid[x - 1, y].Dice);
                        }
                        else
                        {
                            diceGrid[x - 1, y].Dice = diceGrid[x, y].Dice;

                            diceGrid[x, y].Dice.GetComponent<RectTransform>().position = diceGrid[x - 1, y].GetComponent<RectTransform>().position;
                            diceGrid[x, y].Dice = null;
                        }
                    }
                }
            }
        }
    }

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
            dice.GetComponent<RectTransform>().position = diceGrid[idx % 5, idx / 5].GetComponent<RectTransform>().position;
        }
    }
}
