using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoger : MonoBehaviour {
    
    public Transform posManos;

    public GameObject objLetra;

    public float offsetSueloY=-0.5f;
    public float offsetSueloX= 0.5f;

    public float fuerza = 2.0f;

    [HideInInspector]
    public bool estaCargando = false;
    bool puedeRecoger = false;

    [HideInInspector]
    public bool estaTacleando = false;
    

    Animator animMuffin;

	// Use this for initialization
	void Start () {
        animMuffin = this.gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (estaCargando){
            irCargando();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)){
            
            if (estaCargando){
                soltar();
            }
            
            if (puedeRecoger&&!estaCargando&&!estaTacleando) {
                levantar();
            }   
        }

        
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)) {
            if (estaCargando) arrojar();
            else if(!estaTacleando) taclear();
        }

    }
    
    void levantar() {
        objLetra.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        objLetra.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "LetraRecogida";
        objLetra.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = "LetraRecogida";

        estaCargando = true;
        
        this.gameObject.GetComponent<movimiento>().velocidad /= 2;
        animMuffin.Play("idleCargando_Muffin");
    }
    
    public void soltar()
    {
        objLetra.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        objLetra.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "LetraEnPiso";
        objLetra.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = "LetraEnPiso";

        estaCargando = false;
        
        this.gameObject.GetComponent<movimiento>().velocidad *= 2;
        animMuffin.Play("idle_Muffin");

       
        objLetra.transform.position = posManos.transform.position + (posManos.transform.right*offsetSueloX)+(Vector3.down*offsetSueloY);
        
    }


    void arrojar() {
        objLetra.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        objLetra.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "LetraEnPiso";
        objLetra.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = "LetraEnPiso";
        
        estaCargando = false;

        this.gameObject.GetComponent<movimiento>().velocidad *= 2;
        animMuffin.Play("idle_Muffin");
        
        objLetra.transform.position = posManos.transform.position + (posManos.transform.right * offsetSueloX);

        objLetra.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(posManos.transform.right.x,0.0f)*fuerza*1.2f;
        objLetra.gameObject.GetComponent<letra>().acelerada = true;
    }


    void taclear() {
        estaTacleando = true;
        StartCoroutine("corrutineTaclear");
    }

    IEnumerator corrutineTaclear() {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(posManos.transform.right.x, 0.0f) * fuerza*1.8f;
        animMuffin.Play("taclear_Muffin");
        this.gameObject.GetComponent<movimiento>().enabled=false;

        while (this.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f) {
            yield return new WaitForSeconds(0.01f);
        }

        animMuffin.Play("idle_Muffin");
        this.gameObject.GetComponent<movimiento>().enabled=true;
        estaTacleando = false;
    }

    void irCargando()
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
        }  
    }


    void OnCollisionEnter2D(Collision2D invasor) {
        if (invasor.gameObject.tag == "Player") {
            if (estaTacleando) {
                invasor.gameObject.GetComponent<marearse>().noquear();
            }
        }
    }



}