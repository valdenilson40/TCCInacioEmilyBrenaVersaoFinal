using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class movimentoP : MonoBehaviour {

    [Header("Controle Personagem")]
    private Transform cam;
    private CharacterController controller;
    private Animator anim;
    public float velocidadeMovimento;
    private Vector3 direcao;
    private Vector3 direcaoMovimento;
    private float suavizacaoMovimento = 0.01f;
    private float suavizancaoVelocidade;
    private bool walk;

    [Header("Controle Gravidade")]
    public Transform gruondCheck;
    public LayerMask layerInteracao;
    public float gravidade;
    public bool sensor;
    private Vector3 velocidade;
    public float alturaPulo;
    public bool dormir;

    [Header("HUD")]
    public float vidaInicial;
    public float vidaAtual;
    public float porcentVida;
    public Image imgVida;

    [Header("TEMPOFASE")]
    public float relogio;
    public TextMeshProUGUI textoRelogio;

    [Header("RayCast")]
    public LayerMask layerInter;
    public Transform posicaoRay;
    public float alcandeRaio;
    public float distanciaRaio;
    private Ray raio;
    private RaycastHit rayInfo;
    public GameObject plantarArvore;
    public GameObject infoPlantarArvore;
    public TextMeshProUGUI textQuantArvoresPlantar;
    public int quantArvoresPlantar;
    public bool validadorPlantou;
    public string tag;
    public GameObject particulasPlantarArvore;
    public GameObject cameraPrincipal;
    public GameObject cameraAnimacao;

    void Start() {

        cam = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.visible = false;
        relogio = 60;
        infoPlantarArvore.SetActive(false);
        quantArvoresPlantar = 6;
        textQuantArvoresPlantar.text = quantArvoresPlantar.ToString();

        cameraAnimacao.SetActive(false);
    }
  
    void Update() {

        Inputplayer();

        MovimentoPersonagem();

        anim.SetBool("ground", sensor);

        relogioFase();

        RayCastPlayer();

        if (Input.GetKeyDown(KeyCode.B))
        {
            cameraAnimacao.SetActive(true);
            cameraPrincipal.SetActive(false);
        }


    }

    private void FixedUpdate() {
        sensor = Physics.CheckSphere(gruondCheck.position, 0.3f, layerInteracao);
    }

    void Inputplayer() {

        direcao = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump") && sensor) {
            velocidade.y = Mathf.Sqrt(alturaPulo * -2 * gravidade);
        }



    }

    void MovimentoPersonagem() {

        if (sensor && velocidade.y < 0) {
            velocidade.y = -0.01f;
        }


        if (direcao.magnitude > 0.1f) {

            float anguloDestino = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloDestino, ref suavizancaoVelocidade, suavizacaoMovimento);

            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            direcaoMovimento = Quaternion.Euler(0f, anguloDestino, 0f) * Vector3.forward;

            walk = true;

        }
        else {

            walk = false;

        }

        controller.Move(direcaoMovimento.normalized * velocidadeMovimento * direcao.magnitude * Time.deltaTime);

        anim.SetBool("walk", walk);

        velocidade.y += gravidade * Time.deltaTime;

        controller.Move(velocidade * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "dormir") {

            if (Input.GetKey(KeyCode.Z)) {

                anim.SetBool("dormir", true);

            }

        }

        if (other.gameObject.tag == "vida") {

            vidaAtual -= 1;
            porcentVida = vidaAtual / vidaInicial;
            imgVida.fillAmount = porcentVida;

        }

        if (other.gameObject.tag == "ganharVida") {

            vidaAtual = vidaInicial;
            porcentVida = vidaAtual / vidaInicial;
            imgVida.fillAmount = porcentVida;
            Destroy(other.gameObject);

        }


    }

    void relogioFase() {

        if (relogio > 0) {

            relogio -= Time.deltaTime;

            textoRelogio.text = relogio.ToString("0");

        }

    }

    void RayCastPlayer() {

        raio.origin = posicaoRay.position;
        raio.direction = posicaoRay.forward;
        Debug.DrawRay(raio.origin, raio.direction * alcandeRaio, Color.red);

        if (Physics.Raycast(raio.origin, raio.direction, out rayInfo, alcandeRaio, layerInter)) {

            distanciaRaio = rayInfo.distance;

            if (rayInfo.transform.gameObject.tag == "plantarArvore") {

                plantarArvore = rayInfo.transform.gameObject;
                infoPlantarArvore.SetActive(true);
               
                if (Input.GetKeyDown(KeyCode.E)) {

                    plantarArvore.GetComponent<Animator>().SetTrigger("tronco");
                    quantArvoresPlantar -= 1;
                    textQuantArvoresPlantar.text = quantArvoresPlantar.ToString();
                    plantarArvore.gameObject.tag = tag;
                    Instantiate(particulasPlantarArvore, plantarArvore.transform.position, particulasPlantarArvore.transform.rotation);

                }

            }
            else {

                plantarArvore = null;
            }
        }
        else {

            plantarArvore = null;
            infoPlantarArvore.SetActive(false);

        }
    }



}
