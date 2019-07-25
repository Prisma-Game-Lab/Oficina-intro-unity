using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTerceiraPessoa : MonoBehaviour
{
    [Tooltip("Se o cursor do mouse deve ficar travado ou livre")]
    [SerializeField] private bool travarCursor;
    [Tooltip("A sensibilidade do movimento da câmera baseado no mouse")]
    [SerializeField] private float sensibidadeMouse = 10;
    [Tooltip("GameObject que a câmera orbitará em volta (centro da órbita)")]
    [SerializeField] private Transform centroRotacaoCamera;
    [Tooltip("Distância que a câmera ficará do centro da órbita")]
    [SerializeField] private float distanciaDoCentro = 2;
    [Tooltip("Mínimo (x) e máximo (y) da inclinação da câmera")]
    [SerializeField] private Vector2 minMaxInclinacao = new Vector2(-40, 85);

    // É o tempo que a câmera demorará para concluir a rotação indicada pelo movimento do mouse.
    // Esse atraso dá uma sensação de um movimento mais suave do que se fosse imediato
    [Tooltip("Nível de suavidade do movimento da câmera")]
    [SerializeField] private float tempoDeSuavizacaoRotacao = .12f;
    Vector3 velocidadeDaSuavizacaoRotacao;
    Vector3 rotacaoAtual;

    float guinada;      // Rotação na horizontal
    float inclinacao;   // Rotação na vertical

    void Start()
    {
        if (travarCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        guinada += Input.GetAxis("Mouse X") * sensibidadeMouse;
        inclinacao -= Input.GetAxis("Mouse Y") * sensibidadeMouse;
        inclinacao = Mathf.Clamp(inclinacao, minMaxInclinacao.x, minMaxInclinacao.y);

        rotacaoAtual = Vector3.SmoothDamp(rotacaoAtual, new Vector3(inclinacao, guinada), 
            ref velocidadeDaSuavizacaoRotacao, tempoDeSuavizacaoRotacao);

        transform.eulerAngles = rotacaoAtual;
        transform.position = centroRotacaoCamera.position - transform.forward * distanciaDoCentro;
    }
}

