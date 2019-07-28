using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movimento3D : MonoBehaviour
{
    [Tooltip("Câmera que será usada para")]
    public Camera cam;

    private string inputHorizontal = "Horizontal";
    private string inputVertical = "Vertical";

    private float forcaRampa = 5f;
    private float comprimentoRaioForcaRampa = 2f;

    [Tooltip("A taxa de diminuição de força que o pulo sofrerá até seu ápice")]
    public AnimationCurve decrescimentoDoPulo;
    [Tooltip("A 'força' do pulo")]
    public float forcaPulo;
    [Tooltip("A tecla usada para pular")]
    public KeyCode teclaDePulo;

    [Tooltip("Quão suave será a rotação do personagem")]
    public float turnSmoothVelocity;
    float turnSmoothTime;

    private bool isJumping;

    [Tooltip("Velocidade com a qual o objeto se moverá")]
    public float velocidadeMovimento;

    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        if(cam == null)
            cam = GetComponentInChildren<Camera>();
    }

    float horizInput;
    float vertInput;
    Vector3 direcaoFrente = Vector3.zero;
    private void Update()
    {
        horizInput = Input.GetAxis(inputHorizontal);
        vertInput = Input.GetAxis(inputVertical);

        Debug.Log(cam.transform.forward);

        if (Input.GetKeyDown(teclaDePulo) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
        else
        {

            if(horizInput != 0 || vertInput != 0)
            {
                float targetRotation = Mathf.Atan2(horizInput, vertInput) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
                if (vertInput < 0)
                    vertInput = vertInput * -1;
            }
            charController.SimpleMove(Vector3.ClampMagnitude((vertInput * transform.forward +
                                        horizInput * cam.transform.right), 1.0f) * velocidadeMovimento);
        }

        if((horizInput != 0 || vertInput != 0) && CheckSlope())
        {
            charController.Move(Vector3.down * charController.height / 2 * forcaRampa * Time.deltaTime);
        }

    }

    private bool CheckSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2f * comprimentoRaioForcaRampa))
            if (hit.normal != Vector3.up)
                return true;

        return false;
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0f;
        do
        {
            float jumpForce = decrescimentoDoPulo.Evaluate(timeInAir);
            charController.Move((Vector3.ClampMagnitude((vertInput * transform.forward +
                                    horizInput * cam.transform.right), 1.0f) * velocidadeMovimento  
                                    + Vector3.up *jumpForce * forcaPulo) * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }
}