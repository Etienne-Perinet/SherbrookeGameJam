using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;

    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Move(float _speed) {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    protected virtual void Update()
    {
        Move(speed);
    }
}
