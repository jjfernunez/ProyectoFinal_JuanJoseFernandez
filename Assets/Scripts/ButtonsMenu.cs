using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    public GameObject charaSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void PlayButton()
    {
        this.gameObject.SetActive(false);
        charaSelect.SetActive(true);
    }

    public void AbilityButton()
    {
        SceneManager.LoadScene("Upgrades");
    }
}
