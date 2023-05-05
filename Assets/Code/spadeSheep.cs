using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spadeSheep : MonoBehaviour
{
    public Animator _animator;
    public GameObject spadePrefab;
    public float throwRate = 2f;
    public Transform spadeSpawnPoint;

    Rigidbody2D _rigidbody2d;
    GameObject closestEnemy;
    bool inRange = false;
    float nextThrowTime;
    public float attackSpeed = 1f;
    public int levelAS;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        SetAttackSpeed(levelAS);

    }

    void FixedUpdate()
    {
        if (inRange == true)
        {
            _animator.SetBool("isThrowing", true);
            if (Time.time > nextThrowTime)
            {
                ThrowSpade();
                nextThrowTime = Time.time + throwRate;
            }
        }
        else
        {
            _animator.SetBool("isThrowing", false);
        }
    }

    private Coroutine throwSpadeRoutine;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            inRange = true;

            if (other.transform.position.x < transform.position.x){
                transform.localScale = new Vector2(-1,1);
            } else {
                transform.localScale = new Vector2(1,1);
            }

            // Start the ThrowSpadeRoutine
            if (throwSpadeRoutine == null) {
                throwSpadeRoutine = StartCoroutine(ThrowSpadeRoutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (inRange == true) {
            inRange = false;

            // Stop the ThrowSpadeRoutine
            if (throwSpadeRoutine != null) {
                StopCoroutine(throwSpadeRoutine);
                throwSpadeRoutine = null;
            }
        }
    }

    public void SetAttackSpeed(int level)
    {
        switch (level)
        {
            case 1:
                attackSpeed = 1f; // Attack speed for level 1
                break;
            case 2:
                attackSpeed = 0.8f; // Attack speed for level 2
                break;
            case 3:
                attackSpeed = 0.6f; // Attack speed for level 3
                break;
            default:
                attackSpeed = 1f;
                break;
        }
    }


    void ThrowSpade()
    {
        Quaternion spadeRotation;
        Vector2 spadeVelocity;

        if (transform.localScale.x < 0)
        {
            spadeRotation = Quaternion.Euler(0, 180, 0);
            spadeVelocity = new Vector2(-spadePrefab.GetComponent<SpadeProjectile>().speed, 0);
        }
        else
        {
            spadeRotation = Quaternion.identity;
            spadeVelocity = new Vector2(spadePrefab.GetComponent<SpadeProjectile>().speed, 0);
        }

        GameObject spade = Instantiate(spadePrefab, spadeSpawnPoint.position, spadeRotation);
        spade.GetComponent<SpadeProjectile>().rb.velocity = spadeVelocity;
    }
    IEnumerator ThrowSpadeRoutine()
    {
        while (inRange)
        {
            ThrowSpade();
            yield return new WaitForSeconds(attackSpeed);
        }
    }


}
