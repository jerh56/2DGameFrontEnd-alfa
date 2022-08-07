using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        const string URL = "https://game2dbackend-alfa.herokuapp.com/test-phrase";
        Text txtPhraseText = transform.Find("PhraseText").GetComponent<Text>();
        //txtReturn.text = text;
        StartCoroutine(ProcessRequest(URL, txtPhraseText));
    }

    private IEnumerator ProcessRequest(string uri, Text txtPhraseText)
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
                txtPhraseText.text = request.downloadHandler.text;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
