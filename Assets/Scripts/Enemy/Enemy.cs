using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float speed =2;
    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected int health = 1;

    protected virtual void Awake()
    {
        Debug.Log("Starting enemy spawn");
        
        target = GameObject.Find("FeuFollet").GetComponent<Transform>();
        
    }

    protected virtual void Collide() {}

    protected virtual void Die() 
    {
        Destroy(gameObject);
    }

    protected virtual void Shoot() {}

    protected virtual void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Collision other : " + other.gameObject.tag);
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            health--;
        }
        if(health < 1)
            Die();
    }

    protected virtual void Move(float _speed) {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    protected virtual void Update()
    {
        Move(speed);
    }

}
