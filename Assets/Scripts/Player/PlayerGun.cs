using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;
    public int diagWeaponLvl = 0;
    public int maxDamagePerShot = 200;
    public Text powerText;

    void Awake()
    {
        powerText.text = + damagePerShot + "/" + maxDamagePerShot;
    }

    public void GainPower(int amount) 
    {
        damagePerShot += amount;
        if (damagePerShot > maxDamagePerShot) {
            damagePerShot = maxDamagePerShot;
        }
        
        powerText.text = + damagePerShot + "/" + maxDamagePerShot;
//        Debug.Log(damagePerShot);
    }

    public void FasterWeapon()
    {
        if (timeBetweenBullets > 0.03f){
            timeBetweenBullets -= 0.02f;
        }
    }

    public void AddDiagWeaponLvl()
    {
        if (diagWeaponLvl < 2){
            diagWeaponLvl += 1;
        }
    }
}