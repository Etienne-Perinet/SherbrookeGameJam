using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory 
{
    private Queue<Enemy> enemiesToSpawn;
    private List<Enemy> enemies;

    public EnemiesFactory(List<Enemy> enemiesOptions) 
    {
        enemies = enemiesOptions;
        enemiesToSpawn = new Queue<Enemy>();
    }

    public void GenerateEnemies(int cost)
    {
        Queue<Enemy> generatedEnemies = new Queue<Enemy>();
        while(cost > 0)
        {
            int randIndex = Random.Range(0, enemies.Count);
            int randCost = Random.Range(0, (int) enemies[randIndex].Cost);

            if(cost - randCost >= 0)
            {
                generatedEnemies.Enqueue(enemies[randIndex]);
                cost -= randCost;
            }
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    public bool IsEmpty() => enemiesToSpawn.Count <= 0; 

    public Enemy NextEnemy() => enemiesToSpawn.Dequeue();

    public void IncreaseCost(float percent)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].IncreaseCost(percent);
        }
    }
}
