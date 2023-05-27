using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetAbilityIncreases : MonoBehaviour
{
    public Abilities abilities;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAbilityFromDatabase());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GetAbilityFromDatabase()
    {
        string phpUrl = "http://localhost/SelectAbilities.php";
        // Preparamos los datos que queremos mandar al archivo PHP
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("id").ToString());

        // Creamos la petición HTTP POST
        UnityWebRequest www = UnityWebRequest.Post(phpUrl, form);

        // Enviamos la petición al servidor y esperamos a recibir respuesta
        yield return www.SendWebRequest();

        // Si hay algún error en la conexión, lo mostramos en la consola
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            yield break;
        }

        // Si la conexión fue exitosa, obtenemos los datos recibidos como string
        string responseText = www.downloadHandler.text;
        www.Dispose();
        // Convertimos los datos recibidos de JSON a un objeto Ability
        abilities = JsonUtility.FromJson<Abilities>("{\"data\":" + responseText + "}");



    }

    public void UpdateStats()
    {

    }

}
[Serializable]
public class AbilitiesArray
{
    public AbilityData[] data;
}
[Serializable]
public class AbilitiesData
{
    public string name;
    public int abilityLevel;
    public int increaseAmount;
}


