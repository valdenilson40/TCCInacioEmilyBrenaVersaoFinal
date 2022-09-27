using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    private Transform mainCamera;
    private Ray raio;
    private RaycastHit raioInformacao;
    public float alcanceRaio;
    public LayerMask layerInteracao;
    public GameObject posicaoRaycast;
    public float distanciaAlvo;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    
    void Update()
    {
        raio.origin = posicaoRaycast.transform.position;
        raio.direction = posicaoRaycast.transform.forward;
        Debug.DrawRay(raio.origin, raio.direction * alcanceRaio, Color.red);

        if (Physics.Raycast(raio.origin, raio.direction, out raioInformacao, alcanceRaio, layerInteracao))
        {
            distanciaAlvo = raioInformacao.distance;
        }
    }
}
