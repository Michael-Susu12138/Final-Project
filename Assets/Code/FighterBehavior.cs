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
    public int damage = 1;
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
        if (Vector2.Distance(transform.position,GetClosestEnemy().transform.position) < lookDst){
            if(GetClosestEnemy().transform.position.x > transform.position.x && transform.localScale.x < 0 || 
                GetClosestEnemy().transform.position.x < transform.position.x && transform.localScale.x > 0){
                    transform.localScale *= new Vector2(-1,1);
                }

            Vector2 dir = (GetClosestEnemy().transform.position - transform.position);
            
            _rigidbody2D.velocity = new Vector2(dir.normalized.x * x_speed, dir.normalized.y*y_speed);
            
        }
    }
    void attack(){
        var closestEnemy = GetClosestEnemy();
        Enemy closestEnemyScript = closestEnemy.GetComponent<Enemy>();  //?
        closestEnemyScript.TakeDamage(damage);
    }

    // Transform GetClosestEnemy(ArrayList lst)
    // {
    //     Transform tMin = null;
    //     float minDist = Mathf.Infinity;
    //     Vector2 currentPos = transform.position;
    //     foreach (Transform t in lst)
    //     {
    //         float dist = Vector2.Distance(t.position, currentPos);
    //         if (dist < minDist)
    //         {
    //             tMin = t;
    //             minDist = dist;
    //         }
    //     }
    //     return tMin;
    // }

    GameObject GetClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = gameObject.transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        
        if (distance < lookDst) {
            return closest;
        }
        else {
            return null;
        }
    }

    // ArrayList UpdateEnemies(ArrayList lst, Transform Enemy){
    //     int removed_ind = lst.IndexOf(Enemy);
    //     lst.RemoveAt(removed_ind);
    //     return lst;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            fight = true;
        }
    }
}
