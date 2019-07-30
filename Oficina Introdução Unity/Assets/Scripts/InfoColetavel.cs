using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova infoColetavel", menuName = "InfoColetavel")]
public class InfoColetavel : ScriptableObject
{
    [Tooltip("Se esse GameObject será destruído ao colidir com um outro")]
    public bool destruirColetavel;
    [Tooltip("O som que tocará ao objeto ser coletado")]
    public AudioClip somATocar;
    [Tooltip("O valor que será adicionado à pontuação do jogador ao objeto ser coletado")]
    public float valorDoColetavel;
}
