using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private string restartScene;
    [SerializeField] private GameObject endGameOverlay;

    Dictionary<HealthBarColor, int> spawns = new Dictionary<HealthBarColor, int>();
    Dictionary<HealthBarColor, float> ratios = new Dictionary<HealthBarColor, float>();

    private AudioManager am;
    
    void Awake() 
    {
        if (instance == null)
            instance = this;

        spawns.Add(HealthBarColor.RED, 0);
        spawns.Add(HealthBarColor.BLUE, 0);
        spawns.Add(HealthBarColor.GREEN, 0);

        ratios.Add(HealthBarColor.RED, 0f);
        ratios.Add(HealthBarColor.BLUE, 0f);
        ratios.Add(HealthBarColor.GREEN, 0f);
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

    public void SpawnEnemy(HealthBarColor enemyType)
    {
        spawns[enemyType] += 1;
        
        CalculateRatios();
        UpdateMainTheme();
    } 

    private void UpdateMainTheme()
    {
        am.setSoundVolume("StemRed", ratios[HealthBarColor.RED]);
        am.setSoundVolume("StemGreen", ratios[HealthBarColor.GREEN]);
        am.setSoundVolume("StemBlue", ratios[HealthBarColor.BLUE]);
    }

    public void DecrementEnemyCount(HealthBarColor enemyType)
    {
        spawns[enemyType]--;
        CalculateRatios();
        UpdateMainTheme();
    }

    private void CalculateRatios()
    { 
        float total = spawns[HealthBarColor.BLUE] + spawns[HealthBarColor.RED] + spawns[HealthBarColor.GREEN];

        if(total == 0f)
            return;
        ratios[HealthBarColor.BLUE] = (float)spawns[HealthBarColor.BLUE]/total;
        ratios[HealthBarColor.RED] = (float)spawns[HealthBarColor.RED]/total;
        ratios[HealthBarColor.GREEN] = (float)spawns[HealthBarColor.GREEN]/total;      
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
