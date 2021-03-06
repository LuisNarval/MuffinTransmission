﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marearse : MonoBehaviour {
    
    private Animator animMuffin;

	// Use this for initialization
	void Start () {
        animMuffin = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {}


    public void noquear()
    {
        StartCoroutine("corrutineNoquear");
    }

    IEnumerator corrutineNoquear()
    {
        this.GetComponent<movimiento>().enabled=false;

        if (this.GetComponent<recoger>().estaCargando) {
            this.GetComponent<recoger>().soltar();
        }

        this.GetComponent<recoger>().enabled = false;

        this.GetComponent<Rigidbody2D>().mass = 1100;
        animMuffin.Play("noqueado_Muffin");
        
        yield return new WaitForSeconds(2.0F);
        
        animMuffin.Play("idle_Muffin");
        this.GetComponent<Rigidbody2D>().mass = 10;

        this.GetComponent<movimiento>().enabled = true;
        this.GetComponent<recoger>().enabled = true;
    }



}
