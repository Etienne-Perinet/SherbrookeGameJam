using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory 
{
    private Queue<Enemy> enemiesToSpawn;
    private List<Enemy> enemiesType;

    public EnemiesFactory(List<Enemy> enemiesOptions) 
    {
        enemiesType = enemiesOptions;
        enemiesToSpawn = new Queue<Enemy>();
    }

    public void GenerateEnemies(int nbEnemies)
    {
        Queue<Enemy> generatedEnemies = new Queue<Enemy>();
        
        for(int i = 0; i < enemiesType.Count; i++)
            AddEnemies(generatedEnemies, enemiesType[i], Random.Range(1, nbEnemies));

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    void AddEnemies(Queue<Enemy> enemies, Enemy enemyPrefab, int number)
    {
        for(int i = 0; i < number; i++)
            enemies.Enqueue(enemyPrefab);
    }

    public bool IsEmpty() => enemiesToSpawn.Count <= 0; 

    public Enemy NextEnemy() 
    {
        return enemiesToSpawn.Dequeue();

    }

    public void IncreaseCost(float gameTimer)
    {
        for(int i = 0; i < enemiesType.Count; i++)
        {
            enemiesType[i].IncreaseCost(gameTimer);
        }
    }
}
