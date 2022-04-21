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

    public void DecrementEnemyCount(HealthBarColor enemyType)
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
