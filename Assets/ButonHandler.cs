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

        const string URL = "https://dev-simplesales-backend.herokuapp.com/api/articulos-color/";
        Text txtReturn = transform.Find("ReturnText").GetComponent<Text>();
        //txtReturn.text = text;
        StartCoroutine(ProcessRequest(URL, txtReturn));
    }

    public void ButtonAPressed(){
        Debug.Log("El Botón A ha sido presionado");
    }

    public void ButtonBPressed(){
        Debug.Log("El Botón B ha sido presionado");
    }

    public void ButtonCPressed(){
        Debug.Log("El Botón C ha sido presionado");
    }

    public void ButtonDPressed(){
        Debug.Log("El Botón D ha sido presionado");
    }

    public void ButtonEPressed(){
        Debug.Log("El Botón E ha sido presionado");
    }

    private IEnumerator ProcessRequest(string uri, Text txtReturn)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("Authorization", "Token 07d1107de8e2977b035f6723da8a5240d25bf15a");
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
