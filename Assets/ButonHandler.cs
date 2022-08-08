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
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        txtReturn.text = "El Botón A ha sido presionado";
    }

    public void ButtonBPressed(){
        Debug.Log("El Botón B ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        txtReturn.text = "El Botón B ha sido presionado";
    }

    public void ButtonCPressed(){
        Debug.Log("El Botón C ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        txtReturn.text = "El Botón C ha sido presionado";
    }

    public void ButtonDPressed(){
        Debug.Log("El Botón D ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        txtReturn.text = "El Botón D ha sido presionado";
    }

    public void ButtonEPressed(){
        Debug.Log("El Botón E ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        txtReturn.text = "El Botón E ha sido presionado";
    }

    public void ButtonPressed(){
        Debug.Log("El Botón "  + this.name + " ha sido presionado");
        Text txtReturn = GameObject.FindWithTag("MsgText").GetComponent<Text>();
        Text txtPhraseText = GameObject.FindWithTag("PhraseText").GetComponent<Text>();
        Text txtPhraseTextHide = GameObject.FindWithTag("PhraseTextHide").GetComponent<Text>();
        txtReturn.text = "El Botón "  + this.name + " ha sido presionado";
        string ButtonName = this.name.Substring(this.name.Length - 1,1);
        string txtPhraseResult = "";
        Debug.Log(ButtonName);
 
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
        //Debug.Log(PhraseTextResult);
        txtPhraseText.text = txtPhraseResult;


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
}
