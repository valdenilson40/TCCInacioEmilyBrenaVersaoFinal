using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passarp : MonoBehaviour
{


    public GameObject passaro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time != 0)
        {
            passaro.gameObject.GetComponent<Animator>().SetTrigger("animPassaro");
        }
        
    }
}
