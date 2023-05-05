using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBehavior : MonoBehaviour
{
    public float attackSpeed = 1f;
    public int damage = 3;
    public LayerMask enemyLayer;
    public float searchRadius = 10f;
    public float moveSpeed = 2f;
    public GameObject fighterPrefab;
    public float respawnTime = 3f;
    int health = 9;
    private Transform target;

    private void Start()
    {
        StartCoroutine(FindNearestEnemy());
        StartCoroutine(AttackNearestEnemy());
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
        if(health <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(respawnTime);
        Instantiate(fighterPrefab, transform.position, Quaternion.identity);
        
    }

    private IEnumerator FindNearestEnemy()
    {
        while (true)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, searchRadius, enemyLayer);

            if (enemiesInRange.Length > 0)
            {
                Collider2D nearestEnemy = enemiesInRange[0];
                float minDistance = Vector2.Distance(transform.position, nearestEnemy.transform.position);

                foreach (Collider2D enemy in enemiesInRange)
                {
                    Enemy enemyComponent = enemy.GetComponent<Enemy>();
                    if (enemyComponent.attacker != null) continue;

                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemy = enemy;
                    }
                }

                target = nearestEnemy.transform;
                nearestEnemy.GetComponent<Enemy>().SetAttacker(GetComponent<FighterBehavior>()); // Updated this line
            }
            else
            {
                target = null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }


    private IEnumerator AttackNearestEnemy()
    {
        while (true)
        {
            if (target != null)
            {
                float distance = Vector2.Distance(transform.position, target.position);
                if (distance <= 1f)
                {
                    target.GetComponent<Enemy>().TakeDamage(damage);
                    health-=2;
                }
            }

            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}
