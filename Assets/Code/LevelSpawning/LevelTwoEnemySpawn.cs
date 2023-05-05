using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoEnemySpawn : MonoBehaviour
{
    public GameObject Enemy1, Enemy2, Enemy3;
    GameManager _gameManager;
    IEnumerator Start()
    {
        Enemy1.GetComponent<WolverineMove>().routeTag = "wolverineRoute";
        Enemy1.GetComponent<Enemy>().nextLevelToLoad = "Win";

        Enemy2.GetComponent<WolverineMove>().routeTag = "wolverineRoute";
        Enemy2.GetComponent<Enemy>().nextLevelToLoad = "Win";

        Enemy3.GetComponent<WolverineMove>().routeTag = "wayPoints";
        Enemy3.GetComponent<Enemy>().nextLevelToLoad = "Win";
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        var count = 0;
        var spawnPos = new Vector2(-10.01f,3.62f);
        while(true){
           
            while(count < 1){
            
                Instantiate(Enemy1, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.7f);
                Instantiate(Enemy2, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(1.7f);
                Instantiate(Enemy3, new Vector2(-10.01f,-2.86f), Quaternion.identity);
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
