using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
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
            int randCost = Random.Range(0, enemies[randIndex].Cost);
            Debug.Log("Rand cost " + randCost + " " + enemies[randIndex].Prefab.name);

            if(cost - randCost >= 0)
            {
                generatedEnemies.Enqueue(enemies[randIndex]);
                cost -= randCost;
            }
            Debug.Log("Cost " + cost);
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
        Debug.Log("Enemies to spawn " + enemiesToSpawn.Count);
    }

    public bool IsEmpty() => enemiesToSpawn.Count <= 0; 

    public Enemy NextEnemy() 
    {
        return enemiesToSpawn.Dequeue();

    }

    // public void GenerateWave(int r, int g, int b) {
    //     for (int i = 0; i < r; i++)
    //         enemiesToSpawn.Enqueue(new RedEnemy());

    //     for (int i = 0; i < g; i++)
    //         enemiesToSpawn.Enqueue(new GreenEnemy());

    //     for (int i = 0; i < b; i++)
    //         enemiesToSpawn.Enqueue(new BlueEnemy());
    // }


}
