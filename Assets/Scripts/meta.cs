using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meta : MonoBehaviour {

    public int equipo = 1;

    public casilla[] casillas;
    public string[] PALABRA;

    

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}


    public void colocarLetras() {
        for (int i = 0; i < casillas.Length; i++) {
            casillas[i].inicializar(PALABRA[i]);
        }
    }





}