using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{
    void Start()
    {
        EnemyType = HealthBarColor.GREEN;
        health = 2;
        CollisionDamage = 6;
        speed = 6;
        maxSpeed = 40;
    }
}
