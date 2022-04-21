using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    private void Die() 
    {
        gameObject.SetActive(false);   
        FindObjectOfType<GameManager>().EndGame();
    }

    private void ChangeColor(Color color) 
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.gameObject.tag == "Enemy")
        {
            if(gameObject.GetComponent<AudioManager>() == null)
            {
                FindObjectOfType<AudioManager>().AttributeAudioSource("DamageSound", gameObject.AddComponent<AudioSource>());
            }

            FindObjectOfType<AudioManager>().Play("DamageSound");

            Enemy enemyObject = other.gameObject.GetComponent<Enemy>();
            HealthBarColor enemyColor = enemyObject.GetEnemyDamageType(); 
            HealthBarResponse response = healthBar.AddColor(enemyColor, enemyObject.CollisionDamage);

            ChangeColor(response.color);

            if (response.isEnd) 
            {
                Die();
            }
        } 
    }
}
