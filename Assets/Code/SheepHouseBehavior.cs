using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHouseBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Transform spawnPos;
    public GameObject sheepArcher;
    void Start()
    {
        spawnPos = gameObject.transform.GetChild(0);
        Instantiate(sheepArcher,spawnPos.position,Quaternion.identity);
    }

    
}
