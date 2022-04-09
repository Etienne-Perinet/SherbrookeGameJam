using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float speed =2;
    public GameObject deathAnimation;

    [SerializeField]
    protected Transform target;
    [SerializeField]
    public int health = 1;

    protected string enemyType;
    protected float collisionDamage = 1f;

    protected virtual void Awake()
    {
        Debug.Log("Starting enemy spawn");
        
        target = GameObject.Find("FeuFollet").GetComponent<Transform>();
        
    }

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
        }else if(other.gameObject.CompareTag("Player"))
        {
            if(deathAnimation != null)
            {
                GameObject effect = Instantiate(deathAnimation, transform.position, Quaternion.identity);
                Destroy(effect, 0.4f);
            }
            Die();
        }

        if(health < 1)
            Die();
    }

    protected virtual void Move(float _speed) {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    public string GetEnemyDamageType()
    {
        return enemyType;
    }

        public float GetEnemyCollisionDamage()
    {
        return collisionDamage;
    }

    protected virtual void Update()
    {
        Move(speed);
    }

}
