using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
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

    public void Retry(){
        audioManager.PlaySound(buttonPressSoundName);
        SceneManager.LoadScene(1);  
        GameMaster.gameOverStat = false;
    }

    public void OnMouseOver() {
        audioManager.PlaySound(mouseHoverSoundName);
    }
}
