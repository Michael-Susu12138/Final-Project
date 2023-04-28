using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spadeSheep : MonoBehaviour
{
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy;
    bool inRange = false; 
    Coroutine last = null;

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        // closestEnemy = FindClosestEnemy();
        // StartCoroutine(Throw());
    }

    void FixedUpdate() {
        if (inRange == true) {
            last = StartCoroutine(Throw());
            print("start");
        }
        else {
        // else if (last != null) {
            // StopCoroutine(last);
            StopAllCoroutines();
            print("stop");
        }
        // if ((last != null) && (inRange == false)) {
        //     StopCoroutine(last);
        //     last = null;
        //     print("stop");
        // }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
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
  
    IEnumerator Throw() {
        while (true) {
            _animator.SetBool("isThrowing", true);
            yield return new WaitForSeconds(0.003f);
            _animator.SetBool("isThrowing", false);
        }
    }
}
