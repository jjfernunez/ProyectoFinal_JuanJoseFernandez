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
    [Header("Starting Stats")]

    public float initialArmor;
    public float initialSpeed;
    public float initialAttack;
    public float initialPullStrength;

    [Header ("Current Stats")]

    public Stat armor;
    public Stat speed;
    public Stat attack;
    public Stat pullStrength;
    public float currentSpeed;

    private void Awake()
    {
        playerLevel = 1;
        expIncrease = 10;
        maxExp = 100;
        currentExp = 0;


        armor = new Stat("armor", initialArmor);
        speed = new Stat("speed", initialSpeed);
        attack = new Stat("attack", initialAttack);
        pullStrength = new Stat("pullStrength", initialPullStrength);
    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        expBar.fillAmount = currentExp / maxExp;

        levelText.text = playerLevel.ToString();

        currentSpeed = speed.GetValue();

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

public class Stat
{
    public string name;
    public float value;

    public Stat(string name, float value)
    {
        this.name = name;
        this.value = value;
    }

    public float GetValue(){ return value; }

    public void SetValue(float value) { this.value = value; }
}
