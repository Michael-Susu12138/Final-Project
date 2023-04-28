using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSheep : MonoBehaviour
{
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy = null;
    bool inRange = false; 

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (inRange == true) {
            _animator.SetBool("isThrowing", true);
            if(closestEnemy is not null){
                if (closestEnemy.transform.position.x < transform.position.x){
                transform.localScale = new Vector2(-1,1);
                } else {
                    transform.localScale = new Vector2(1,1);
                }
            }
            
        }
        else {
            _animator.SetBool("isThrowing", false);
        }
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
