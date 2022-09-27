using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasManager : MonoBehaviour
{
    public GameObject particula;
    public Transform posicaoInstanciar;


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "machado") {
            instanciarParticulaMachado();

            print("Toque");
        }
    }
    public void instanciarParticulaMachado() {

        Instantiate(particula, posicaoInstanciar.position, Quaternion.identity);
    }
}
