using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    // public Transform destination;
    ArrayList wayPoints = new ArrayList();
    int index = 0;
    public float speed = 1.2f;
    float _deltaTime;
    void Start()
    {
        
        _rigidbody2D = GetComponent<Rigidbody2D>(); 

        // wayPointsObj = GameObject.FindGameObjectsWithTag("wayPoints");
        // StartCoroutine(MoveLoop());
        foreach(GameObject wayPoint in GameObject.FindGameObjectsWithTag("wayPoints")){
            wayPoints.Add(wayPoint.transform);
        }
        Debug.Log(wayPoints);
    }
    // Update is called once per frame
    void Update()
    { 
        if (index<wayPoints.Count){
            float step = speed*Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,getItemByIndex(wayPoints,index).position,step);
            if (Vector2.Distance(transform.position,getItemByIndex(wayPoints,index).position) < 0.1f){
                index += 1;
            }
        }
        
        
    }
    Transform getItemByIndex(ArrayList lst, int index){
        Transform result = (Transform)lst[index];
        return result;
    }
    
}
