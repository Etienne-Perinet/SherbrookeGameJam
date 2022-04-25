using UnityEngine;

[System.Serializable]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] protected int health = 1;
    
    protected Transform target;
    private GameManager gameManager;
    public GameObject deathAnimation;
    [field: SerializeField] public int CollisionDamage { get; protected set; }

    [field: SerializeField] public GameObject Prefab { get; protected set; }

    public int Cost 
    {
        get { return CollisionDamage * (int) speed; }
    }
    public HealthBarColor EnemyType { get; protected set; }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() => Move(speed);

    private void Die() 
    {
        gameManager.DecrementEnemyCount(EnemyType);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
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

    private void Move(float _speed) 
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }
}
