using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    private float pontuacao;

    [Tooltip("Lista de componentes Text que vão apresentar a pontuação do jogador")]
    public List<Text> displaysPontuacao = new List<Text>();

    private void Update()
    {
        foreach(Text texto in displaysPontuacao)
        {
            if(texto != null)
                texto.text = pontuacao.ToString();
        }
    }

    public void AumentarPontuacao(float incremento)
    {
        pontuacao += incremento;
    }
}
