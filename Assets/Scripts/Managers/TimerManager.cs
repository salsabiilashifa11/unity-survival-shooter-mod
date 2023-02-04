using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float time = 0f;
    public PlayerHealth playerHealth;
    public Text timerText;
    public bool countDown;

    string timeStringFormat;
    TimeSpan timeSpan;
	WeaponUpgradeManager weaponUpgradeManager;
    
    void Start() {
        timerText.enabled = true;
		weaponUpgradeManager = GameObject.Find("WeaponUpgradeManager").GetComponent<WeaponUpgradeManager> ();
    }
    
    
    void Update()
    {
        if (playerHealth.currentHealth > 0 && time >= 0) {
            if (countDown) {
                time -= Time.deltaTime;
            } else {
                time += Time.deltaTime;
            }
            timeSpan = TimeSpan.FromSeconds((double)(new decimal(time)));
            timeStringFormat = timeSpan.ToString("hh':'mm':'ss");
//            Debug.Log(timeStringFormat);
            
            timerText.text = timeStringFormat;
            int timeInt = (int)time;
            Debug.Log(timeInt);
            PlayerPrefs.SetInt("time", timeInt);
            PlayerPrefs.SetString("timeString", timeStringFormat);
            PlayerPrefs.Save();

            if ((int)time % 30 == 0 && (int)time != 0){
			    weaponUpgradeManager.showButtons();
            }
            
        }
    }
    
    
    public void RestartTimer() {
        time = 0f;
    }
}

