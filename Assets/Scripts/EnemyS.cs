using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public int maxHealth = 100;
    public int currnetHealth;
    public HealthBar healthBar;
    public int hurtForce = 7;
    public int fireHitForce = 10;
    private Rigidbody2D rb;
    Player player;

    public Transform deathParticles;
    public float shakeAmount = 0.1f;
    public float shakeLenght = 0.1f;
    public string deathSoundName = "Explosion";

    private void Start() {
        currnetHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (deathParticles == null)
        {
            Debug.LogWarning("Ölüm PARÇACIKLARI");
        }
    }

    private void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().DamagePlayer(10); 
             if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //sola doğru hareket etmem lazım
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else //otomatik olarak burasıda düşmanın sağda olduğu durum
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
        }
    }
    public void SetEnemyHealth(int damage){
        currnetHealth-= damage;
        if (gameObject.transform.position.x > transform.position.x)
                {
                    //sola doğru hareket etmem lazım
                    rb.velocity = new Vector2(-fireHitForce, rb.velocity.y);
                }
                else //otomatik olarak burasıda düşmanın sağda olduğu durum
                {
                    rb.velocity = new Vector2(fireHitForce  , rb.velocity.y);
                }
        healthBar.SetHealth(currnetHealth);
        if (currnetHealth<=0)
        {
           GameMaster.KillEnemy(this);
        }
    }
}
