using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHouseBehavior : MonoBehaviour
{
    public GameObject chick_lvl1_fighter;
    public int numFighters;
    private List<GameObject> chickenFighters = new List<GameObject>();
    ArrayList wayPoints = new ArrayList();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject wayPoint in GameObject.FindGameObjectsWithTag("wayPoints")){
            wayPoints.Add(wayPoint.transform);
        }
        SpawnChickenFighters();
    }

    void SpawnChickenFighters()
    {
        float spawnAreaSize = 0.5f;
        Vector2 spawnCenter = GetClosestWayPoint(wayPoints).position;

        for (int i = 0; i < numFighters; i++)
        {
            Vector2 spawnOffset = new Vector2(Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2), Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2));
            Vector2 spawnPos = spawnCenter + spawnOffset;
            chickenFighters.Add(Instantiate(chick_lvl1_fighter, spawnPos, Quaternion.identity));
        }
    }

    void OnDestroy()
    {
        foreach (GameObject chickenFighter in chickenFighters)
        {
            Destroy(chickenFighter);
        }
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
