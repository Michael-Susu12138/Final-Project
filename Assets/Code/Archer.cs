using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject weapon;
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy = null;
    bool thrown = false;
    Vector3 weaponPosition;
    public GameObject weaponPoint;
    public int range;

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        weaponPosition = weaponPoint.transform.position;
        closestEnemy = GetClosestEnemy();
    }

    void Update() {
        if (closestEnemy != null) {
            if (closestEnemy.transform.position.x < transform.position.x){
                transform.localScale = new Vector2(-1,1);
            } 
            
            else {
                transform.localScale = new Vector2(1,1);
            }

            thrown = true;
        }

        if (weapon.transform.position == weaponPosition) {
            thrown = false;
        }

        closestEnemy = GetClosestEnemy();

        if ((thrown == false) && (weapon.transform.position == weaponPosition) && (closestEnemy != null)) {
            _animator.SetBool("isThrowing", true);
        }
        else {
            _animator.SetBool("isThrowing", false);
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

        if (distance < range) {
            return closest;
        }

        else {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            closestEnemy = other.gameObject;
        }
    }
}
