using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneEnemySpawning : MonoBehaviour
{
    public GameObject Enemy1, Enemy2;
    GameManager _gameManager;
    IEnumerator Start()
    {
        Enemy1.GetComponent<WolverineMove>().routeTag = "wayPoints";
        Enemy1.GetComponent<Enemy>().nextLevelToLoad = "Level_2";

        Enemy2.GetComponent<WolverineMove>().routeTag = "wayPoints";
        Enemy2.GetComponent<Enemy>().nextLevelToLoad = "Level_2";
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        var count = 0;
        var spawnPos = new Vector2(6.98f,5.33f);
        while(true){
            while(count < 1){
                Instantiate(Enemy1, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.7f);
                Instantiate(Enemy2, spawnPos, Quaternion.identity);
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
