using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public enum TiposDeAbertura{
        Escorrega,
        Rotaciona
    }

    public enum TipoDeJogo{
        Jogo_2D,
        Jogo_3D
    }

    [Tooltip("Indica o lado que a porta irá abrir. Mostrado pela linha amarela")] 
    public Vector3 direcaoQueAbrira = Vector3.up;
    [Tooltip("Indica o modo que a porta se modifica quando é aberta")] 
    public TiposDeAbertura modoDeAbrir;

    [Tooltip("Indica o pivô ao redor do qual a porta girará. Ignore caso a porta esteja em modo de escorregar. Mostrado pela bola verde")]
    public Vector3 pivotDeRotacao;

    [Tooltip("Indica o eixo ao redor do qual a porta girará. Ignore caso a porta esteja em modo de escorregar. Mostrado pela bola azul")]
    public Vector3 eixoDeRotacao;

    [Tooltip("Indica o ângulo de giro da porta. Ignore caso a porta esteja em modo de escorregar.")]
    public float anguloDeRotacao = 60;

    [Tooltip("Indica o quanto a porta irá escorregar. Ignore caso a porta esteja em modo de rotacionar. Mostrado pela linha verde")]
    public float deslocamentoDaPorta;
    [Tooltip("A porta está aberta?")]
    public bool portaAberta = false;

    [Header("Interação porta-jogador")]
    [Tooltip("Tecla que abre a porta")]
    public KeyCode botaoParaAbrir;

    [Tooltip("Distância maxima entre player e porta para que o mesmo possa a abrir. Mostrado pela bola vermelha")]
    public float distanciaMaxima;

    [Tooltip("Qual tag possui o objeto do jogador (necessário para diferenciar se ele ativou o botão)")]
    public string tagDoJogador = "Player";
    [Tooltip("Define se este é um projeto 2D ou 3D")]
    public TipoDeJogo tipoDeJogo = TipoDeJogo.Jogo_2D;

    void InteragePorta(){
        // A seguinte parte deste código pode estar te assustando:
        // (portaAberta ? 1 : -1)
        // Então vou explicar o que ela significa! Basicamente, é como se a gente
        // perguntasse para o Unity se portaAberta é verdadeiro ou não; se for,
        // a gente diz para ele usar o 1, se não, usar o -1
        portaAberta = !portaAberta;
        switch(modoDeAbrir){
            case TiposDeAbertura.Escorrega:
                transform.Translate((portaAberta ? 1 : -1) * direcaoQueAbrira * deslocamentoDaPorta);
                break;
            case TiposDeAbertura.Rotaciona:
                transform.RotateAround(pivotDeRotacao, eixoDeRotacao, (portaAberta ? 1 : -1) * anguloDeRotacao);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(botaoParaAbrir)){
            switch(tipoDeJogo){
                case TipoDeJogo.Jogo_2D:
                    Collider2D[] resultados = Physics2D.OverlapCircleAll(transform.position, distanciaMaxima);
                    foreach(var resultado in resultados){
                        if(resultado.tag == tagDoJogador){
                            InteragePorta();
                            return;
                        }
                    }
                    break;
                case TipoDeJogo.Jogo_3D:
                    Collider[] resultados3d = Physics.OverlapSphere(transform.position, distanciaMaxima);
                    foreach(var resultado in resultados3d){
                        if(resultado.tag == tagDoJogador){
                            InteragePorta();
                            return;
                        }
                    }
                    break;
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        switch(modoDeAbrir){
            case TiposDeAbertura.Escorrega:
                Gizmos.DrawLine(transform.position, (transform.position + deslocamentoDaPorta*direcaoQueAbrira));
                break;
            case TiposDeAbertura.Rotaciona:
                Gizmos.DrawWireSphere(transform.position + pivotDeRotacao, 0.1f);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, (transform.position + eixoDeRotacao));
                break;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + direcaoQueAbrira);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaMaxima);
    }
}
