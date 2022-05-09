using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool PauseMenuActive = false;
    public GameObject pauseMenuUI;

    [SerializeField] string mouseHoverSoundName = "ButtonHover";     
    [SerializeField] string buttonPressSoundName = "ButtonPress";
    AudioManager audioManager;

     private void Start() {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No audio manager in the scene ");
        }
    }

    public void Quit(){
        audioManager.PlaySound(buttonPressSoundName);
        Debug.LogWarning("OYUNDAN CIKILDI !!");
        Application.Quit();
    }

    public void MainMenu(){
        audioManager.PlaySound(buttonPressSoundName);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);  
    } 


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && GameMaster.gameOverStat == false&& UpgradeMenuUI.upgradeMenuUIActive==false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume(){
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            audioManager.PlaySound(buttonPressSoundName);
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenuActive = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        PauseMenuActive = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
     public void OnMouseOver() {
        audioManager.PlaySound(mouseHoverSoundName);
    }
}
