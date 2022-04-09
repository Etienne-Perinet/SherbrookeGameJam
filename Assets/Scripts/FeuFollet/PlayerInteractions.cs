using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField]
    private HealthBar healthBar;
    private int falseHealthBar = 5;
    public Rigidbody2D rb;

    private HealthBar.Color lastEnemyColor;

    private void Die() 
    {
        gameObject.SetActive(false);   
        FindObjectOfType<GameManager>().EndGame();
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
            Enemy enemyObject = other.gameObject.GetComponent<Enemy>();
            HealthBar.Color enemyColor = enemyObject.GetEnemyDamageType();
            if(!healthBar.AddColor(enemyColor, enemyObject.GetEnemyCollisionDamage()))
                 Die();
            else
            {
                lastEnemyColor = other.gameObject.GetComponent<Enemy>().GetEnemyDamageType();
                falseHealthBar--;
            }
        } 
        //if(healthBar.IsDead() || falseHealthBar < 1)
            //Die();
    }
}
