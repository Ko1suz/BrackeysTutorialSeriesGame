using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerStats : MonoBehaviour
    {
         public float speed = 5;
         public float jumpForce = 10;
        public static PlayerStats instance;
        // public int health = 100;
        public int maxHealth = 100;
        public int _currnetHealth;
        public float fireRate = 5f;
        public int damage = 25;
        public int currnetHealth{
            get{ return _currnetHealth;}
            set{_currnetHealth = Mathf.Clamp(value,0,maxHealth);}
        }
        
        public HealthBar healthBar;
        public float healthRegenRate = 2f;

         private void Awake() 
         {
             if (instance==null)
             {
                 instance = this;
             }
            //  currnetHealth =maxHealth;
         }
    } 

   
