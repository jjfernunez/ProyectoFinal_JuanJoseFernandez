using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UpgradeLogic : MonoBehaviour, IPointerClickHandler
{
    public int idCharacter;
    public string upgradeName;
    public string statToIncrease;
    public float increaseAmount;
    public string description;
    public string image;
    public GameManager manager;
    public GameObject DB;


    [Header ("UI Elements")]
    public TMP_Text nameUpgrade;
    public TMP_Text desc;
    public UnityEngine.UI.Image imageSprite;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        StartCoroutine(GetData());
        
    }

    public IEnumerator GetData()
    {
        int randomNumber = UnityEngine.Random.Range(0, 4);
        Debug.Log(randomNumber);


        DB.GetComponent<GetUpgrades>().idCharacter = PlayerPrefs.GetInt("idCharacter");
        yield return StartCoroutine(DB.GetComponent<GetUpgrades>().GetAbilityFromDatabase());
        
        this.upgradeName = DB.GetComponent<GetUpgrades>().array.data[randomNumber].upgradeName;
        Debug.Log("Nombre de upgrade: " + upgradeName);
        this.statToIncrease = DB.GetComponent<GetUpgrades>().array.data[randomNumber].statToIncrease;
        increaseAmount = DB.GetComponent<GetUpgrades>().array.data[randomNumber].increaseAmount;
        this.description = DB.GetComponent<GetUpgrades>().array.data[randomNumber].description;
        this.image = DB.GetComponent<GetUpgrades>().array.data[randomNumber].image;

        nameUpgrade.SetText(upgradeName);
        desc.SetText(description);
      

        var tex = new Texture2D(64, 64);
        byte[] bytes = System.Convert.FromBase64String(this.image);
        tex.LoadImage(bytes);
        tex.Apply();
        imageSprite.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0f, 0f));
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        GameObject.Find("Player").GetComponent<StatsLogic>().IncreaseStat(statToIncrease, increaseAmount);
        GameManager.gameIsPaused = false;
        GameManager.PauseGame();
        if(!manager.LevelUpUI.activeInHierarchy)
            manager.LevelUpUI.SetActive(true);
        else
        {
            manager.LevelUpUI.SetActive(false);
        }
    }
}
