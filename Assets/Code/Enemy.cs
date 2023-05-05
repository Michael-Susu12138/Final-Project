using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 9; //hp is the max health of this enemy
    public int currentHealth;
    public HealthBar healthBar;
    public AudioClip destroySound;
    public float soundDelay;
    AudioSource audioSource;
    void Start()
    {
        currentHealth = hp;
        healthBar.SetMaxHealth(hp);
        audioSource = GetComponent<AudioSource>();
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
            
            Destroy(gameObject);
        }
    }
    
}
