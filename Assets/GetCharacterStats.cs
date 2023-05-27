using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetCharacterStats : MonoBehaviour
{
    public int idCharacter;
    public Stats array;

  
    public IEnumerator GetAbilityFromDatabase()
    {

        // URL de la página PHP que devuelve los usuarios
        string url = "http://localhost/GetCharacterStats.php";

        // Crear un formulario con el idCharacter a enviar
        WWWForm form = new WWWForm();
        form.AddField("idCharacter", PlayerPrefs.GetInt("idCharacter"));

        // Realizar la petición GET a la página PHP
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        // Verificar si ocurrió algún error
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al obtener los datos: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + response);

            if (!string.IsNullOrEmpty(www.downloadHandler.text))
            {

                // Convertir el resultado de la petición a una lista de objetos JSON
                array = JsonUtility.FromJson<Stats>("{\"data\":" + www.downloadHandler.text + "}");
                Debug.Log(array.data[0].pullStrength);
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
public class Stats
{
    public StatsData[] data;
}
[Serializable]
public class StatsData
{
    public float health;
    public float speed;
    public float damage;
    public float armor;
    public float pullStrength;
}

