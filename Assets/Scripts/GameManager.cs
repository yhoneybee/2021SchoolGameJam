using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<RectTransform> poss = new List<RectTransform>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    public TextManager scoreText;
    public int RoundCount
    {
        get { return roundCount; }
        set
        {
            roundCount = value;

            EnemyCount = GetRoundEnemy();
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
            {
                print("라운드 종료");
                RoundCount++;
                KillCount = 0;
            }
        }
    }
    private int killCount;

    //KillCount는 get, KillCount = 는 set
    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            Instance.scoreText.ChangeScore();
        }
    }
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
        Player.life = 3;
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

    public int GetRoundEnemy()
    {
        int temp = 0;

        if (roundCount % 3 == 0)
            temp += 4;
        if (roundCount % 3 == 1)
            temp += 8;
        if (roundCount % 3 == 2)
            temp += 13;

        temp += 13 * (roundCount / 3);

        return temp;
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
