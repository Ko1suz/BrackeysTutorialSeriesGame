using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour
{
    private Text MoneyText;
    void Start()
    {
        MoneyText = GetComponent<Text>();
    }

    
    void Update()
    {
        MoneyText.text = "MONEY : "+GameMaster.Money.ToString();
    }
}
