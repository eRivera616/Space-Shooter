using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;


    private void Update() 
    {
        Scene _sceneNum = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //current game scene
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if (_sceneNum.buildIndex == 1)
           {
               SceneManager.LoadScene(0);
           }
        }
        //if the esc key is pressed
        //quit application
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
