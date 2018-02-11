using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour {

    public float velocidad = 10.0f;
    
    public GameObject manos;

    [HideInInspector]
    public Animator animMuffin;

    SpriteRenderer spriteMuffin;

    float movX;
    float movY;
    

	// Use this for initialization
	void Start () {
        animMuffin = this.GetComponent<Animator>();
        spriteMuffin = this.GetComponent<SpriteRenderer>();
    }
	
    private void FixedUpdate()
    {
        moverse();
    }

    
    void moverse() {

        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");
        

        if (movX < 0) {
            manos.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
            spriteMuffin.flipX = true;
        }

        else if (movX > 0) {
            manos.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            spriteMuffin.flipX = false;
        }
            

        Vector3 direccion = new Vector3(movX, movY, 0.0f);
        animMuffin.SetFloat("velocidad", Vector3.Magnitude(direccion));
        
        
        this.transform.Translate(direccion.normalized*velocidad*Time.deltaTime);
        



    }
    
    






}
