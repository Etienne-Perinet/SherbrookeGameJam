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

    public int Cost 
    {
        get { return CollisionDamage * (int) speed; }
    }

    public HealthBar.Color EnemyType { get; protected set;}

    protected virtual void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        Move(speed);
    }

    protected virtual void Die() 
    {
        FindObjectOfType<PlayerInteractions>().AddPoints(Cost);
        FindObjectOfType<GameManager>().DecrementEnemyCount(EnemyType);
        Destroy(gameObject);
    }

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
}
