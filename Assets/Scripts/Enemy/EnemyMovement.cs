using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed =2;
    [SerializeField]
    protected Transform target;

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

    protected Vector3 randomPos() {
        float radius = 5f;
        float angle = Random.Range(0f, 360f);
        Vector3 newPos = new Vector3();
        newPos.y = target.position.y + (radius * Mathf.Sin(angle));
        newPos.x = target.position.x + (radius * Mathf.Cos(angle));
        return newPos;
    }
}
