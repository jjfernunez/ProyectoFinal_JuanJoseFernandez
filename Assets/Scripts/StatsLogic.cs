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
    public GameObject DB;

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
    public float currentArmor;
    public float currentAttack;
    public float currentPullStrength;


    private void Awake()
    {
        playerLevel = 1;
        expIncrease = 10;
        maxExp = 100;
        currentExp = 0;

        StartCoroutine(InitializeStats());
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
        currentArmor = armor.GetValue();
        currentAttack = attack.GetValue();
        currentPullStrength = pullStrength.GetValue();
        

    }

    public IEnumerator InitializeStats()
    {
        yield return StartCoroutine(DB.GetComponent<GetCharacterStats>().GetAbilityFromDatabase());
        armor = new Stat("armor", DB.GetComponent<GetCharacterStats>().array.data[0].armor);
        speed = new Stat("speed", DB.GetComponent<GetCharacterStats>().array.data[0].speed);
        attack = new Stat("attack", DB.GetComponent<GetCharacterStats>().array.data[0].damage);
        pullStrength = new Stat("pullStrength", DB.GetComponent<GetCharacterStats>().array.data[0].pullStrength);
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

    internal void IncreaseStat(string statToIncrease, float increaseAmount)
    {
        switch (statToIncrease)
        {
            case "armor":
                armor.SetValue(armor.GetValue() + (armor.GetValue() * increaseAmount));
                break;
            case "speed":
                speed.SetValue(speed.GetValue() + (speed.GetValue() * increaseAmount));
                break;
            case "attack":
                attack.SetValue(attack.GetValue() + (attack.GetValue() * increaseAmount));
                break;
            case "pullStrength":
                pullStrength.SetValue(pullStrength.GetValue() + (pullStrength.GetValue() * increaseAmount));
                break;
        }
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
