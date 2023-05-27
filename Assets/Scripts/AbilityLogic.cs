using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AbilityLogic : MonoBehaviour, IPointerClickHandler
{
    public GameObject selection;
    public int id;
    AbilityData data;
    GameObject DBManager;
    // Start is called before the first frame update
    void Start()
    {
        id = id - 1;
        DBManager = GameObject.Find("DBManager");
        data = DBManager.GetComponent<SelectAbilities>().ability.data[id];
        if (data.abilityLevel > 0)
        {
           for(int i = 0; i < data.abilityLevel; i++)
            {
                if(i == 5)
                {
                    break;
                }   
                this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.blue;
            }
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tocado " + id);
        data = DBManager.GetComponent<SelectAbilities>().ability.data[id];
        selection.transform.GetChild(0).gameObject.SetActive(true);
        selection.transform.GetChild(0).gameObject.transform.position = this.gameObject.transform.position;
        DBManager.GetComponent<SelectAbilities>().selectedAbility = this.id;
        selection.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().SetText(data.description + "      Cost: " + data.upgradeCost);
        
            
    }
   
}
