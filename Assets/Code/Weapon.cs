using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator _animator;
    public GameObject target;
    public GameObject archer;

    public float speed = 10f;
    private float dist;
    private float nextX;
    private float baseY;
    private float height;
    private float archerX;
    private float targetX;
    public Vector3 movePosition;
    public Vector3 spawnPosition;
    // public bool thrown;

    // Enemy enemy;

    void Start() {
        // thrown = false;
        _animator = GetComponent<Animator>();
        target = GetClosestEnemy();
        spawnPosition = transform.position;
    }

    void Update() {
        // https://weeklyhow.com/how-to-make-arrow-projectile-in-2d/#Scripting_Projectiles
        if (target != null) {
            // if (!thrown) {
                // thrown = true;

                targetX = target.transform.position.x;
                archerX = archer.transform.position.x;

                dist = targetX - archerX;
                nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
                baseY = Mathf.Lerp(archer.transform.position.y, target.transform.position.y, (nextX - archerX) / dist);
                height = 2 * (nextX - archerX) * (nextX - targetX) / (-0.25f * dist * dist);

                movePosition = new Vector3(nextX, baseY + height, transform.position.z);

                transform.rotation = LookAtTarget(movePosition - transform.position);
                transform.position = movePosition;

                if (transform.position == target.transform.position) {
                    transform.position = spawnPosition;
                    // thrown = false;
                }
            // }
        }
        else {
            print("get");
            target = GetClosestEnemy();
            // thrown = false;
        }
    }

    public static Quaternion LookAtTarget(Vector2 rotation) {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            // enemy = other.GetComponent<Enemy>();  //?
            // enemy.TakeDamage(5);
            Destroy(other.gameObject);
            target = null;
        }
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
}
