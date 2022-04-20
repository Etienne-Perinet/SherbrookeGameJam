using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private string restartScene;
    [SerializeField] private GameObject endGameOverlay;

    Dictionary<HealthBar.Color, int> spawns = new Dictionary<HealthBar.Color, int>();
    Dictionary<HealthBar.Color, float> ratios = new Dictionary<HealthBar.Color, float>();

    private AudioManager am;
    
    void Awake() 
    {
        if (instance == null)
            instance = this;

        spawns.Add(HealthBar.Color.RED, 0);
        spawns.Add(HealthBar.Color.BLUE, 0);
        spawns.Add(HealthBar.Color.GREEN, 0);

        ratios.Add(HealthBar.Color.RED, 0f);
        ratios.Add(HealthBar.Color.BLUE, 0f);
        ratios.Add(HealthBar.Color.GREEN, 0f);
    }

    void Start() 
    {
        am = FindObjectOfType<AudioManager>();
        StartMusic();
        
        if(endGameOverlay == null)
            endGameOverlay = GameObject.Find("Overlay");
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

    public void SpawnEnemy(HealthBar.Color enemyType)
    {
        spawns[enemyType] += 1;
        
        CalculateRatios();
        UpdateMainTheme();
    } 

    private void UpdateMainTheme()
    {
        am.setSoundVolume("StemRed", ratios[HealthBar.Color.RED]);
        am.setSoundVolume("StemGreen", ratios[HealthBar.Color.GREEN]);
        am.setSoundVolume("StemBlue", ratios[HealthBar.Color.BLUE]);
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
        if(total == 0f)
            return;
        ratios[HealthBar.Color.BLUE] = (float)spawns[HealthBar.Color.BLUE]/total;
        ratios[HealthBar.Color.RED] = (float)spawns[HealthBar.Color.RED]/total;
        ratios[HealthBar.Color.GREEN] = (float)spawns[HealthBar.Color.GREEN]/total;      
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
