using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] protected float health = 1f;
    
    protected Transform target;
    public GameObject deathAnimation;
    protected HealthBar.Color enemyType;

    [field: SerializeField] public float CollisionDamage { get; protected set; }

    [field: SerializeField] public GameObject Prefab { get; protected set; }

    public int Damage { get; protected set; } 

    public float Cost 
    {
        get { return health * CollisionDamage * speed; }
    }

    protected virtual void Awake()
    {
        target = GameObject.Find("FeuFollet").GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        Move(speed);
    }

    protected virtual void Die() 
    {
        Destroy(gameObject);
    }

    protected virtual void Shoot() {}

    protected virtual void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
            health--;
        else if(other.gameObject.CompareTag("Player"))
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

    protected virtual void Move(float _speed) 
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    public HealthBar.Color GetEnemyDamageType()
    {
        return enemyType;
    }

    public void IncreaseCost(float percent)
    {
        CollisionDamage += CollisionDamage * (percent / 100);
        health += health * (int)(percent / 100);
    }

}
