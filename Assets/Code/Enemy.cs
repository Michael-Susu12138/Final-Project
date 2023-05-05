using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int hp = 9; //hp is the max health of this enemy
    public int currentHealth;
    public HealthBar healthBar;
    public AudioClip destroySound;
    public float soundDelay;
    AudioSource audioSource;
    GameManager _gameManager;
    public string nextLevelToLoad;
    float speed;
    public FighterBehavior attacker; // Added attacker variable

    void Start()
    {
        currentHealth = hp;
        healthBar.SetMaxHealth(hp);
        audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        speed = GetComponent<WolverineMove>().speed;
    }

    void Update()
    {
        // If there's no attacker, move the enemy (add your enemy movement code here)
        if (attacker == null)
        {
            GetComponent<WolverineMove>().speed = speed;
        } else {
            GetComponent<WolverineMove>().speed = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        audioSource.PlayOneShot(destroySound);
        if (currentHealth <= 0)
        {
            if (_gameManager.lastLevel)
            {
                SceneManager.LoadScene(nextLevelToLoad);
            }
            _gameManager.addGold(20);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("killZone"))
        {
            _gameManager.addGold(20);
            if (_gameManager.lastLevel && _gameManager.health > 0)
            {
                SceneManager.LoadScene(nextLevelToLoad);
            }
            _gameManager.reduceHealth(1);
            Destroy(gameObject);
            if (_gameManager.health <= 0)
            {
                SceneManager.LoadScene("Fail");
            }
        }
    }

    // Added SetAttacker method
    public void SetAttacker(FighterBehavior chickenFighter)
    {
        attacker = chickenFighter;
    }
}
