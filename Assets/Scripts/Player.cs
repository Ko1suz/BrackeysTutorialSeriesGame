using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    
    private void Start() {
       
        playerStats = PlayerStats.instance;
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("PANIC NO AUDIO MANAGER IN SCENE");
        }
        deathSoundName = "DeathVoice";
        hurtSoundName = "Grunt";
        PlayerStats.instance.currnetHealth =  PlayerStats.instance.maxHealth;
        //PlayerStats.instance.healthBar.SetHealth( PlayerStats.instance.maxHealth);
        // playerStats.currnetHealth = playerStats.maxHealth;
        // playerStats.healthBar.SetHealth(playerStats.maxHealth);

        InvokeRepeating("RegenHealth",1/playerStats.healthRegenRate,1/playerStats.healthRegenRate);
        InvokeRepeating("UpdateHealthBar",1,1);
    }
    // public PlayerStats playerStats = new PlayerStats(); 

    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string hurtSoundName = "Grunt";

    private AudioManager audioManager; 
    private PlayerStats playerStats;

   void RegenHealth(){
        playerStats.currnetHealth += 1;
        playerStats.healthBar.SetHealth(playerStats.currnetHealth);
    } 
   private void Update() {
       
       if (playerStats.healthBar==null)
       {
         playerStats.healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
       }
       playerStats.healthBar.SetHealth(playerStats.currnetHealth);
       if (transform.position.y<=-20)
       {
           DamagePlayer(999999);
       }
    //    if (playerStats.currnetHealth>playerStats.maxHealth)
    //    {
    //        playerStats.currnetHealth=playerStats.maxHealth;
    //    }
   }
    public void DamagePlayer(int damage){
        
        playerStats.currnetHealth  -= damage;
        Debug.Log(damage+" hasar yedinnnnnnnnnnnnnnnnnnnnn");
        playerStats.healthBar.SetHealth(playerStats.currnetHealth);
        if (playerStats.currnetHealth<=0)
        {
           AudioManager.instance.PlaySound(deathSoundName);
           GameMaster.KillPlayer(this);
        }
        else
        {
            AudioManager.instance.PlaySound(hurtSoundName);
            // audioManager.PlaySound(hurtSoundName);
        }
    }

    void UpdateHealthBar(){
        PlayerStats.instance.healthBar.SetHealth( PlayerStats.instance.maxHealth);
    }

}
