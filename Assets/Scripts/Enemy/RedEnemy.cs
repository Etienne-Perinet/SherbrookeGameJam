using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    void Start()
    {
        //health = 2;
        enemyType = HealthBar.Color.RED;
        health = 7;
        CollisionDamage = 12;
        initialSpeed = 1;
        maxSpeed = 20;
        speed = initialSpeed;
    }


}
