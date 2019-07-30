using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    
    public int vida;
    public int numDeCoracoes;

    public Image[] coracoes;
    public Sprite coracaoCheio;
    public Sprite coracaoVazio;

    void Update()
    {
        if(vida > numDeCoracoes)
        {
            vida = numDeCoracoes;
        }

        for (int i = 0; i < coracoes.Length; i++)
        {
            if(i < vida)
            {
                coracoes[i].sprite = coracaoCheio;
            }
            else
            {
                coracoes[i].sprite = coracaoVazio;
            }


            if (i < numDeCoracoes)
            {
                coracoes[i].enabled = true;
            }
            else
            {
                coracoes[i].enabled = false;
            }
        }
    }


}
