using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SelectAbilities : MonoBehaviour
{
    private string phpUrl; // URL del archivo PHP
    public Abilities ability;
    public int selectedAbility;
    public TMP_Text currentMoney;
    void Awake()
    {
        
        phpUrl = "http://localhost/SelectAbilities.php";
        StartCoroutine(GetAbilityFromDatabase());
        currentMoney.SetText(PlayerPrefs.GetInt("money").ToString());
    }

    private IEnumerator GetAbilityFromDatabase()
    {
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
        ability = JsonUtility.FromJson<Abilities>("{\"data\":"+responseText+"}");

       

    }

    public void UpgradeAbility()
    {
        if (ability.data[selectedAbility].abilityLevel < 5 && PlayerPrefs.GetInt("money") > ability.data[selectedAbility].upgradeCost)
        {
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - ability.data[selectedAbility].upgradeCost);
            StartCoroutine(UpdateMoneySaved((PlayerPrefs.GetInt("money"))));
            StartCoroutine(UpdateAbility());

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Volver()
    {
        SceneManager.LoadScene("InGameMenu");
    }

    IEnumerator UpdateAbility()
    {
        // Crear un nuevo formulario para enviar los datos
        WWWForm form = new WWWForm();
        form.AddField("idHab", ability.data[selectedAbility].idHab);
        form.AddField("idPlayer", ability.data[selectedAbility].idPlayer);
        form.AddField("name", ability.data[selectedAbility].name);
        form.AddField("abilityLevel", ability.data[selectedAbility].abilityLevel + 1);
        form.AddField("increaseAmount", ability.data[selectedAbility].increaseAmount);
        form.AddField("upgradeCost", ability.data[selectedAbility].upgradeCost * 2);
        form.AddField("description", ability.data[selectedAbility].description);

        // Enviar los datos al script PHP
        string url = "http://localhost/UpdateAbility.php";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        // Verificar si hubo un error en la comunicación
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error de conexión: " + www.error);
            yield break;
        }

        // Obtener la respuesta del script PHP
        string response = www.downloadHandler.text;
        www.Dispose();
        Debug.Log("Respuesta: " + response);
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

[Serializable]
public class Abilities
{
    public AbilityData[] data;
}
[Serializable]
public class AbilityData
{
    public int idHab;
    public int idPlayer;
    public string name;
    public int abilityLevel;
    public int increaseAmount;
    public int upgradeCost;
    public string description;
}

