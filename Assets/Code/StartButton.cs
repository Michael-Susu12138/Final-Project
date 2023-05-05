using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneName;
    //GameManager _gameManager;
    public void StartGame(){
        Debug.Log("Clicked");
        //_gameManager = FindObjectOfType<GameManager>();
        SceneManager.LoadScene(sceneName);
    }
}
