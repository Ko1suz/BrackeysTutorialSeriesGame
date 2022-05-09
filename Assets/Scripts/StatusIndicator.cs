using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarRect;

    private void Start() {
        if (healthBarRect == null)
        {
            Debug.LogError("Health barı bulamadım");
        }
    }


    public void SetHealth(int curHealth, int maxHealth){
        float value = (float)curHealth/maxHealth;
        healthBarRect.localScale = new Vector3(value,  healthBarRect.localScale.y,  healthBarRect.localScale.z);

    }
}
