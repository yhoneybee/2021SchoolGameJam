using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public static class ObjPool
{
    private static List<Enemy> enemies = new List<Enemy>();
    private static List<Enemy> bossEnemies = new List<Enemy>();
    private static List<Enemy> activeEnemies = new List<Enemy>();
    public static List<Enemy> ActiveEnemies => activeEnemies;

    public static float CurHp
    {
        get => curHp;
        set => curHp = value;
    }
    public static float AddHp
    {
        get => addHp;
        set => addHp = value;
    }
    private static float curHp = 126.375f;
    private static float addHp = 126.375f;

    public static int SpawnCount
    {
        get => spawnCount;
        set
        {
            spawnCount = value;
            if (spawnCount % 10 == 0)
            {
                curHp += addHp;
            }
        }
    }
    private static int spawnCount = 0;

    public static Enemy GetFrontEnemy() => activeEnemies.Count > 0 ? (activeEnemies[0].gameObject.activeSelf ? activeEnemies[0] : null) : null;
    public static Enemy GetRandEnemy()
    {
        var temp = activeEnemies.Where(x => x.gameObject.activeSelf);
        return temp.ElementAt(Random.Range(0, temp.Count()));
    }

    public static Enemy GetBoss(Vector2 pos)
    {
        Enemy boss = null;
        bossEnemies.RemoveAll(x => x == null);

        if (bossEnemies.Count > 0)
        {
            boss = bossEnemies[0];
            bossEnemies.RemoveAt(0);
        }
        else
        {
            boss = Object.Instantiate(Resources.Load<Enemy>("Enemy"), GameObject.Find("EnemyParent").GetComponent<RectTransform>(), false);
        }
        activeEnemies.Add(boss);

        boss.gameObject.SetActive(true);
        boss.gameObject.transform.position = pos;

        //boss.EnemyData = GameManager.Instance.bossData[Random.Range(0, GameManager.Instance.bossData.Count)];
        boss.GetComponent<RectTransform>().DOSizeDelta(new Vector2(350, 350), 3);

        boss.stat.MaxHP = curHp;
        boss.stat.HP = int.MaxValue;

        return boss;
    }

    public static Enemy GetEnemy(Vector2 pos)
    {
        Enemy enemy = null;
        enemies.RemoveAll(x => x == null);

        if (enemies.Count > 0)
        {
            enemy = enemies[0];
            enemies.RemoveAt(0);
        }
        else
        {
            enemy = Object.Instantiate(Resources.Load<Enemy>("Enemy"), GameObject.Find("EnemyParent").GetComponent<RectTransform>(), false);
        }
        activeEnemies.Add(enemy);

        enemy.gameObject.SetActive(true);
        enemy.gameObject.transform.position = pos;

        enemy.EnemyData = GameManager.Instance.enemyDatas[Random.Range(0, GameManager.Instance.enemyDatas.Count)];
        enemy.GetComponent<RectTransform>().DOSizeDelta(new Vector2(350, 350), 3);

        enemy.stat.MaxHP = curHp;
        enemy.stat.HP = int.MaxValue;

        return enemy;
    }

    public static void ReturnEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        activeEnemies.Remove(enemy);
        enemy.gameObject.SetActive(false);
        enemy.gameObject.transform.position = Vector3.zero;
        enemy.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
    }
}
