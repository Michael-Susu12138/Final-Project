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
    void Start()
    {
        currentHealth = hp;
        healthBar.SetMaxHealth(hp);
        audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // if(hp < 0){
        //     Destroy(gameObject);

        // }
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        audioSource.PlayOneShot(destroySound);
        if(currentHealth<=0){
            
            
            if(_gameManager.lastLevel){
                SceneManager.LoadScene(nextLevelToLoad);
            }
            _gameManager.addGold(20);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("killZone")){
            _gameManager.addGold(20);
            if (_gameManager.lastLevel && _gameManager.health > 0){
                SceneManager.LoadScene(nextLevelToLoad);
            }
            _gameManager.reduceHealth(1);
            Destroy(gameObject);
            if(_gameManager.health <= 0){
                SceneManager.LoadScene("Fail");
            }
        }
    }
    
}
