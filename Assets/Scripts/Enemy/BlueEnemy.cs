using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    void Start()
    {
        EnemyType = HealthBar.Color.BLUE;
    }
}
