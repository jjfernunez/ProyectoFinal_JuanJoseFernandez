using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class UpgradeLogic : MonoBehaviour, IPointerClickHandler
{
    public string statToUpgrade;
    public int upgradeLevel;
    public float upgradeAmount;
    public string upgradeDesc;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        print("I was clicked");
        GameManager.gameIsPaused = false;
        GameManager.PauseGame();
        manager.LevelUpUI.SetActive(false);
    }
}
