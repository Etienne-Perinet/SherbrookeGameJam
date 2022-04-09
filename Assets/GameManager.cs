using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private EnemiesFactory factory = new EnemiesFactory();

    public GameObject redEnemy;
    public GameObject greenEnemy;
    public GameObject blueEnemy;
    public int redEnemies;
    public int greenEnemies;
    public int blueEnemies;

    [SerializeField]
    private GameObject player;
    
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start() 
    {
        player = GameObject.Find("FeuFollet");
        NextWave();
    }

    void NextWave() 
    {
        
        //Instantiate(redEnemy, randomPos(), Quaternion.identity);
        GenerateWave(redEnemies, greenEnemies, blueEnemies);
    }

    public void GenerateWave(int r, int g, int b) {
        for (int i = 0; i < r; i++)
            Instantiate(redEnemy, randomPos(), Quaternion.identity);

        for (int i = 0; i < g; i++)
            Instantiate(greenEnemy, randomPos(), Quaternion.identity);

        for (int i = 0; i < b; i++)
            Instantiate(blueEnemy, randomPos(), Quaternion.identity);
    }

    protected Vector3 randomPos() {
        //Debug.Log("target : " + player.name);
        float radius = Random.Range(15f, 25f);
        float angle = Random.Range(0f, 360f);
        Vector3 newPos = new Vector3();
        newPos.y = player.transform.position.y + (radius * Mathf.Sin(angle));
        newPos.x = player.transform.position.x + (radius * Mathf.Cos(angle));
        return newPos;
    }
}
