using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class controlplayer : MonoBehaviour
{
    [Header("Mecânica Básica")]
    public bool sensor;
    public LayerMask layerPulo;
    public Transform posicaoSensor;
    public float jumpForce;
    private Vector2 input;
    private Animator controleAnimacao;
    float agachar;

    [Header("HUD")]
    public float vidaInicial;
    public float vidaAtual;
    public float porcentVida;
    public Image imgVida;
    private CharacterController controller;


    [Header("Gravidade Player")]
    public float fatorMultiplicativo;
    public float gravidade;
    private Vector3 velocidadePulo;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controleAnimacao = GetComponent<Animator>();
        vidaAtual = vidaInicial;
        controller = GetComponent<CharacterController>(); 
        gravidade = Physics.gravity.y;
    }


    void Update()
    {
        movimentacao();
        gravidadePlayer();
        sensorChao();
        JumpPlayer();
    }

    public void movimentacao()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        controleAnimacao.SetFloat("inputX", input.x);
        controleAnimacao.SetFloat("inputY", input.y);

        if (Input.GetKeyDown(KeyCode.C))
        {
            agachar = 1;

        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            agachar = 0;
        }
        controleAnimacao.SetFloat("agachar", agachar);
        
        if (Input.GetButtonDown("Jump") && sensor == true)
        {
            velocidadePulo.y = jumpForce;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vida")
        {
            vidaAtual -= 1;
            porcentVida = vidaAtual / vidaInicial;
            imgVida.fillAmount = porcentVida;
        }

        if (other.gameObject.tag == "ganharVida")
        {
            vidaAtual = vidaInicial;
            porcentVida = vidaAtual / vidaInicial;
            imgVida.fillAmount = porcentVida;
        }
    }
    public void gravidadePlayer()
    {
        velocidadePulo.y += gravidade * fatorMultiplicativo * Time.deltaTime;

        controller.Move(velocidadePulo * Time.deltaTime);
    }
    public void sensorChao()
    {
        sensor = Physics.CheckSphere(posicaoSensor.position, 0.5f, layerPulo);
    }
    public void JumpPlayer()  
    {
        if (Input.GetButtonDown("Jump") && sensor == true)
        {
            velocidadePulo.y = jumpForce;

           
        }

        controleAnimacao.SetBool("jump", sensor);
    }
}
