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
    float lookDst = 1.6f;
    public bool fight = false;
    Rigidbody2D _rigidbody2D;
    ArrayList Enemies = new ArrayList();
    
    
    // // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // Enemies = GameObject.FindGameObjectWithTag("Enemy");
        foreach(GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            Enemies.Add(Enemy.transform);
        }
    }

    // // Update is called once per frame
    void Update()
    {
        if(!fight){
            behavior();   
        } else {
            attack();
        }
        
    }

    void behavior(){
        if (Vector2.Distance(transform.position,GetClosestEnemy(Enemies).position) < lookDst){
            if(GetClosestEnemy(Enemies).position.x > transform.position.x && transform.localScale.x < 0 || 
                GetClosestEnemy(Enemies).position.x < transform.position.x && transform.localScale.x > 0){
                    transform.localScale *= new Vector2(-1,1);
                }

            Vector2 dir = (GetClosestEnemy(Enemies).position - transform.position);
            
            _rigidbody2D.velocity = new Vector2(dir.normalized.x * x_speed, dir.normalized.y*y_speed);
            
        }
    }
    void attack(){
        var closestEnemy = GetClosestEnemy(Enemies).gameObject;
        var count = 0;
        count++;
        if (count >=3 ){
            count = 0;
            fight = false;
            Enemies = UpdateEnemies(Enemies,closestEnemy.transform);
            return;
        }
        _rigidbody2D.velocity = Vector2.zero;
        closestEnemy.GetComponent<Enemy>().hp -= 3;
        closestEnemy.GetComponent<EnemyMove>().speed = 0;
        
    }

    Transform GetClosestEnemy(ArrayList lst)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Transform t in lst)
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

    ArrayList UpdateEnemies(ArrayList lst, Transform Enemy){
        int removed_ind = lst.IndexOf(Enemy);
        lst.RemoveAt(removed_ind);
        return lst;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            fight = true;
        }
    }
}
