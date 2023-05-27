using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CallSaveData : MonoBehaviour
{
    public int saveId;
    public GameObject[] savesObj;
    
    void Start()
    {
        StartCoroutine(CallSave());
    }
    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator CallSave()
    {
        // URL de la página PHP que devuelve los usuarios
        string url = "http://localhost/ServerLogin.php";

        // Realizar la petición GET a la página PHP
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // Verificar si ocurrió algún error
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al obtener los usuarios: " + www.error);
        }
        else
        {
            if (!string.IsNullOrEmpty(www.downloadHandler.text))
            {
                
                // Convertir el resultado de la petición a una lista de objetos JSON
                Saves data = JsonUtility.FromJson<Saves>("{\"saves\":" + www.downloadHandler.text + "}");
                
                // Recorrer la lista de usuarios e imprimir sus datos
                for (int i = 0; i < data.saves.Length; i++)
                {
                    savesObj[i].GetComponent<ShowData>().name.SetText(data.saves[i].name);
                    savesObj[i].GetComponent<ShowData>().time_played.SetText(data.saves[i].time_played);
                    savesObj[i].GetComponent<ShowData>().money = data.saves[i].moneySaved;
                    savesObj[i].GetComponent<ShowData>().kills = data.saves[i].amountOfKills;
                }
            }
            else
            {
                Debug.Log("Respuesta del servidor vacía");
            }

        }
        www.Dispose();
    }
}

[Serializable]
public class Saves
{
    public SaveData[] saves;
}
[Serializable]
public class SaveData
{
    public int id;
    public string time_played;
    public string name;
    public int moneySaved;
    public int amountOfKills;
}
