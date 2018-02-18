using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caerMuffin : MonoBehaviour {

    public float tiempoRespawn = 5.0f;
    public Transform posicionRespawn;

    public GameObject cuerpo;

    void OnTriggerExit2D(Collider2D plataforma) {
        if (plataforma.gameObject.tag == "Campo") {
            StartCoroutine("caerse");
        }
    }



    IEnumerator caerse() {

        cuerpo.gameObject.GetComponent<movimiento>().enabled = false;
        cuerpo.gameObject.GetComponent<recoger>().enabled = false;
        cuerpo.gameObject.GetComponent<recoger>().StopCoroutine("corrutineTaclear");
        cuerpo.gameObject.GetComponent<recoger>().estaTacleando = false;

        cuerpo.gameObject.GetComponent<Rigidbody2D>().mass = 1000;
        

        float tamO = cuerpo.gameObject.transform.localScale.x;

        cuerpo.gameObject.GetComponent<movimiento>().animMuffin.Play("noqueado_Muffin");


        float gradiente = tamO;

        while (gradiente>0) {
            gradiente -= 0.03f * Time.deltaTime;
            cuerpo.transform.localScale = new Vector3(gradiente,gradiente,gradiente);
            yield return new WaitForEndOfFrame();
        }

        

        cuerpo.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        yield return new WaitForSeconds(tiempoRespawn);

        cuerpo.transform.position = posicionRespawn.position;
        cuerpo.transform.localScale = new Vector3(tamO, tamO, tamO);
        cuerpo.gameObject.GetComponent<movimiento>().enabled = true;
        cuerpo.gameObject.GetComponent<recoger>().enabled = true;
        cuerpo.gameObject.GetComponent<Rigidbody2D>().mass = 10;

        cuerpo.gameObject.GetComponent<movimiento>().animMuffin.Play("idle_Muffin");

    }


	
}