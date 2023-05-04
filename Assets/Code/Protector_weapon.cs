using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector_weapon : MonoBehaviour
{
    Rigidbody2D _rigidbody2d;
    Enemy enemy;
    void Start() {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            enemy = other.GetComponent<Enemy>();  //?
            enemy.TakeDamage(5);
            //Destroy(other.gameObject);
        }
    }


}
