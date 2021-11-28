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
    private Vector2 delta;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var item in deck)
        {
            item.stat.Level = 0;
            item.Count = 0;
        }
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                diceGrid[x, y] = rtrnGridParent.GetChild(x + y * 5).GetComponent<Dice>();
                diceGrid[x, y].PosIndex = new Vector2Int(x, y);
            }
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
            DiceMove(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            DiceMove(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            DiceMove(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            DiceMove(Vector2Int.left);
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            delta = touch.deltaPosition;
            print(delta);
        }
        if (Input.touchCount == 0 && delta != Vector2.zero)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);
            if (x > 2 && y > 2)
            {
                if (x > y)
                {
                    if (delta.x > 0)
                    {
                        DiceMove(Vector2Int.right);
                    }
                    else
                    {
                        DiceMove(Vector2Int.left);
                    }
                }
                else
                {
                    if (delta.y > 0)
                    {
                        DiceMove(Vector2Int.up);
                    }
                    else
                    {
                        DiceMove(Vector2Int.down);
                    }
                }
                delta = Vector2.zero;
            }
        }
#endif
    }

    public void DiceMove(Vector2Int dir)
    {
        for (int i = 0; i < 5; i++)
        {
            if (dir == Vector2Int.left || dir == Vector2Int.up)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
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
                for (int y = 5 - 1; y >= 0; y--)
                {
                    for (int x = 5 - 1; x >= 0; x--)
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
                PosIndex.Add(item.PosIndex.x + item.PosIndex.y * 5);
                PosIndex = PosIndex.Distinct().ToList();
            }
            else
            {
                PosIndex.Remove(item.PosIndex.x + item.PosIndex.y * 5);
                item.isMerge = false;
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
            var data = deck[UnityEngine.Random.Range(0, deck.Count)];
            data.Count++;
            diceGrid[idx % 5, idx / 5].DiceData = data;
        }
    }
}
