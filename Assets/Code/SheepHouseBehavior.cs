using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHouseBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Transform spawnPos;
    public GameObject sheepArcher_prefab;
    GameObject archer;
    public int level;
    void Start()
    {
        spawnPos = gameObject.transform.GetChild(0);
        archer = Instantiate(sheepArcher_prefab,spawnPos.position,Quaternion.identity);
        archer.GetComponent<spadeSheep>().levelAS = level;
    }

    void OnDestroy()
    {
        Destroy(archer);
    }

    
}
