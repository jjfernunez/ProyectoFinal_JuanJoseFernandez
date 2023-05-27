using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsLogic : MonoBehaviour
{
    public int maxExp;
    public float currentExp;

    public Image expBar;

    public int expIncrease;
    public int playerLevel;
    public GameManager gm;

    public TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 1;
        expIncrease = 10;
        maxExp = 100;
        currentExp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        expBar.fillAmount = currentExp / maxExp;

        levelText.text = playerLevel.ToString();
    }

    public void CalculateExpIncrease(int exp)
    {
        
        currentExp += exp;
       
        expBar.fillAmount = (currentExp / maxExp)/100;
        if (currentExp >= maxExp)
        {
            LevelUp();

        }

       
    }

    private void LevelUp()
    {
        maxExp += expIncrease;
        currentExp = 0;
        playerLevel += 1;
        gm.LevelUpEvent();
    }

}
