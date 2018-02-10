using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoger : MonoBehaviour {
    
    public Transform posManos;

    public GameObject objLetra;

    bool puedeRecoger = false;
    bool estaCargando = false;


    Animator animMuffin;

	// Use this for initialization
	void Start () {
        animMuffin = this.gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (estaCargando){
            cargar();
        }
    }


    void Update()
    {




        //if (Input.GetAxis("Fire1")>0.0f)

        if (Input.GetKeyDown(KeyCode.Joystick1Button0)){

            Debug.Log("Boton A presionado");

            if (puedeRecoger) {

                objLetra.gameObject.GetComponent<BoxCollider2D>().enabled=false;
                objLetra.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "LetraRecogida";
                objLetra.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = "LetraRecogida";

                puedeRecoger = false;
                estaCargando = true;


                this.gameObject.GetComponent<movimiento>().velocidad /= 2;

                animMuffin.Play("idleCargando_Muffin");
            }
        }

    }




    void cargar()
    {
        if (objLetra != null)
            objLetra.transform.position = posManos.position;
        else
            Debug.Log("El GameObject se encuentra vacio");
    }

    
    void OnTriggerEnter2D(Collider2D invasor)
    {
        if (invasor.gameObject.tag == "Letra") {
            puedeRecoger = true;
            objLetra = invasor.gameObject;
        } 
    }

    void OnTriggerExit2D(Collider2D invasor)
    {
        if (invasor.gameObject.tag == "Letra") {
            puedeRecoger = false;
            //objLetra = null;
        }  
    }




}