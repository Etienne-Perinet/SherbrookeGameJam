using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    void Start()
    {
        EnemyType = HealthBarColor.BLUE;
        health = 4;
        CollisionDamage = 20;
        speed = 3;
        maxSpeed = 30;
    }
}
