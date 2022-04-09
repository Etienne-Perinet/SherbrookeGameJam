using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private EnemiesFactory factory = new EnemiesFactory();

    void Awake() 
    {
        if (instance != null)
        {
            instance = this;
        }
    }

    void Start() 
    {
        NextWave();
    }

    void NextWave() 
    {
        factory.GenerateWave(10, 10, 10);
    }
}
