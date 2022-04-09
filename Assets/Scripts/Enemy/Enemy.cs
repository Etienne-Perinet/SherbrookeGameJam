using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : EnemyMovement
{
    public GameObject prefab;
    public GameObject self;
    private float health;
    protected virtual void Collide() {}

    protected virtual void Die() {}

    protected virtual void GetShot() {}

    protected virtual void Shoot() {}

    public virtual Enemy Spawn() {
        self = Instantiate(prefab, transform.position, transform.rotation);
        return this;
    } 
}
