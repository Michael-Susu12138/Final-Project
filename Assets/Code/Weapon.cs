using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    GameObject target;
    public GameObject archer;

    public float speed;
    private float dist;
    private float nextX;
    private float baseY;
    private float height;
    private float archerX;
    private float targetX;
    Vector3 movePosition;
    public GameObject weaponPoint;

    // Enemy enemy;

    void Start() {
        target = GetClosestEnemy();
    }

    void FixedUpdate() {
        // https://weeklyhow.com/how-to-make-arrow-projectile-in-2d/#Scripting_Projectiles
        if (target != null) {
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
                transform.position = weaponPoint.transform.position;
                transform.rotation = Quaternion.Euler(0,0,0);
                transform.localScale = archer.transform.localScale;
            }
        }

        else {
            transform.position = weaponPoint.transform.position;
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.localScale = archer.transform.localScale;
            
            target = GetClosestEnemy();
        }

        target = GetClosestEnemy();
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
        Vector3 position = archer.transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        
        if (distance < 10) {
            return closest;
        }

        else {
            return null;
        }
    }
}
