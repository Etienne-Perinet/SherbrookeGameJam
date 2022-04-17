using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private EnemiesFactory enemyFactory;
    private float waveTimer;
    private int currentWave;
    private GameManager gameManager;
    private GameObject player;
    [SerializeField] private List<Enemy> enemiesType;
    [SerializeField] private List<Wave> waves;

    void Awake()
    {
        enemyFactory = new EnemiesFactory(enemiesType);
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = gameObject.GetComponent<GameManager>();
    }


    void Start() 
    {
        currentWave = 0;
        waveTimer = 0f;
        CreateWaves();
        enemyFactory.GenerateEnemies(waves[currentWave].NbEnemies);
    }

    void Update()
    {
        if(player.activeSelf)
        {
            if(!enemyFactory.IsEmpty())
                SpawnEnemy();
            else if(waveTimer >= waves[currentWave].WaveTime)
            {   
                Nextwave();
            }
        }
    }

    void CreateWaves()
    {
        waves.Add(new Wave(5, 5));
        waves.Add(new Wave(7, 5));
        waves.Add(new Wave(8, 7));
        waves.Add(new Wave(10, 7));
        waves.Add(new Wave(10, 10));
        waves.Add(new Wave(10, 7));
    }

    void Nextwave()
    {
        waveTimer = 0;
        currentWave = (currentWave + 1) % waves.Count; 
        enemyFactory.GenerateEnemies(waves[currentWave].NbEnemies);        
    }


    void FixedUpdate()
    {
        waveTimer += Time.deltaTime; 
    }

    void SpawnEnemy()
    {
        Enemy nextEnemy = enemyFactory.NextEnemy();
        gameManager.SpawnEnemy(nextEnemy.EnemyType);
        
        Instantiate(nextEnemy, RandomPos(), Quaternion.identity);
    }

    protected Vector3 RandomPos() 
    {
        float radius = Random.Range(15f, 30f);
        float angle = Random.Range(0f, 360f);
        Vector3 newPos = new Vector3();
        newPos.y = player.transform.position.y + (radius * Mathf.Sin(angle));
        newPos.x = player.transform.position.x + (radius * Mathf.Cos(angle));
        return newPos;
    }
}
