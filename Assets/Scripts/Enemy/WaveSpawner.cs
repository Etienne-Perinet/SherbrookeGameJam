using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private EnemiesFactory enemyFactory;
    private float waveTimer;
    private int currentWave;
    private GameManager gameManager;

    [SerializeField] private GameObject player;
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
        enemyFactory.GenerateEnemies(50);
    }

    void Update()
    {
        if(player.activeSelf)
        {
            if(!enemyFactory.IsEmpty())
            SpawnEnemy();
            else if(waveTimer >= 7)
            {
                waveTimer = 0;
                enemyFactory.GenerateEnemies(40);        
            }
        }
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
