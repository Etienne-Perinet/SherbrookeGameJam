using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Die() 
    {
        gameObject.SetActive(false);   
        gameManager.EndGame();
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
                audioManager.AttributeAudioSource("DamageSound", gameObject.AddComponent<AudioSource>());
            }

            audioManager.Play("DamageSound");

            Enemy enemyObject = other.gameObject.GetComponent<Enemy>();

            HealthBarColor enemyColor = enemyObject.enemyType; 
            HealthBarResponse response = healthBar.AddColor(enemyColor, enemyObject.CollisionDamage);

            ChangeColor(response.color);

            if (response.isEnd) 
            {
                Die();
            }
        } 
    }
}
