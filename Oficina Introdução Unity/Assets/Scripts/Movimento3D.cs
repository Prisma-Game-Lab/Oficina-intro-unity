using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movimento3D : MonoBehaviour
{
    [Tooltip("Câmera que será usada para")]
    public Camera cam;

    [Tooltip("Nome do input para mover o objeto para frente e para trás")]
    [SerializeField] private string inputHorizontal;
    [Tooltip("Nome do input para mover o objeto para esquerda e direita")]
    [SerializeField] private string inputVertical;

    [Tooltip("A força aplicada para manter o jogador colado ao chão em uma descida enclinada")]
    public float forcaRampa;
    [Tooltip("O comprimento do raio que checa se o jogador está em uma rampa ou não")]
    public float comprimentoRaioForcaRampa;

    [Tooltip("A taxa de diminuição de força que o pulo sofrerá até seu ápice")]
    public AnimationCurve decrescimentoDoPulo;
    [Tooltip("A 'força' do pulo")]
    public float multiplicadorPulo;
    [Tooltip("A tecla usada para pular")]
    public KeyCode teclaDePulo;


    private bool isJumping;

    [Tooltip("Velocidade com a qual o objeto se moverá")]
    public float velocidadeMovimento;

    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        float horizInput = Input.GetAxis(inputHorizontal);
        float vertInput = Input.GetAxis(inputVertical);

        charController.SimpleMove(Vector3.ClampMagnitude((vertInput * cam.transform.forward +
                                    horizInput * cam.transform.right) * velocidadeMovimento , 1.0f) * velocidadeMovimento);

        if((horizInput != 0 || vertInput != 0) && CheckSlope())
        {
            charController.Move(Vector3.down * charController.height / 2 * forcaRampa * Time.deltaTime);
        }

        if(Input.GetKeyDown(teclaDePulo) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private bool CheckSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * comprimentoRaioForcaRampa))
            if (hit.normal != Vector3.up)
                return true;

        return false;
    }

    private IEnumerator JumpEvent()
    {
        float timeInAir = 0f;
        do
        {
            float jumpForce = decrescimentoDoPulo.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * multiplicadorPulo * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 90.0f;
        isJumping = false;
    }
}