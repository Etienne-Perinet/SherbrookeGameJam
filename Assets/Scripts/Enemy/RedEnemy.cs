using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{
    void Start()
    {
        EnemyType = HealthBarColor.RED;
        health = 7;
        CollisionDamage = 12;
        speed = 1;
        maxSpeed = 20;
    }
}
