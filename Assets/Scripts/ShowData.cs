using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShowData : MonoBehaviour, IPointerClickHandler
{
    public int id = 0;
    public TMP_Text name;
    public TMP_Text time_played;
    public int money;
    public int kills;
    public GameObject DBManager;
    public GameObject saveNameInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (name.text == "Nueva Partida")
        {
            saveNameInput.SetActive(true);
            saveNameInput.GetComponent<NameButton>().save = this.gameObject;
            
        }
        else
        {
            PlayerPrefs.SetInt("id", id);
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("kills", kills);
            SceneManager.LoadScene("InGameMenu");
        }
    }
}
