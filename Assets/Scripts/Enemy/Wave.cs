using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [field: SerializeField] public int NbEnemies { get; protected set; }
    [field: SerializeField] public float WaveTime { get; protected set; }

    public Wave(int nbEnemies, float waveTime)
    {
        NbEnemies = nbEnemies;
        WaveTime = waveTime;
    }
}
