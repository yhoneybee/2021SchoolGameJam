using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<RectTransform> poss = new List<RectTransform>();
    public List<Dice> dices = new List<Dice>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjPool.GetEnemy(poss[0].position);
        }
    }
}
