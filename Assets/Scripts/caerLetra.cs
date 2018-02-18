using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caerLetra : MonoBehaviour {

    
    Rigidbody2D cuerpo;

    public bool aSalvo=false;

    private void Start()
    {
        cuerpo = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerExit2D(Collider2D plataforma)
    {
        if (plataforma.gameObject.tag == "Campo")
        {
            StartCoroutine("verificarPosicion");
        }
    }



    IEnumerator verificarPosicion()
    {
        while (true) {
            if (aSalvo) {
                Debug.Log("A SALVO");
                break;
            }

            if (cuerpo.velocity.magnitude < 0.1f) {
                StartCoroutine("caerse");
                break;
            }

            Debug.Log("Velocidad : "+ cuerpo.velocity.magnitude);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator caerse() {

        float gradiente = this.gameObject.transform.localScale.x;

        while (gradiente >= 0)
        {
            gradiente -= 0.03f * Time.deltaTime;
            cuerpo.transform.localScale = new Vector3(gradiente, gradiente, gradiente);
            yield return new WaitForEndOfFrame();
        }
        Autodestruccion();
    }

    void Autodestruccion() {
        Destroy(this.gameObject,0.5f);
    }

}
