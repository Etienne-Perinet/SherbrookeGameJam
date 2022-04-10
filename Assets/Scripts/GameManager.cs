using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private EnemiesFactory enemyFactory;
    private float waveTimer = 0f;
    private float increasePercent;


    [SerializeField] private string restartScene;
    [SerializeField] private GameObject endGameOverlay;
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
        if(endGameOverlay == null)
            endGameOverlay = GameObject.Find("Overlay");
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
                UpdatePercent();   
                Debug.Log("Increase percent " + increasePercent);
            }
        }
        
    }

    void FixedUpdate()
    {
        waveTimer += Time.deltaTime; 
    }

    void UpdatePercent() => increasePercent = (float) 1.2*Mathf.Pow(0.2f, waveTimer) + 2;


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

    public void StartGame()
    {
        SceneManager.LoadScene(restartScene);
    }

    public void EndGame()
    {
        endGameOverlay.SetActive(true);
    }

    public void IncreaseCost() => enemyFactory.IncreaseCost(increasePercent);
}
