using UnityEngine.UI;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] WaveSpawner spawner;

    [SerializeField] Animator waveAnim;

    [SerializeField] Text waveCountdownText;

    [SerializeField] Text waveCountText;

    private WaveSpawner.SpawnState previousState;

    void Start()
    {
        if (spawner == null)
        {
            Debug.LogWarning("Spawner referansı yok");
            this.enabled = false;
        }
        if (waveAnim == null)
        {
            Debug.LogWarning("Spawner referansı yok");
            this.enabled = false;
        }
        if (waveCountdownText == null)
        {
            Debug.LogWarning("Spawner referansı yok");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogWarning("Spawner referansı yok");
            this.enabled = false;
        } 
    }

    void Update()
    {
        switch(spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;   
        }

        previousState =  spawner.State;
    }

    void UpdateCountingUI(){
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnim.SetBool("WaveIncoming",false);
            waveAnim.SetBool("WaveCountdown",true);

            //Debug.Log("COUNTING");
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
        
    }
    void UpdateSpawningUI(){
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnim.SetBool("WaveCountdown",false);
            waveAnim.SetBool("WaveIncoming",true );
            waveCountText.text = spawner.NextWave.ToString();
            //Debug.Log("SPAWNING");
            
        }
    }
}
