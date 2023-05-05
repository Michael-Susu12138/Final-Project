using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelSpawning : MonoBehaviour
{
    public GameObject Enemy;
    public GameManager _gameManager;
    IEnumerator Start()
    {
        Enemy.GetComponent<WolverineMove>().routeTag = "wayPoints";
        Enemy.GetComponent<Enemy>().nextLevelToLoad = "Win";
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        var count = 0;
        var spawnPos = new Vector2(-4.05f,4.38f);
        while(true){
            while(count < 1){
                Instantiate(Enemy, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.7f);
                count++;
            } 
            yield return new WaitForSeconds(6.2f);
            count = 0;
            _gameManager.increaseWave();
            if(_gameManager.lastLevel == true){
                StopAllCoroutines();
                break;
            }

        }
        
    }

}
