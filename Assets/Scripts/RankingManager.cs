using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public TextMeshProUGUI rankingText;
    private string url = "http://localhost/juego3/ranking.php";

    void Start()
    {
        StartCoroutine(GetRanking());
    }

    IEnumerator GetRanking()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/juego3/ranking.php");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            UserList lista = JsonUtility.FromJson<UserList>("{\"ranking\":" + json + "}");

            rankingText.text = "Ranking:\n";
            foreach (UserModel user in lista.ranking)
            {
                rankingText.text += $"{user.nombre_usuario}: {user.puntaje}\n";
            }
        }
        else
        {
            Debug.Log("Error al obtener ranking");
        }
    }

    [System.Serializable]
    public class Usuario
    {
        public string nombre_usuario;
        public int puntaje;
    }

    [System.Serializable]
    public class RankingList
    {
        public List<Usuario> ranking;
    }

}
