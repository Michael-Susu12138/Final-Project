using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class killZone : MonoBehaviour
{
    public string failLevelToLoad,nextLevelToLoad;
    public string[] tagsToDestroy;

    GameManager _gameManager;
    void Start() {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(tagsToDestroy.Contains(other.tag)) {
            Destroy(other.gameObject);
            if(_gameManager.lastLevel){
                SceneManager.LoadScene(nextLevelToLoad);
            }
            _gameManager.reduceHealth(1);
            if(_gameManager.health <= 0){
                SceneManager.LoadScene(failLevelToLoad);
            }
            
        }
    }

}
