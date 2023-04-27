using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSheep : MonoBehaviour
{
    public Animator _animator;
    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy;

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();

        closestEnemy = FindClosestEnemy();
        StartCoroutine(Throw());
    }

    void Update() {
        // StartCoroutine(Throw());
        // _animator.SetBool("isThrowing", false);
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
            _animator.SetBool("isThrowing", true);
            yield return new WaitForSeconds(0.05f);
            _animator.SetBool("isThrowing", false);
        }
    }
}
