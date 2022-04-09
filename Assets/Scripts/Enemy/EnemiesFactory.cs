using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesFactory : MonoBehaviour
{

    private List<Enemy> enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWave(int r, int g, int b) {
        for (int i = 0; i < r; i++)
            enemies.Add(new RedEnemy());

        for (int i = 0; i < g; i++)
            enemies.Add(new GreenEnemy());

        for (int i = 0; i < b; i++)
            enemies.Add(new BlueEnemy());
    }


}
