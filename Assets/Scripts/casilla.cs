using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class casilla : MonoBehaviour {
    
    public Image panel;

    string letraAsignada;
    Text txt_miLetra;
    bool decisionTomada =false;
    
    public void inicializar(string letAsig) {
        letraAsignada = letAsig;
        txt_miLetra = this.GetComponentInChildren<Text>();
        txt_miLetra.text = letraAsignada;
        txt_miLetra.color = new Color(0.0f,0.0f,0.0f,105.0f/255.0f);
    }
    
    private void OnTriggerEnter2D(Collider2D invasor)
    {
        if (invasor.gameObject.tag == "Letra"&&!decisionTomada) {
            if (invasor.GetComponentInChildren<Text>().text == letraAsignada)
                letraCorrecta();
            else 
                letraIncorrecta();

         }
    }
    
    void letraCorrecta() {
        panel.color = Color.green;
        txt_miLetra.gameObject.SetActive(false);
        decisionTomada = true;

        GameObject.Find("GAMEADMIN").GetComponent<GAMEADMIN>().fichaAnotada(this.transform.parent.GetComponent<meta>().equipo,1);
    }

    void letraIncorrecta() {
        panel.color = Color.red;
        txt_miLetra.gameObject.SetActive(false);
        decisionTomada = true;

        GameObject.Find("GAMEADMIN").GetComponent<GAMEADMIN>().fichaAnotada(this.transform.parent.GetComponent<meta>().equipo,0);
    }

}