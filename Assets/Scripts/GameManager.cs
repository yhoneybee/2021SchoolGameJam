using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<RectTransform> poss = new List<RectTransform>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    public int RoundCount
    {
        get { return roundCount; }
        set
        {
            roundCount = value;

            enemyCount = 0;

            if (roundCount % 3 == 0)
                EnemyCount += 4;
            if (roundCount % 3 == 1)
                EnemyCount += 8;
            if (roundCount % 3 == 2)
                EnemyCount += 13;

            EnemyCount += 13 * (roundCount / 3);
        }
    }
    public readonly int SMALL = 4;
    public readonly int MIDDLE = 8;
    public readonly int BIG = 13;
    public int EnemyCount
    {
        get { return enemyCount; }
        set
        {
            enemyCount = value;
            if (value <= 0)
                RoundCount++;
        }
    }
    public int killCount;
    public Vector3 down;

    [SerializeField] private int roundCount;
    private int enemyCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RoundCount = 0;
        StartCoroutine(ESpawn());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            down = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            var dir = Input.mousePosition - down;
            down = Vector2.zero;
        }
    }

    IEnumerator ESpawn()
    {
        var wait = new WaitForSeconds(1.07f);
        while (true)
        {
            ObjPool.GetEnemy(poss[0].position);
            EnemyCount--;
            yield return wait;
        }
    }
}
