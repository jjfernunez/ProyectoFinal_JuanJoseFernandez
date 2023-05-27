using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UITimer : MonoBehaviour
{
    public TMP_Text TimerText;
    public bool playing;
    private float Timer;
    

    private void Start()
    {
        playing = true;
    }

    void Update()
    {

        if (playing == true)
        {

            Timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60F);
            int seconds = Mathf.FloorToInt(Timer % 60F);
            int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

            if (minutes.Equals(30))
            {
                int moneySaved = PlayerPrefs.GetInt("money");
                int amountOfKills = PlayerPrefs.GetInt("kills");

                GameObject gm = GameObject.Find("GameManager");
                StartCoroutine(SendData(moneySaved + gm.GetComponent<GameManager>().currentCoins, amountOfKills + gm.GetComponent<GameManager>().currentKills));
                SceneManager.LoadScene("GameOver");
            }
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
