using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 9;
    public HealthBar healthBar;
    void Start()
    {
        healthBar.SetMaxHealth(hp);
    }

    void Update()
    {
        if(hp < 0){
            Destroy(gameObject);

        }
    }
    
}
