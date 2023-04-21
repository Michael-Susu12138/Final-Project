using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    // public Transform destination;
    public Transform[] wayPoints;
    int index = 0;
    float speed = 1.2f;
    float _deltaTime;
    void Start()
    {
        
        _rigidbody2D = GetComponent<Rigidbody2D>(); 

        // wayPointsObj = GameObject.FindGameObjectsWithTag("wayPoints");
        // StartCoroutine(MoveLoop());
    }
    // Update is called once per frame
    void Update()
    { 
        if (index<wayPoints.Length){
            float step = speed*Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,wayPoints[index].position,step);
            if (Vector2.Distance(transform.position,wayPoints[index].position) < 0.1f){
                index += 1;
            }
        }
        
        
    }
    
}
