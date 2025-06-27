using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public TMP_InputField puntajeInput;
    public TextMeshProUGUI puntajeAnteriorText;
    private string insertUrl = "http://localhost/juego3/insertar_actualizar_usuario.php";
    private string getUrl = "http://localhost/juego3/obtener_puntaje.php";

    public void EnviarPuntaje()
    {
        StartCoroutine(ObtenerYActualizar());
    }

    private IEnumerator ObtenerYActualizar()
    {
        string nombre = nombreInput.text;
        UnityWebRequest get = UnityWebRequest.Get(getUrl + "?nombre_usuario=" + nombre);
        yield return get.SendWebRequest();

        if (get.result == UnityWebRequest.Result.Success)
        {
            
            string json = get.downloadHandler.text;
            PuntajeModel datos = JsonUtility.FromJson<PuntajeModel>(json);

            puntajeAnteriorText.text = "Puntaje anterior: " + datos.puntaje;

           WWWForm form = new WWWForm();
            form.AddField("nombre_usuario", nombre);
            form.AddField("puntaje", int.Parse(puntajeInput.text));

            UnityWebRequest post = UnityWebRequest.Post(insertUrl, form);
            yield return post.SendWebRequest();

            Debug.Log(post.downloadHandler.text); 
        }
        else
        {
            Debug.Log("Error al obtener puntaje");
        }
    }

    [System.Serializable]
    public class PuntajeModel
    {
        public int puntaje;
    }
}
