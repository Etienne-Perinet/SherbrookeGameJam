using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    private EnemiesFactory enemyFactory;
    private float waveTimer;
    private int currentWave;
    private int waveCount;
    private GameManager gameManager;
    private GameObject player;
    private TextMeshProUGUI timerTxt;
    private TextMeshProUGUI waveCountTxt;
    private List<Wave> waves;
    [SerializeField] private List<Enemy> enemiesType;

    void Awake()
    {
        enemyFactory = new EnemiesFactory(enemiesType);
        player = GameObject.FindGameObjectWithTag("Player");
        timerTxt = GameObject.FindGameObjectWithTag("WaveTimer").GetComponent<TextMeshProUGUI>();
        waveCountTxt = GameObject.FindGameObjectWithTag("WaveCount").GetComponent<TextMeshProUGUI>();
        gameManager = gameObject.GetComponent<GameManager>();
        waves = new List<Wave>();
    }

    void Start() 
    {
        CreateWaves();
        enemyFactory.GenerateEnemies(waves[currentWave].NbEnemies);
    }

    void Update()
    {
        if(player.activeSelf)
        {
            if(!enemyFactory.IsEmpty())
                SpawnEnemy();
            else if(waveTimer <= 0)
                Nextwave();
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
        IncrementWaveCount();
        currentWave = (currentWave + 1) % waves.Count; 
        enemyFactory.GenerateEnemies(waves[currentWave].NbEnemies);   
        StartCoroutine(StartCountdown(waves[currentWave].WaveTime));
    }

    public IEnumerator StartCountdown(float countdownValue)
    {
        waveTimer = countdownValue;
        while (waveTimer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            waveTimer--;
            DisplayTime(waveTimer);
        }
    }

    void IncrementWaveCount()
    {
        waveCount++;
        waveCountTxt.text = "Vague " + waveCount.ToString();
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);  
        float seconds = time - (minutes * 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
