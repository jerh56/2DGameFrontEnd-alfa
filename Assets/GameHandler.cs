using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    public static int numFailAttemps;
    public static int numAllowFailAttemps;
    
    public Renderer fondo;
    public GameObject winner;
    public string escenaActual;
    public static bool isWinner = false;

    private float positionX = 0f;
    private float positionY = 0.030f;

    [System.Serializable]
    public class MyPhrase
    {
        public string phrase;
        public string type;
    }
    // Start is called before the first frame update
    void Start()
    {
        winner.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        escenaActual = SceneManager.GetActiveScene().name;
        numFailAttemps = 0;
        numAllowFailAttemps = 3;
        GameObject DialogContainer = GameObject.FindGameObjectWithTag("Canvas");
        DialogContainer.transform.Find("DialogBoxGameOver").gameObject.SetActive(false);
        const string URL = "https://game2dbackend-alfa.herokuapp.com/phrase";
        
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
                
                MyPhrase myPhrase = new MyPhrase();

                myPhrase = JsonUtility.FromJson<MyPhrase>(request.downloadHandler.text);

                textPhrase = myPhrase.phrase;
                foreach(char c in textPhrase)
                    {
                        // Debug.Log(c);
                        if (c != ' '){
                            textPhraseHide = textPhraseHide + "_ ";
                        }
                        else{
                            textPhraseHide = textPhraseHide + "  ";
                        }
                    }

                txtPhraseText.text = textPhraseHide;
                Text txtPhraseHide = GameObject.FindWithTag("PhraseTextHide").GetComponent<Text>();
                txtPhraseHide.text = textPhrase;
                GameObject DialogContainer = GameObject.FindGameObjectWithTag("Canvas");
                DialogContainer.transform.Find("PanelGameOver").gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWinner && Input.anyKey)
        {
            SceneManager.LoadScene(escenaActual);
            isWinner = false;
            winner.SetActive(false);
        }

        if (isWinner)
        {
            winner.SetActive(true);
        }

        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new UnityEngine.Vector2(positionX, positionY) * Time.deltaTime;
    }
}
