using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButonHandler : MonoBehaviour
{
    public void SetText(string text){
        Text txt = transform.Find("Text (Legacy)").GetComponent<Text>();
        txt.text = text;

        const string URL = "https://jsonplaceholder.typicode.com/todos/1";
        Text txtReturn = transform.Find("ReturnText").GetComponent<Text>();
        //txtReturn.text = text;
        StartCoroutine(ProcessRequest(URL, txtReturn));
    }

    private IEnumerator ProcessRequest(string uri, Text txtReturn)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            Debug.Log(request.result);
            if (request.result ==  UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                txtReturn.text = request.downloadHandler.text;
            }
        }
    }
}
