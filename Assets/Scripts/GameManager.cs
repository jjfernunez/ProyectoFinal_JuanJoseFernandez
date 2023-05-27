using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int currentCoins;
    public int currentKills;
    public int currentTimePlayed;
    private int storedCoins;
    public static bool gameIsPaused;
    public TMP_Text coinUI;
    public TMP_Text killsUI;
    public GameObject LevelUpUI;
    private static int saveId;


    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        currentCoins = 0;
        currentKills = 0;
        LevelUpUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coinUI.text = currentCoins.ToString();
        killsUI.text = currentKills.ToString();
    }

    public static void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LevelUpEvent()
    {
        gameIsPaused = true;
        PauseGame();

        LevelUpUI.SetActive(true);
    }

    public static void LoadSave(int id)
    {
        saveId = id;
    }
}
