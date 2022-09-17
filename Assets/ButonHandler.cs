using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;


public class ButonHandler : MonoBehaviour
{

    //private int numFailAttemps = 0;
    public AudioSource fuente;
    public AudioClip clip;
    public void SetText(string text){
        Text txt = transform.Find("Text (Legacy)").GetComponent<Text>();
        txt.text = text;

        //numFailAttemps = 0;

        const string URL = "https://dev-simplesales-backend.herokuapp.com/api/articulos-color/";
        Text txtReturn = transform.Find("ReturnText").GetComponent<Text>();
        //txtReturn.text = text;
        StartCoroutine(ProcessRequest(URL, txtReturn));
    }

    public void ButtonPressed(){
        fuente = GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>();
        Debug.Log("El Botón "  + this.name + " ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        //Text txtPhraseText = GameObject.FindWithTag("PhraseText").GetComponent<Text>();
        TMP_Text txtPhraseText = GameObject.FindWithTag("txt-phrase2-mesh").GetComponent<TMP_Text>();

        Text txtPhraseTextHide = GameObject.FindWithTag("PhraseTextHide").GetComponent<Text>();
        Text txtAttempts = GameObject.FindWithTag("txt-attempts").GetComponent<Text>();
        Text txtFailAttempts = GameObject.FindWithTag("txt-fail-attempts").GetComponent<Text>();
        Button btnButton = this.GetComponent<Button>();
        bool optFoundIt = false;
        PlaySound();
        btnButton.gameObject.SetActive(false);
        txtReturn.text = "El Botón "  + this.name + " ha sido presionado";
        string ButtonName = this.name.Substring(this.name.Length - 1,1);
        string txtPhraseResult = "";
        int attempts = -1;
        int.TryParse(txtAttempts.text, out attempts);
        attempts++;
        
        txtAttempts.text = attempts.ToString();
        char ButtonNameChar = ButtonName[0];

        string PhraseText = txtPhraseTextHide.text;
        string PhraseTextResult = txtPhraseText.text;
        
        for (int i = 0; i < PhraseText.Length; i ++){
            if (PhraseText[i].Equals(ButtonNameChar)){
                txtPhraseResult += PhraseText[i];
                optFoundIt = true;
            }
            else{
              
                if (PhraseTextResult[i] != '_'){
                    txtPhraseResult += PhraseTextResult[i];
                }
                else{
                    txtPhraseResult += "_";
                }
                

           }
        }

        if (optFoundIt == false) {
            GameHandler.numFailAttemps++;
        }
        
        txtPhraseText.text = txtPhraseResult;

        txtFailAttempts.text = GameHandler.numFailAttemps.ToString();

        if (!txtPhraseResult.Contains("_"))
        {
            GameHandler.isWinner = true; //Ganador
        }

        if (!(GameHandler.numFailAttemps < GameHandler.numAllowFailAttemps)) {
            GameObject DialogContainer = GameObject.FindGameObjectWithTag("Canvas");
            //GameObject PanelGameOver = GameObject.FindGameObjectWithTag("PanelGameOver");
            DialogContainer.transform.Find("PanelGameOver").gameObject.SetActive(true);
            DialogContainer.transform.Find("DialogBoxGameOver").gameObject.SetActive(true);
        }

    }

    private IEnumerator ProcessRequest(string uri, Text txtReturn)
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
                Debug.Log(request.downloadHandler.text);
                textPhrase = request.downloadHandler.text;
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

                txtReturn.text = textPhraseHide;
            }
        }
    }

  void ApplyDamage(float damage)
    {
        print(damage);
    }

    public void PlaySound(){
        try
        {
            fuente.Play();
            Debug.Log("Play Sound");

        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void Restart(){
        //Reiniciar el juego
        Application.LoadLevel(0); //Obsoleto
        //SceneManager.LoadScene(0);
    }
}
