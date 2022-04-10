using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] protected int health = 1;
    
    protected Transform target;
    public GameObject deathAnimation;
    [field: SerializeField] public int CollisionDamage { get; protected set; }

    [field: SerializeField] public GameObject Prefab { get; protected set; }

    public int Damage { get; protected set; } 

    public int Cost 
    {
        get { return health * CollisionDamage * (int)speed; }
    }
    protected HealthBar.Color enemyType;
    protected float collisionDamage = 1f;

    protected virtual void Awake()
    {
        Debug.Log("Starting enemy spawn");
        
        target = GameObject.Find("FeuFollet").GetComponent<Transform>();
    }

    protected virtual void Die() 
    {
        //Debug.Log("Dieee");
        FindObjectOfType<GameManager>().DecrementEnemyCount(enemyType);
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

    public float GetEnemyCollisionDamage()
    {
        return CollisionDamage;
    }

    protected virtual void Update()
    {
        Move(speed);
    }

}
