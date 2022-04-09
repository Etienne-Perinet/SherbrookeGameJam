using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnLocation;
    
    private float waveTimer;
    private float mavwaveTime;

    void Start()
    {
        waveTimer = 0;    
    }

    void Update()
    {
        waveTimer += Time.deltaTime;


    }
}
