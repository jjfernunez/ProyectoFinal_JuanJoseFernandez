using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CreateSave : MonoBehaviour
{
    private string createSaveUrl = "http://localhost/CreateSave.php";

    public void CreateNewSave(int id, string nombre, string tiempo, int amountOfKills, int moneySaved)
    {
        StartCoroutine(SendCreateSaveRequest(id, nombre, tiempo, amountOfKills, moneySaved));
    }

    IEnumerator SendCreateSaveRequest(int id, string nombre, string time_played, int amountOfKills, int moneySaved)
    {
        // Crear una instancia de WWWForm y agregar los datos necesarios
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("name", nombre);
        form.AddField("time_played", time_played);
        form.AddField("moneySaved", moneySaved);
        form.AddField("amountOfKills", amountOfKills);

        // Enviar una solicitud POST al servidor PHP
        using (UnityWebRequest www = UnityWebRequest.Post(createSaveUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Si la solicitud se realizó con éxito, mostrar la respuesta
                Debug.Log("Response: " + www.downloadHandler.text);
            }
            else
            {
                // Si la solicitud falló, manejar el error
                Debug.Log("Error: " + www.error);
            }
        }
    }


}
