using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    private int falseHealthBar = 5;
    public Rigidbody2D rb;

    private HealthBar.Color lastEnemyColor;

    private int playerPoints;
    public TextMeshProUGUI pointsUI;

    private void Awake()
    {
        playerPoints = 0;
    }

    void Update()
    {
        if(pointsUI != null)
            pointsUI.SetText(playerPoints + "");
    }

    private void Die() 
    {
        gameObject.SetActive(false);   
        FindObjectOfType<GameManager>().EndGame();
    }

    private Color HexToRGB(string hex) 
    {
        Color c = new Color();
        ColorUtility.TryParseHtmlString(hex, out c);
        return c;
    }

    private void ChangeColor(string hex) 
    {
        GetComponent<SpriteRenderer>().color = HexToRGB(hex);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.gameObject.tag == "Enemy")
        {
            // Code qui pourrait servir à donner une impulsion à la collision
            // Rigidbody2D enemy = other.otherRigidbody;
            // if(enemy != null)
            // {
            //     rb.isKinematic = false;
            //     Vector2 difference = enemy.transform.position - transform.position;
            //     difference = difference.normalized * other.gameObject.GetComponent<Enemy>().GetEnemyCollisionDamage();
            //     rb.AddForce(difference, ForceMode2D.Impulse);
            //     rb.isKinematic = true; 
            // }

            if(gameObject.GetComponent<AudioManager>() == null)
            {
                FindObjectOfType<AudioManager>().AttributeAudioSource("DamageSound", gameObject.AddComponent<AudioSource>());
            }

            FindObjectOfType<AudioManager>().Play("DamageSound");
            AddPoints(10);

            ChangeColor("#"+healthBar.GetColor());

            Enemy enemyObject = other.gameObject.GetComponent<Enemy>();
            HealthBar.Color enemyColor = enemyObject.GetEnemyDamageType(); 
            if(!healthBar.AddColor(enemyColor, enemyObject.CollisionDamage))
            {
                Die();
            }
            else
            {
                lastEnemyColor = other.gameObject.GetComponent<Enemy>().GetEnemyDamageType();
                //falseHealthBar--;
            }
        } 
        //if(healthBar.IsDead() || falseHealthBar < 1)
            //Die();
    }

    public void AddPoints(int p) => playerPoints += p;
}
