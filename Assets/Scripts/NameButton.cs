using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameButton : MonoBehaviour
{
    public GameObject save;
    public TMP_InputField input;
    public GameObject DBManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveName() {

        

        DBManager.GetComponent<CreateSave>().CreateNewSave(save.GetComponent<ShowData>().id, input.text, "00:00:00", 0, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
