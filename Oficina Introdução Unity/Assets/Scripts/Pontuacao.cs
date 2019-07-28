using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    private int pontuacao;

    [Tooltip("Lista de textos que vão apresentar a pontuação do jogador")]
    public List<Text> displaysPontuacao = new List<Text>();

    private void Update()
    {
        foreach(Text texto in displaysPontuacao)
        texto.text = pontuacao.ToString();
    }

    public void AumentarPontuacao(int incremento)
    {
        pontuacao += incremento;
    }
}
