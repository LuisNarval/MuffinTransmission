using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fijarVista : MonoBehaviour {

    public Transform objetivo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(objetivo);
	}
}
