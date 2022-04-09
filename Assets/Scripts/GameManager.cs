using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private EnemiesFactory enemyFactory;
    private float waveTimer = 0f;

    [SerializeField] private List<Enemy> enemies;

    [SerializeField] private GameObject player;
    
    void Awake() 
    {
        if (instance == null)
            instance = this;

        player = GameObject.Find("FeuFollet");
        enemyFactory = new EnemiesFactory(enemies);
    }

    void Start() 
    {
        enemyFactory.GenerateEnemies(50);
    }

    void Update() 
    {
        if(!enemyFactory.IsEmpty())
            SpawnEnemy();
        else if(waveTimer >= 10)
        {
            waveTimer = 0;
            enemyFactory.GenerateEnemies(50);        }
    }

    void FixedUpdate()
    {
        waveTimer += Time.deltaTime; 
    }

    void SpawnEnemy() => Instantiate(enemyFactory.NextEnemy().Prefab, RandomPos(), Quaternion.identity);

    protected Vector3 RandomPos() 
    {
        float radius = Random.Range(15f, 25f);
        float angle = Random.Range(0f, 360f);
        Vector3 newPos = new Vector3();
        newPos.y = player.transform.position.y + (radius * Mathf.Sin(angle));
        newPos.x = player.transform.position.x + (radius * Mathf.Cos(angle));
        return newPos;
    }
}
