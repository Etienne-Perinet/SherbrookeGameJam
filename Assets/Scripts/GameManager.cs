using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private EnemiesFactory enemyFactory;
    [SerializeField] private List<Enemy> enemies;

    public int redEnemies;
    public int greenEnemies;
    public int blueEnemies;

    [SerializeField]
    private GameObject player;
    
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
        Debug.Log("Is it empty " + enemyFactory.IsEmpty());
        if(!enemyFactory.IsEmpty())
        {
            Debug.Log("Not empty");
            SpawnEnemy();
        }
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
