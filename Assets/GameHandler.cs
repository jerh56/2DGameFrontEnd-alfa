using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameHandler : MonoBehaviour
{

    public static int numFailAttemps;
    public static int numAllowFailAttemps;

    [System.Serializable]
    public class MyPhrase
    {
        public string phrase;
        public string type;
    }
    // Start is called before the first frame update
    void Start()
    {

        numFailAttemps = 0;
        numAllowFailAttemps = 3;
        GameObject DialogContainer = GameObject.FindGameObjectWithTag("Canvas");
        DialogContainer.transform.Find("DialogBoxGameOver").gameObject.SetActive(false);
        const string URL = "https://game2dbackend-alfa.herokuapp.com/phrase";
        Screen.fullScreen = !Screen.fullScreen;
        Text txtPhraseText = transform.Find("PhraseText").GetComponent<Text>();
        //txtReturn.text = text;
        StartCoroutine(ProcessRequest(URL, txtPhraseText));
    }

    private IEnumerator ProcessRequest(string uri, Text txtPhraseText)
    {
        string textPhrase = "";
        string textPhraseHide = "";
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
                //Debug.Log(request.downloadHandler.text);
                //txtPhraseText.text = request.downloadHandler.text;
                Debug.Log(request.downloadHandler.text);
                MyPhrase myPhrase = new MyPhrase();

                myPhrase = JsonUtility.FromJson<MyPhrase>(request.downloadHandler.text);
                Debug.Log(myPhrase);

                textPhrase = myPhrase.phrase;
                foreach(char c in textPhrase)
                    {
                        Debug.Log(c);
                        if (c != ' '){
                            textPhraseHide = textPhraseHide + "_";
                        }
                        else{
                            textPhraseHide = textPhraseHide + " ";
                        }
                    }

                txtPhraseText.text = textPhraseHide;
                Text txtPhraseHide = GameObject.FindWithTag("PhraseTextHide").GetComponent<Text>();
                txtPhraseHide.text = textPhrase;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
