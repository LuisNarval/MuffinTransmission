using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movFisicas : MonoBehaviour {

    float movX;
    float movZ;

    Vector3 direccion;

    Rigidbody cuerpo;

    public float velocidad = 10.0f;

	// Use this for initialization
	void Start () {
        cuerpo = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {}

    private void FixedUpdate()
    {
        moverse();
    }

    void moverse() {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        direccion = new Vector3(movX, 0.0f, movZ);

        cuerpo.MovePosition(cuerpo.position+(direccion*velocidad*Time.deltaTime));
    }


}