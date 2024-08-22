using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    public int currentGold = 20;
    public TMP_Text goldText;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        UpdateUI();

    }

    public void AddGold(int qty)
    {
        currentGold += qty;
        UpdateUI();
    }

    public void QuitGold(int qty)
    {
        currentGold -= qty;

        //Player has no gold
        if (currentGold <= 0)
        {
            currentGold = 0;
            GameManager.instance.GameOver();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        //Updates gold counter in UI
        goldText.text = $"x {currentGold}";
    }
}
