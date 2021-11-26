using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObjPool
{
    private static List<Enemy> enemies = new List<Enemy>();
    private static List<Enemy> activeEnemies = new List<Enemy>();

    public static Enemy GetFrontEnemy() => activeEnemies.Count > 0 ? (activeEnemies[0].gameObject.activeSelf ? activeEnemies[0] : null) : null;
    public static Enemy GetRandEnemy()
    {
        var temp = activeEnemies.Where(x => x.gameObject.activeSelf);
        return temp.ElementAt(Random.Range(0, temp.Count()));
    }

    public static Enemy GetEnemy(Vector2 pos)
    {
        Enemy enemy = null;

        if (enemies.Count > 0)
        {
            enemy = enemies[0];
            enemies.RemoveAt(0);
        }
        else
        {
            enemy = Object.Instantiate(Resources.Load<Enemy>("Enemy"), GameObject.Find("Canvas").GetComponent<RectTransform>(), false);
        }
        activeEnemies.Add(enemy);

        enemy.gameObject.SetActive(true);
        enemy.gameObject.transform.position = pos;

        enemy.EnemyData = GameManager.Instance.enemyDatas[Random.Range(0, GameManager.Instance.enemyDatas.Count)];

        return enemy;
    }

    public static void ReturnEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        activeEnemies.Remove(enemy);
        enemy.gameObject.SetActive(false);
        enemy.gameObject.transform.position = Vector3.zero;
    }
}
