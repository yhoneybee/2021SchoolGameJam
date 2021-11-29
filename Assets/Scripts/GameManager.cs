using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<RectTransform> poss = new List<RectTransform>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    public List<BossData> bossData = new List<BossData>();
    public TextManager scoreText;
    public Scroll scroll;
    public Image Fade;

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
        }
    }
    private int killCount;

    //KillCount´Â get, KillCount = ´Â set
    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            if (EnemyCount <= 0 && killCount == GetRoundEnemy())
            {
                scroll.gameObject.SetActive(true);
            }
            scoreText.ChangeScore();
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
        Fade.DOFade(0, 1);
        Player.spawnCount = 1;
        Player.Money = 100;
        Player.Life = 3;
        StartCoroutine(ESpawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
            Time.timeScale -= 1;
        if (Input.GetKeyDown(KeyCode.RightBracket))
            Time.timeScale += 1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player.Money += 10000;
        }
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
            if (EnemyCount > 0)
            {
                ObjPool.GetEnemy(poss[0].position);
                EnemyCount--;
            }
            yield return wait;
        }
    }
}
