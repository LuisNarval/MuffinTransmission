using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GAMEADMIN : MonoBehaviour {


    public GameObject letraInstance;

    public GameObject meta1;
    public GameObject meta2;

    string[] palabra1;
    string[] palabra2;

    public int contadorMeta1;
    public int contadorMeta2;

    public struct fichas{
        public string letra;
        public bool jugada;
    }


    fichas[] piscinaFichas;

    public int fichaActual;

	// Use this for initialization
	void Start () {
        inicializarJuego();

  

    }

    // Update is called once per frame
    void Update () {}



    void inicializarJuego() {
        inicializarMetas();
        inicializarPiscina();
        StartCoroutine("corrutineNuevaLetra");
    }


    void inicializarMetas() {
        obtenerPalabras();

        meta1.gameObject.GetComponent<meta>().PALABRA=palabra1;
        meta2.gameObject.GetComponent<meta>().PALABRA=palabra2;

        meta1.gameObject.GetComponent<meta>().colocarLetras();
        meta2.gameObject.GetComponent<meta>().colocarLetras();
    }

    void inicializarPiscina() {
        piscinaFichas = new fichas[palabra1.Length + palabra2.Length];
        
        int gradiente = 0;

        for (int i =0; i<palabra1.Length; i++) {

            piscinaFichas[i + gradiente].letra = palabra1[i];
            piscinaFichas[i + gradiente].jugada = false;

            piscinaFichas[i + gradiente + 1].letra = palabra2[i];
            piscinaFichas[i + gradiente + 1].jugada = false;

            gradiente++;
        }
        
        for (int i=0; i< piscinaFichas.Length;i++) {
            Debug.Log("ID: "+i+" Letra: "+piscinaFichas[i].letra+" Jugada: "+ piscinaFichas[i].jugada);
        }
        
    }
    
    void obtenerPalabras() {
        palabra1 = new string[] {"C","O","M","I","D","A"};
        palabra2 = new string[] { "C", "A", "M", "I", "N", "O" };
    }



    private void OnTriggerEnter2D(Collider2D invasor)
    {

        if (invasor.gameObject.tag == "Letra") {
            StartCoroutine("corrutineNuevaLetra");
        }

        
    }

    IEnumerator corrutineNuevaLetra(){

        yield return new WaitForSeconds(2.0f);

        GameObject instanciaL = Instantiate(letraInstance, Vector3.zero, Quaternion.identity) as GameObject;
        instanciaL.GetComponentInChildren<Text>().text = sacarLetraDePiscina();

        float escalaOptima = instanciaL.transform.localScale.x;
        float escalaActual = 0.0f;
        instanciaL.transform.localScale = new Vector3(0.0f,0.0f,0.0f);


        while (escalaActual < escalaOptima) {
            escalaActual += 10 * Time.deltaTime;
            instanciaL.transform.localScale = new Vector3(escalaActual, escalaActual, escalaActual);
            yield return new WaitForEndOfFrame();
        }

        instanciaL.transform.localScale = new Vector3(escalaOptima, escalaOptima, escalaOptima);
        
    }
    

    public void fichaAnotada(int equipo, int valor) {

        piscinaFichas[fichaActual].jugada = true;

        if (equipo == 1)
            contadorMeta1 += valor;
        
        else if (equipo == 2) 
            contadorMeta2 += valor;


        if (contadorMeta1 >= palabra1.Length || contadorMeta2 >= palabra2.Length) {

            StopCoroutine("corrutineNuevaLetra");

            Debug.Log("SE HA TERMINADO EL JUEGO");

            Debug.Log("EL EQUIPO " +equipo+ " HA GANADO.");
        }

    }



    string sacarLetraDePiscina() {

        fichas ficha;

        ficha.letra = "";
        ficha.jugada = false;

        while (ficha.letra == "") {

            fichaActual = (int)Random.Range(0.0f, 12.0f);
            
            ficha =  piscinaFichas[fichaActual];
            if (ficha.jugada == true)
                ficha.letra = "";
        }
        
        Debug.Log("Ficha actual: " + fichaActual);

        return ficha.letra;
    } 


}