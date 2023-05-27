using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour, IPointerClickHandler
{
    public int idCharacter;
    public bool unlocked;
    public int cost;
    public TMP_Text costUI;
    // Start is called before the first frame update
    void Start()
    {
        if (costUI != null) {
            costUI.SetText("" + cost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (unlocked)
        {
            PlayerPrefs.SetInt("idCharacter", idCharacter);
            SceneManager.LoadScene("Gameplay");
        }
        else
        {
            if(PlayerPrefs.GetInt("money") > cost)
            {
                PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") - cost));
                StartCoroutine(UpdateMoneySaved((PlayerPrefs.GetInt("money"))));
            }
        }


    }

    IEnumerator UpdateMoneySaved(int moneySaved)
    {
        string phpURL = "http://localhost/UpdateMoneySaved.php";
        // Crear un nuevo formulario para enviar los datos
        WWWForm form = new WWWForm();
        form.AddField("moneySaved", moneySaved);

        // Enviar los datos al script PHP
        using (UnityWebRequest www = UnityWebRequest.Post(phpURL, form))
        {
            yield return www.SendWebRequest();

            // Verificar si hubo un error en la comunicación
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error de conexión: " + www.error);
                yield break;
            }
        }
    }
}
