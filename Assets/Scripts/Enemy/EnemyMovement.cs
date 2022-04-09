using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed =2;
    private Transform target;

    protected virtual void Start()
    {
        target = GameObject.Find("FeuFollet").GetComponent<Transform>();
    }

    protected virtual void Move(float _speed) {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    protected virtual void Update()
    {
        Move(speed);
    }

}
