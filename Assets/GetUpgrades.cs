using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GetUpgrades : MonoBehaviour
{
    public int idCharacter;
    public GameObject upgrades;
    public Upgrades array;
 

    public IEnumerator GetAbilityFromDatabase()
    {
        
            // URL de la p�gina PHP que devuelve los usuarios
            string url = "http://localhost/GetUpgrades.php";
      
        // Crear un formulario con el idCharacter a enviar
        WWWForm form = new WWWForm();
        form.AddField("idCharacter", PlayerPrefs.GetInt("idCharacter"));

        // Realizar la petici�n GET a la p�gina PHP
        UnityWebRequest www = UnityWebRequest.Post(url, form);
            yield return www.SendWebRequest();

            // Verificar si ocurri� alg�n error
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error al obtener los datos: " + www.error);
            }
            else
            {
            string response = www.downloadHandler.text;
            

            if (!string.IsNullOrEmpty(www.downloadHandler.text))
                {

                    // Convertir el resultado de la petici�n a una lista de objetos JSON
                    array = JsonUtility.FromJson<Upgrades>("{\"data\":" + www.downloadHandler.text + "}");
                    foreach(UpgradesData upg in array.data)
                {
                    Debug.Log(upg.upgradeName);
                }
            }
                else
                {
                    Debug.Log("Respuesta del servidor vac�a");
                }

            }
            www.Dispose();
        
    }


}


[Serializable]
public class Upgrades { 
    public UpgradesData[] data;
}
[Serializable]
public class UpgradesData
{
    public string upgradeName;
    public string statToIncrease;
    public float increaseAmount;
    public string description;
    public string image;
}

