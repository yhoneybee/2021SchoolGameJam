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

    private List<int> posIndex = new List<int>()
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

    public void SpawnDice()
    {
        if (posIndex.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, posIndex.Count);
            int idx = posIndex[rand];

            var dice = Instantiate(Resources.Load<Dice>("Dice"));
            dice.diceData = deck[UnityEngine.Random.Range(0, deck.Count)];

            diceGrid[idx].Dice = dice;
            diceGrid[idx].Dice.GetComponent<RectTransform>().anchoredPosition = diceGrid[idx].GetComponent<RectTransform>().anchoredPosition;

            diceGrid[idx].GetComponent<Image>().color = Color.red;

            posIndex.RemoveAt(rand);
        }
    }
}
