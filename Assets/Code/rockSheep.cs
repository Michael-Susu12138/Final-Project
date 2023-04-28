using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSheep : MonoBehaviour
{
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy;
    bool inRange = false; 

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (inRange == true) {
            _animator.SetBool("isThrowing", true);
        }
        else {
            _animator.SetBool("isThrowing", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            inRange = true;
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
