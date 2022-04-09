using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : EnemyMovement
{
    protected virtual void Collide() {}

    protected virtual void Die() {}

    protected virtual void GetShot() {}

    protected virtual void Shoot() {}
}
