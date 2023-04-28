using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    IEnumerator Start()
    {
        
        var count = 0;
        var spawnPos = new Vector2(-4.05f,4.38f);
        while(true){
            while(count < 5){
                Instantiate(Enemy, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(.7f);
                count++;
            } 
            yield return new WaitForSeconds(5.2f);
            count = 0;

        }
        
    }

}
