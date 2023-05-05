using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSheep_new : MonoBehaviour
{
    public GameObject rock;
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    public GameObject closestEnemy = null;
    public bool inRange = false; 

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        rock.SetActive(false);
    }

    void Update() {
        if (inRange == true) {
            _animator.SetBool("isThrowing", true);

            if (closestEnemy is not null){
                if (closestEnemy.transform.position.x < transform.position.x){
                    transform.localScale = new Vector2(-1,1);
                } 
                
                else {
                    transform.localScale = new Vector2(1,1);
                }
            }
            // Instantiate(rock, transform.position, transform.rotation);
            rock.SetActive(true);
        }
        else {
            _animator.SetBool("isThrowing", false);
            if (rock != null) {
                rock.SetActive(false);
            }

            closestEnemy = GetClosestEnemy();
            Vector3 diff = closestEnemy.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            print(curDistance);
            if (curDistance < 10) {
                inRange = true;
            }
            else {
                closestEnemy = null;
            }
        }
    }

    public GameObject getEnemy() {
        return closestEnemy;
    }

    GameObject GetClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            closestEnemy = other.gameObject;
            inRange = true;
            // last = StartCoroutine(Throw());
            // print("start");

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (inRange == true) {
            inRange = false;
        }
    }
  
    /*
    IEnumerator Throw() {
        while (true) {
            _animator.SetBool("isThrowing", true);
            yield return new WaitForSeconds(0.05f);
            _animator.SetBool("isThrowing", false);
        }
    }
    */
}
