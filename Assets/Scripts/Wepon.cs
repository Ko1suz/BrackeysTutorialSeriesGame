using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{

   
    public LayerMask whatToHit;
    public Transform MuzzleFlashPrefab;
    public Transform HitPrefab;
    public Transform BulletTrailPrefab;    

    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    float timeToFire =0;
    Transform firePoint;

    //Handle camera shaking 
    public float camShakeAmt =0.05f;
    public float camShakeLenght =0.07f;
    CameraShake camShake;

    public string weponShootSound = "DefaultShoot";

    //Caching
    AudioManager audioManager;
    


    
    
    private void Awake() {
        firePoint = transform.Find("FirePoint");    
        if (firePoint==null)
        {
            Debug.Log("fire point bulunamadı");
        }
        // myMove = GameObject.FindGameObjectWithTag("Player").GetComponent<MyMove>();

    }

    private void Start() {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
        {
            Debug.LogError("Kamerayı Sallanma effekti scriptini bulamadım");
        }
        audioManager = AudioManager.instance;
        if (audioManager==null)
        {
            Debug.LogError("No AudioManager found in scene..");
        }
    }
    
    void Update()
    {
        if (!PauseMenuUI.GameIsPaused)
        {
             if (PlayerStats.instance.fireRate==0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();            
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time>timeToFire)
            {
                timeToFire  = Time.time + 1/PlayerStats.instance.fireRate;
                Shoot();  
            }
        }
        }
       
    }

    void Shoot(){
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition,mousePosition-firePointPosition,100,whatToHit);
        
        // Debug.DrawLine(firePointPosition,(mousePosition-firePointPosition)*100,Color.cyan);
        if (hit.collider != null)
        {
            // Debug.DrawLine(firePointPosition, hit.point,Color.red);
            
            EnemyS enemyS = hit.collider.GetComponent<EnemyS>();
            if (enemyS != null)
            {
                 Debug.Log("We hit"+hit.collider.name+" and did "+PlayerStats.instance.damage +" damage.");
                enemyS.SetEnemyHealth(PlayerStats.instance.damage);
               
            }
        }
        if (Time.time>=timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;
            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition)*30 ;
                hitNormal = new Vector3(9999,9999,9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time +1/effectSpawnRate;
            // myMove.rb.velocity = new Vector2(myMove.rb.velocity.x,2);

        }

       
        
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal){
        Transform trail = Instantiate(BulletTrailPrefab,firePoint.position,firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.SetPosition(0,firePoint.position);
            lr.SetPosition(1,hitPos);   
        }

         Destroy(trail.gameObject,0.04f); 
         
         if (hitNormal != new Vector3(9999,9999,9999))
         {
            Transform hitParticle = Instantiate(HitPrefab,hitPos,Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitParticle.gameObject,.5f);
         }

        Transform clone = Instantiate(MuzzleFlashPrefab,firePoint.position,firePoint.rotation) as Transform; // as Transforn başına bunu  (Transform)  yazmakla ayı şey
        clone.parent = firePoint;
        float size = Random.Range(0.6f,0.9f);
        clone.localScale = new Vector3(size,size,1);
        Destroy(clone.gameObject,0.02f);

        //Shake the camera 
        camShake.Shake(camShakeAmt,camShakeLenght);

        //Play shoot sound
        audioManager.PlaySound(weponShootSound);
    }

}
