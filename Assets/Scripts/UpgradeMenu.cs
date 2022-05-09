using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private Text healthText;    
    [SerializeField] private Text speedText;    
    [SerializeField] private Text jumpText;    
    [SerializeField] private Text damageText;    
    [SerializeField] private Text fireRateText;
    [SerializeField] private int upgradeCost = 50;
    

    PlayerStats playerStats;
    private void Start() {
        
    }

    private void OnEnable() {
        playerStats = PlayerStats.instance;
        UpdateValues();
    }

    void UpdateValues(){
        healthText.text = "Health : "+playerStats.maxHealth.ToString();
        speedText.text = "Speed : "+playerStats.speed.ToString();
        jumpText.text = "Jump : "+playerStats.jumpForce.ToString();
        damageText.text = "Damage : "+playerStats.damage.ToString();
        fireRateText.text = "Fire Rate : "+playerStats.fireRate.ToString();
    }    

    public void UpgradeHealth(){
        if (GameMaster.Money<upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        GameMaster.Money-= upgradeCost;
        playerStats.maxHealth +=10;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
     public void UpgradeSpeed(){
        if (GameMaster.Money<upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        GameMaster.Money-= upgradeCost;
        playerStats.speed +=2.5f;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
     public void UpgradeJump(){
        if (GameMaster.Money<upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        GameMaster.Money-= upgradeCost;
        playerStats.jumpForce +=2;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
     public void UpgradeDamage(){
        if (GameMaster.Money<upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        GameMaster.Money-= upgradeCost;
        playerStats.damage +=2;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
     public void UpgradeFireRate(){
        if (GameMaster.Money<upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        GameMaster.Money-= upgradeCost;
        playerStats.fireRate +=2;
        UpdateValues();
        AudioManager.instance.PlaySound("Money");
    }
}
