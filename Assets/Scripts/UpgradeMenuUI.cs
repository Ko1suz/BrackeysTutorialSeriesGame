using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeMenuUI : MonoBehaviour
{

    public GameObject upgradeMenuUI;
    public static bool upgradeMenuUIActive = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && GameMaster.gameOverStat == false&& PauseMenuUI.PauseMenuActive==false)
        {
            if (upgradeMenuUI.activeInHierarchy)
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
        upgradeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenuUI.GameIsPaused = false;
        upgradeMenuUIActive = false;
    }

    void Pause(){
        upgradeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenuUI.GameIsPaused = true;
        upgradeMenuUIActive = true;
    }

    
}
