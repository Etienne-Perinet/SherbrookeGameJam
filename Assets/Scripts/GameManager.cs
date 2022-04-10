using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private EnemiesFactory enemyFactory;
    private float waveTimer = 0f;
    [SerializeField] private string restartScene;
    [SerializeField] private GameObject endGameOverlay;
    [SerializeField] private List<Enemy> enemies;

    [SerializeField] private GameObject player;

    Dictionary<HealthBar.Color, int> spawns = new Dictionary<HealthBar.Color, int>();
    Dictionary<HealthBar.Color, float> ratios = new Dictionary<HealthBar.Color, float>();

    private AudioManager am;
    
    void Awake() 
    {
        if (instance == null)
        instance = this;

        player = GameObject.Find("FeuFollet");
        enemyFactory = new EnemiesFactory(enemies);
        
        spawns.Add(HealthBar.Color.RED, 0);
        spawns.Add(HealthBar.Color.BLUE, 0);
        spawns.Add(HealthBar.Color.GREEN, 0);

        ratios.Add(HealthBar.Color.RED, 0f);
        ratios.Add(HealthBar.Color.BLUE, 0f);
        ratios.Add(HealthBar.Color.GREEN, 0f);

        Debug.Log("Awake");
    }

    void Start() 
    {
        am = FindObjectOfType<AudioManager>();
        StartMusic();
        
        if(endGameOverlay == null)
            endGameOverlay = GameObject.Find("Overlay");
        enemyFactory.GenerateEnemies(50);
    }

    void StartMusic()
    {
        am.Play("StemBase");
        am.Play("StemRed");
        am.Play("StemGreen");
        am.Play("StemBlue");

        am.setSoundVolume("StemRed", 0f);
        am.setSoundVolume("StemGreen", 0f);
        am.setSoundVolume("StemBlue", 0f);
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

        //UpdateMainTheme();
    }

    void FixedUpdate()
    {
        waveTimer += Time.deltaTime; 
    }

    void SpawnEnemy()
    {
        Enemy nextEnemy = enemyFactory.NextEnemy();
        //Debug.LogWarning("NEXT ENEMY TYPE : " + nextEnemy.GetEnemyDamageType());
        
        spawns[nextEnemy.GetEnemyDamageType()] += 1;
        //spawns[nextEnemy.GetEnemyDamageType()] = spawns[nextEnemy.GetEnemyDamageType()] + 1;
        
        Instantiate(nextEnemy, RandomPos(), Quaternion.identity);
        CalculateRatios();
        UpdateMainTheme();
        
    } 

    private void UpdateMainTheme()
    {
        //Debug.Log("UPDATING WITH RED : " + spawns[HealthBar.Color.RED]);
        //Debug.Log("UPDATING WITH BLUE : " + spawns[HealthBar.Color.BLUE]);
        am.setSoundVolume("StemRed", ratios[HealthBar.Color.RED]);
        am.setSoundVolume("StemGreen", ratios[HealthBar.Color.GREEN]);
        am.setSoundVolume("StemBlue", ratios[HealthBar.Color.BLUE]);
        Debug.Log("RED VOLUME : " + am.getSoundVolume("StemRed"));
        Debug.Log("BLUE VOLUME : " + am.getSoundVolume("StemBlue"));
        Debug.Log("GREEN VOLUME : " + am.getSoundVolume("StemGreen"));
    }

    public void DecrementEnemyCount(HealthBar.Color enemyType)
    {
        spawns[enemyType]--;
        CalculateRatios();
        UpdateMainTheme();
    }

    private void CalculateRatios()
    { 
        float total = spawns[HealthBar.Color.BLUE] + spawns[HealthBar.Color.RED] + spawns[HealthBar.Color.GREEN];
        //Debug.Log("TOTAL ENEMIES : " + total);
        if(total == 0f)
            return;
        ratios[HealthBar.Color.BLUE] = (float)spawns[HealthBar.Color.BLUE]/total;
        ratios[HealthBar.Color.RED] = (float)spawns[HealthBar.Color.RED]/total;
        ratios[HealthBar.Color.GREEN] = (float)spawns[HealthBar.Color.GREEN]/total;      
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

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Stop("StartMenu");
        FindObjectOfType<AudioManager>().Stop("GameOver");
        FindObjectOfType<AudioManager>().Play("StemBase");
        SceneManager.LoadScene(restartScene);
    }

    public void EndGame()
    {
        FindObjectOfType<AudioManager>().Stop("StemBase");
        FindObjectOfType<AudioManager>().Stop("StemRed");
        FindObjectOfType<AudioManager>().Stop("StemGreen");
        FindObjectOfType<AudioManager>().Stop("StemBlue");
        FindObjectOfType<AudioManager>().Play("GameOver");
        endGameOverlay.SetActive(true);
    }
}
