using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{

    public string restartScene;
    void Start() 
    {
        FindObjectOfType<AudioManager>().Play("StartMenu");    
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Stop("StartMenu");
        SceneManager.LoadScene(restartScene);
    }
}
