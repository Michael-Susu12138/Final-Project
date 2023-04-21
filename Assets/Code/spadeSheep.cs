using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spadeSheep : MonoBehaviour
{
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy;

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        closestEnemy = FindClosestEnemy();
    }

    void Update() {
        if (closestEnemy != null) {
            Vector3 difference = closestEnemy.transform.position - transform.position;
            
            while (difference.sqrMagnitude > 10) {
                print("move");
                if (((closestEnemy.transform.position.x > transform.position.x) && (transform.localScale.x < 0)) 
                    || (closestEnemy.transform.position.x < transform.position.x) && (transform.localScale.x > 0)) {
                        transform.localScale *= new Vector2(-1, 1);
                }
                
                _animator.SetBool("isMoving", true);
                Vector2 angleDirection = (closestEnemy.transform.position - transform.position);
                _rigidbody2d.velocity = angleDirection * 0.1f;           
            }
            
            _animator.SetBool("isMoving", false);
        }
    }

    public GameObject FindClosestEnemy() {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject each in enemies) {
            Vector3 difference = each.transform.position - position;
            float currDistance = difference.sqrMagnitude;
            if (currDistance < distance) {
                closest = each;
                distance = currDistance;
            }
        }
        return closest;
    }

    IEnumerator Throw() {
        while (true) {
            yield return new WaitForSeconds(10f);
            _animator.SetBool("isThrowing", true);
        }
    }
}
