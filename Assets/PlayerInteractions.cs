using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    int HealthBar = 5;

    void Awake() 
    {
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            HealthBar--;                   
        }
        
        if(HealthBar < 1)
            Destroy(gameObject);
    }
}
