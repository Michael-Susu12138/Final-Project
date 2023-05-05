using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int health = 15;
    public int gold;
    int waves = 0;
    public bool lastLevel = false;
    public string levelToLoad;
    TMP_Text goldUI;
    TMP_Text healthUI;
    TMP_Text waveUI;
    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        goldUI = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<TextMeshProUGUI>();
        waveUI = GameObject.FindGameObjectWithTag("WaveUI").GetComponent<TextMeshProUGUI>();
        gold = 2000;
        Debug.Log(gold);
        goldUI.text = "GOLD: " + gold;
        healthUI.text = "HEALTH: " + health.ToString() + "/15";
        waveUI.text = "WAVES: " + waves + "/3";
    }
    public void reduceHealth(int val) {
        health -= val;
        healthUI.text = "HEALTH: " + health.ToString() + "/15";
    }
    public void addGold(int val){
        gold += val;
        goldUI.text = "GOLD: " + gold;
    }
    public void useGold(int val){
        gold -= val;
        goldUI.text = "GOLD: " + gold;
    }
    public void increaseWave(){
        waves += 1;
        waveUI.text = "WAVES: " + waves + "/3";
    }

    void Update()
    {
        if(waves>=3){
            lastLevel = true;
        }
    }
}
