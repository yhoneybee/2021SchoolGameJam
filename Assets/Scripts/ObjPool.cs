using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObjPool
{
    private static List<Enemy> enemies = new List<Enemy>();

    public static Enemy GetFrontEnemy() => enemies.Count > 0 ? (enemies[0].gameObject.activeSelf ? enemies[0] : null) : null;
    public static Enemy GetRandEnemy()
    {
        var temp = enemies.Where(x => x.gameObject.activeSelf);
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
            enemy = Object.Instantiate(Resources.Load<Enemy>("Enemys/Enemy"), GameObject.Find("Canvas").GetComponent<RectTransform>(), false);
        }

        enemy.gameObject.SetActive(true);
        enemy.gameObject.transform.position = pos;
        return enemy;
    }

    public static void ReturnEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        enemy.gameObject.SetActive(false);
        enemy.gameObject.transform.position = Vector3.zero;
    }
}
