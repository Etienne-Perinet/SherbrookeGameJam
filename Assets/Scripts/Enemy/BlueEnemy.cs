using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    void Start()
    {
        enemyType = HealthBar.Color.BLUE;
        health = 4;
        CollisionDamage = 20;
        initialSpeed = 2;
        maxSpeed = 30;
        speed = initialSpeed;
        

    }
}
