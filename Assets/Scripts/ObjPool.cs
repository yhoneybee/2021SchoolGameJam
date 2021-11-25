using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjPool
{
    private static List<Enemy> enemies = new List<Enemy>();
    public static Enemy GetEnemy(Vector2 pos)
    {
        Enemy enemy = null;

        if (enemies.Count > 0)
        {
            enemy = enemies[0];
        }
        else
        {
            enemy = Object.Instantiate(Resources.Load<Enemy>("Enemys/"));
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
