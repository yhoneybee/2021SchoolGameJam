using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public Dice[,] diceGrid = new Dice[5, 5];
    public List<DiceData> deck = new List<DiceData>();
    public RectTransform rtrnGridParent;
    public List<int> PosIndex = new List<int>()
    {
        0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24
    };
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                diceGrid[x, y] = rtrnGridParent.GetChild(x + y * 5).GetComponent<Dice>();
            }
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
        for (int i = 0; i < 5; i++)
        {
            if (dir == Vector2Int.left || dir == Vector2Int.up)
            {
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (diceGrid[x, y].DiceData)
                        {
                            diceGrid[x, y].PosIndex = new Vector2Int(diceGrid[x, y].PosIndex.x + dir.x, diceGrid[x, y].PosIndex.y - dir.y);
                        }
                    }
                }
            }
            else if (dir == Vector2Int.right || dir == Vector2Int.down)
            {
                for (int x = 5 - 1; x >= 0; x--)
                {
                    for (int y = 5 - 1; y >= 0; y--)
                    {
                        if (diceGrid[x, y].DiceData)
                        {
                            diceGrid[x, y].PosIndex = new Vector2Int(diceGrid[x, y].PosIndex.x + dir.x, diceGrid[x, y].PosIndex.y - dir.y);
                        }
                    }
                }
            }

        }

        foreach (var item in diceGrid)
        {
            if (!item.DiceData)
            {
                PosIndex.Remove(item.PosIndex.x + item.PosIndex.y * 5);
                PosIndex.Add(item.PosIndex.x + item.PosIndex.y * 5);
                print($"pos({item.PosIndex.x + item.PosIndex.y * 5}) : {item.PosIndex.x}, {item.PosIndex.y}");
            }
        }
    }

    public void Combine(Dice from, Dice to)
    {
        if (!to.isMerge)
        {
            to.DiceEyesCount++;
            to.DiceData = deck[UnityEngine.Random.Range(0, deck.Count)];
            to.isMerge = true;
            from.DiceData = null;
        }
    }

    public void SpawnDice()
    {
        if (PosIndex.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, PosIndex.Count);
            var idx = PosIndex[rand];
            PosIndex.RemoveAt(rand);

            var pos = new Vector2Int(idx % 5, idx / 5);
            diceGrid[idx % 5, idx / 5].PosIndex = pos;
            diceGrid[idx % 5, idx / 5].DiceData = deck[UnityEngine.Random.Range(0, deck.Count)];

            print($"pos : {diceGrid[idx % 5, idx / 5].PosIndex} / idx({idx}) = {idx % 5} + {idx / 5}");
        }
    }
}
