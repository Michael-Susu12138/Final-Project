using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : MonoBehaviour
{
    Node [] PathNode;
    public GameObject lion;
    public float movespeed;
    float timer;
    int currentNode;
    static Vector3 currentPosition;
    void Start()
    {
        PathNode = GetComponentsInChildren<Node> ();
        CheckNode();
    }

    void CheckNode()
    {
        if(currentNode < PathNode.Length - 1){
            timer = 0;
        }
        currentPosition = PathNode[currentNode].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * movespeed;
        if(lion.transform.position != currentPosition){
            lion.transform.position = Vector3.Lerp(lion.transform.position, currentPosition, timer);

        }else{
            if(currentNode < PathNode.Length - 1){
                currentNode++;
                CheckNode();
            }   
        }
    }
}
