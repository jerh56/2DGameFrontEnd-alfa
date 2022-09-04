using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;


public class ButonHandler : MonoBehaviour
{

    //private int numFailAttemps = 0;

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
        Debug.Log("El Botón "  + this.name + " ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        Text txtPhraseText = GameObject.FindWithTag("PhraseText").GetComponent<Text>();
        Text txtPhraseTextHide = GameObject.FindWithTag("PhraseTextHide").GetComponent<Text>();
        Text txtAttempts = GameObject.FindWithTag("txt-attempts").GetComponent<Text>();
        Text txtFailAttempts = GameObject.FindWithTag("txt-fail-attempts").GetComponent<Text>();

        bool optFoundIt = false;

        txtReturn.text = "El Botón "  + this.name + " ha sido presionado";
        string ButtonName = this.name.Substring(this.name.Length - 1,1);
        string txtPhraseResult = "";
        int attempts = -1;
        int.TryParse(txtAttempts.text, out attempts);
        attempts++;
        Debug.Log(attempts);
        Debug.Log(ButtonName);
        txtAttempts.text = attempts.ToString();
        char ButtonNameChar = ButtonName[0];
        //ButtonName = ButtonName.

        string PhraseText = txtPhraseTextHide.text;
        string PhraseTextResult = txtPhraseText.text;
        Debug.Log(PhraseText);
        Debug.Log(PhraseTextResult);
        for (int i = 0; i < PhraseText.Length; i ++){
           if (PhraseText[i].Equals(ButtonNameChar)){
                //Debug.Log(PhraseText[i]);
                txtPhraseResult += PhraseText[i];
                //PhraseTextResult[i] = PhraseText[i];
                optFoundIt = true;
           }
           else{
              //Debug.Log(PhraseText[i]);
               // Debug.Log("_");
               if (PhraseTextResult[i] != '_'){
                    //PhraseTextResult[i] = PhraseText[i];
                    txtPhraseResult += PhraseTextResult[i];
               }
               else{
                    txtPhraseResult += "_";
               }
                /*if (PhraseText[i] != ' '){
                    txtPhraseResult += "_";
                    PhraseTextResult[i] = PhraseText[i];
                }
                else{
                    txtPhraseResult += " ";
                }*/

           }
        }

        if (optFoundIt == false) {
            GameHandler.numFailAttemps++;
        }
        //Debug.Log(PhraseTextResult);
        txtPhraseText.text = txtPhraseResult;

        Debug.Log("Intentos fallidos : " + GameHandler.numFailAttemps.ToString());
        txtFailAttempts.text = GameHandler.numFailAttemps.ToString();

        if (!(GameHandler.numFailAttemps < GameHandler.numAllowFailAttemps)) {
            EditorUtility.DisplayDialog("Game over.", "Haz alcanzado el limite de intentos fallidos.","Aceptar");
            
            //Reiniciar el juego
            Application.LoadLevel(0); //Obsoleto
            //SceneManager.LoadScene(0);
        }


    }

    private IEnumerator ProcessRequest(string uri, Text txtReturn)
    {
        string textPhrase = "";
        string textPhraseHide = "";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            //request.SetRequestHeader("Authorization", "Token 07d1107de8e2977b035f6723da8a5240d25bf15a");
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
}
