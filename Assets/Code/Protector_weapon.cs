using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protector_weapon : MonoBehaviour
{
    Rigidbody2D _rigidbody2d;

    void Start() {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            
            Destroy(other.gameObject);
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     if (collision.collider.gameObject.tag == "Enemy") {
            
    //         Destroy(collision.gameObject);
    //     }
    // }
}
