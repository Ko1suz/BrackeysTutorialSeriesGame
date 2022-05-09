using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string hoverOverSound = "ButtonHover";
    [SerializeField] string pressButtonSound = "ButtonPress";  
    AudioManager audioManager;
    private void Start() {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audiomanager found");
        }
    }
   public void PlayGame(){
       audioManager.PlaySound(pressButtonSound);
       SceneManager.LoadScene(1);
       if (PauseMenuUI.GameIsPaused)
       {
           PauseMenuUI.GameIsPaused = false;
       }
   }
   public void QuitGame(){
       audioManager.PlaySound(pressButtonSound);
       Debug.LogWarning("ÇIKIS YAPILDI");
       Application.Quit();
   }

   public void OnMouseOver() {
       audioManager.PlaySound(hoverOverSound);
   }

}
