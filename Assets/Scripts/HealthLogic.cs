using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthLogic : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public Canvas healthCanvas;
    public Image healthUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == 100)
        {
            healthCanvas.enabled = false;
        }
        else
        {
            healthCanvas.enabled = true;
        }

        if(currentHealth <= 0)
        {
            int moneySaved = PlayerPrefs.GetInt("money");
            int amountOfKills = PlayerPrefs.GetInt("kills");

            GameObject gm = GameObject.Find("GameManager");
            StartCoroutine(SendData(moneySaved + gm.GetComponent<GameManager>().currentCoins, amountOfKills + gm.GetComponent<GameManager>().currentKills));

            SceneManager.LoadScene("GameOver");
        }

        healthUI.fillAmount = currentHealth / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth--;
        }
    }

    IEnumerator SendData(int moneySaved, int amountOfKills)
    {
        // URL del script PHP en el servidor
        string url = "http://localhost/GameOverUpdate.php";

        // Crear un formulario con los datos a enviar
        WWWForm form = new WWWForm();
        form.AddField("moneySaved", moneySaved);
        form.AddField("amountOfKills", amountOfKills);

        // Enviar la petición POST al script PHP
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        // Verificar si ocurrió algún error
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al enviar los datos: " + www.error);
        }
        else
        {
            // Leer la respuesta del servidor
            string response = www.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + response);
        }

        www.Dispose();
    }
}
