using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letra : MonoBehaviour {

    [HideInInspector]
    public bool acelerada = false;
    Rigidbody2D cuerpoLetra;

	// Use this for initialization
	void Start () {
        cuerpoLetra = this.GetComponent<Rigidbody2D>();
	}


    private void FixedUpdate()
    {
        if (acelerada)
            verVelocidad();
    }

    void verVelocidad() {

        if (cuerpoLetra.velocity.magnitude < 0.5f) {
            acelerada = false;
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
    

    private void OnCollisionEnter2D(Collision2D invasor)
    {
        if (invasor.gameObject.tag == "Player") {

            if (acelerada)
                invasor.gameObject.GetComponent<marearse>().noquear();
        }
    }



}