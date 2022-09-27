using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentocamera : MonoBehaviour
{
    public float velocidadeCamera;
    private Camera cameraPrincial;
    private float cameraY;
    


    void Start()
    {
        cameraPrincial = Camera.main;
    }

    private void FixedUpdate() {
        cameraY = cameraPrincial.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, cameraY, 0f), velocidadeCamera * Time.fixedDeltaTime);
    }
    void Update()
    {
        
    }
    
}
