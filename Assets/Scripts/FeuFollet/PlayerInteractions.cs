using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField]
    private HealthBar healthBar;
    private int falseHealthBar = 5;
    public Rigidbody2D rb;

    private string lastEnemyColor;

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
                       
            switch (other.gameObject.GetComponent<Enemy>().GetEnemyDamageType())
            {
                case "Red":
                    healthBar.AddColor(HealthBar.Color.RED, other.gameObject.GetComponent<Enemy>().GetEnemyCollisionDamage());
                    lastEnemyColor = "Red";Debug.Log("Last enemy damage : " + lastEnemyColor);
                    falseHealthBar--;
                    break;
                case "Blue":
                    healthBar.AddColor(HealthBar.Color.BLUE, other.gameObject.GetComponent<Enemy>().GetEnemyCollisionDamage());
                    lastEnemyColor = "Blue"; Debug.Log("Last enemy damage : " + lastEnemyColor);
                    falseHealthBar--;
                    break;
                case "Green":
                    healthBar.AddColor(HealthBar.Color.GREEN, other.gameObject.GetComponent<Enemy>().GetEnemyCollisionDamage());
                    lastEnemyColor = "Green";Debug.Log("Last enemy damage : " + lastEnemyColor);
                    falseHealthBar--;
                    break;
            }
        } 
        if(healthBar.IsDead() || falseHealthBar < 1)
            Die();
    }
}
