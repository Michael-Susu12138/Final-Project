using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:
// text on health bar
// health bar in canvas
// canvas top left



public class FighterBehavior : MonoBehaviour
{
    public float x_speed, y_speed;
    float lookDst = 1.1f;
    Rigidbody2D _rigidbody2D;
    GameObject Enemies;
    
    
    // // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Enemies = GameObject.FindGameObjectWithTag("Player");
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void behavior(){
        if (Vector2.Distance(transform.position,Enemies.transform.position) < lookDst){
            if(Enemies.transform.position.x > transform.position.x && transform.localScale.x < 0 || 
                Enemies.transform.position.x < transform.position.x && transform.localScale.x > 0){
                    transform.localScale *= new Vector2(-1,1);
                }

            Vector2 dir = (Enemies.transform.position - transform.position);
            
            _rigidbody2D.velocity = new Vector2(dir.normalized.x * x_speed, dir.normalized.y*y_speed);
            
            
        }
    }
}
