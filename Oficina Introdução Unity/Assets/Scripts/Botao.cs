using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    [Tooltip("Lista de componentes a serem ativados enquanto o botão estiver pressionado")] 
    public List<Behaviour> componentesParaAtivar;

    [Tooltip("Caso ativo, quando o jogador sair de cima do botão, os componentes serão desativados")]
    public bool desativarAoSair;

    [Tooltip("Qual tag possui o objeto do jogador (necessário para diferenciar se ele ativou o botão)")]
    public string tagDoJogador = "Player";

    public void MudarEstadoDosComponentes(bool status){
        foreach(Behaviour componente in componentesParaAtivar){
            componente.enabled = status;
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.tag != tagDoJogador){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(true);
        }
    }
    
    public void OnTriggerExit2D(Collider2D col){
        if(col.tag != tagDoJogador || !desativarAoSair){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(false);
        }
    }

    public void OnTriggerEnter(Collider col){
        if(col.tag != tagDoJogador){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(true);
        }
    }
    
    public void OnTriggerExit(Collider col){
        if(col.tag != tagDoJogador || !desativarAoSair){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag != tagDoJogador){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(true);
        }
    }
    
    public void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.tag != tagDoJogador || !desativarAoSair){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(false);
        }
    }

    public void OnCollisionEnter(Collision col){
        if(col.gameObject.tag != tagDoJogador){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(true);
        }
    }
    
    public void OnCollisionExit(Collision col){
        if(col.gameObject.tag != tagDoJogador || !desativarAoSair){ 
            return;
        }
        else{
            MudarEstadoDosComponentes(false);
        }
    }

    public void Start(){
        MudarEstadoDosComponentes(false);
    }
}
