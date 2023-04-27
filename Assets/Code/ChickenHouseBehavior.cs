using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHouseBehavior : MonoBehaviour
{
    public GameObject chick_lvl1_fighter;
    ArrayList wayPoints = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject wayPoint in GameObject.FindGameObjectsWithTag("wayPoints")){
            wayPoints.Add(wayPoint.transform);
        }
        Vector2 spawnPos = GetClosestWayPoint(wayPoints).position;
        Instantiate(chick_lvl1_fighter,spawnPos,Quaternion.identity);
        Vector2 second_spawnPos = new Vector2(spawnPos.x-0.5f,spawnPos.y-0.5f);
        Instantiate(chick_lvl1_fighter,second_spawnPos,Quaternion.identity);
        Vector2 third_spawnPos = new Vector2(spawnPos.x+0.5f,spawnPos.y-0.5f);
        Instantiate(chick_lvl1_fighter,third_spawnPos,Quaternion.identity);
    }

    Transform GetClosestWayPoint(ArrayList wayPoints)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Transform t in wayPoints)
        {
            float dist = Vector2.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}