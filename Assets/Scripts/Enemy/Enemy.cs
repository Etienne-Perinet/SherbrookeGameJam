using UnityEngine;

[System.Serializable]
public abstract class Enemy : MonoBehaviour
{
    protected float speed = 2f;
    protected float maxSpeed = 4f;
    protected int health = 1;
    
    protected Transform target;
    private GameManager gameManager;
    public int CollisionDamage { get; protected set; }
    public HealthBarColor EnemyType { get; protected set; }
    [field: SerializeField] public GameObject Prefab { get; protected set; }
  
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
        if(health < 1 || other.gameObject.CompareTag("Player"))
            Die();
    }

    private void Move(float _speed) 
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    public void IncreaseCost(float gameTimer)
    {
        if(speed < maxSpeed)
            speed += speed * (((float) Mathf.Pow(1.28f, 0.05f*gameTimer) + 1) / 100);
    }
}
