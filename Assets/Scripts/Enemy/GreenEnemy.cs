using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{
    void Start()
    {
        enemyType = HealthBar.Color.GREEN;
        health = 2;
        CollisionDamage = 6;
        initialSpeed = 4;
        maxSpeed = 40;
        speed = initialSpeed;
    }
}
