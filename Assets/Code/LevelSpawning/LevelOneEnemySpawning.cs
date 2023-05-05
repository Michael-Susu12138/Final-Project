using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneEnemySpawning : MonoBehaviour
{
    public GameObject Enemy1, Enemy2;
    GameManager _gameManager;
    IEnumerator Start()
    {
        while (true)
        {
            int enemy_counts = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemy_counts <= 0)
            {
                yield return StartCoroutine(SpawnEnemies());
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
        
    }
    IEnumerator SpawnEnemies()
    {
        var count = 0;
        var spawnPos = new Vector2(6.98f, 5.33f);
        while (true)
        {
            int enemy_counts = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemy_counts <= 0)
            {
                while (count < 1)
                {
                    Instantiate(Enemy1, spawnPos, Quaternion.identity);
                    yield return new WaitForSeconds(.7f);
                    Instantiate(Enemy2, spawnPos, Quaternion.identity);
                    count++;
                }
                yield return new WaitForSeconds(6.2f);
                count = 0;
                _gameManager.increaseWave();
                if (_gameManager.lastLevel == true)
                {
                    break;
                }
            }
            yield return new WaitForSeconds(1f); // Wait before checking enemy count again
        }
    }



}
