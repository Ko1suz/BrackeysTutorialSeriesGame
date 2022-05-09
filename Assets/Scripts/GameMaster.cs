using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField] private int maxLives = 3;
    [SerializeField]private int startingMoney;
    public static int Money;
    private static int _remainingLives = 3; 
    public static int RemainingLives
    {
        get{ return _remainingLives; }
    }
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay =2;
    public Transform spawnPrefab;
    public string ReSpawnCountDownSoundName ="RespawnCountdown";
    public string SpawnSoundName = "Spawn";

    public string gameOverSoundName = "GameOver";
    // public Transform enemyDeathParticles;
    public CameraShake cameraShake;
    [SerializeField] private GameObject gameOverUI;
    // [SerializeField] private GameObject upgradeMenuUI;

    
    private AudioManager audioManager; 

    public static bool gameOverStat = false;
    private void Awake() {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    private void Start() {
         if (cameraShake== null)
         {
             Debug.LogWarning("KAMERASHAKE EFFEKTİ GM İÇİNDE REFERANSI YOK");
         }
         _remainingLives = maxLives;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No audio manager in the scene ");
        }
        Money = startingMoney;  
        

     }

    public void EndGame(){
        audioManager.PlaySound(gameOverSoundName);
        Debug.LogWarning("OYUNU KAYBETTİN KANCIKKKK");
        gameOverUI.SetActive(true);
        gameOverStat = true;
    }

    public IEnumerator ReSpawnPlayer(){

        audioManager.PlaySound(ReSpawnCountDownSoundName);   
        yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(SpawnSoundName);   
        Instantiate(playerPrefab,spawnPoint.position,spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab,spawnPoint.position,spawnPoint.rotation).gameObject;
        Destroy(clone,3f);
        StopAllCoroutines();

    }
    public static void KillPlayer(Player player){
        Destroy(player.gameObject);
        _remainingLives --;
        if (_remainingLives<=0)
        {
            gm.EndGame();    
        }
        else
        {
            gm.StartCoroutine(gm.ReSpawnPlayer());
        }
    }
    public static void KillEnemy(EnemyS enemyS){
        gm._KillEnemey(enemyS);
        GameMaster.Money+=10;
        AudioManager.instance.PlaySound("Money");
    }
    public void _KillEnemey(EnemyS _enemyS){
        //Lets play some sounds
        audioManager.PlaySound(_enemyS.deathSoundName);

        //Add particles
        GameObject _clone = Instantiate(_enemyS.deathParticles,_enemyS.transform.position,Quaternion.identity).gameObject;
        Destroy(_clone, 4f);

        //Go cameraShake
        cameraShake.Shake(_enemyS.shakeAmount,_enemyS.shakeLenght);
        Destroy(_enemyS.gameObject);
    }
}
