using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    private float pontuacao;
    [Tooltip("Se o jogo fará alguma coisa ao atingir uma determinada pontuação máxima")]
    public bool temPontuacaoObjetivo;
    [Tooltip("A pontuação a ser atingida para fazer com que o jogo faça alguma coisa")]
    public float pontuacaoObjetivo;

    [Tooltip("O nome da cena que será carregada quando algum objeto colidir com esse. \nDeixar em branco se não for carregar uma cena")]
    public string cenaParaCarregar;
    [Tooltip("O objeto que será ativado quando algum objeto colidir com esse. \nDeixar em branco se não for ativar nenhum objeto")]
    public List<GameObject> objetosParaAtivar = new List<GameObject>();
    [Tooltip("Quantos segundos a cena demorará para ser carregada. Caso a transição seja instantânea, deixe em branco")]
    public float atrasoCarregamentoCena;
    [Tooltip("Quantos segundos o objeto demorará para ser ativo. Caso a ativação seja instantâneo, deixe em branco")]
    public float atrasoAtivacaoObjeto;

    [Tooltip("Lista de componentes Text que vão apresentar a pontuação do jogador")]
    public List<Text> displaysPontuacao = new List<Text>();

    private void Update()
    {
        foreach(Text texto in displaysPontuacao)
        {
            if(texto != null)
                texto.text = pontuacao.ToString();
        }

        if(temPontuacaoObjetivo && pontuacao >= pontuacaoObjetivo)
        {
            TratarEfeitos();
        }
    }

    public void AumentarPontuacao(float incremento)
    {
        pontuacao += incremento;
    }

    // Realiza as operações desejadas ao jogador obter a pontuação objetivo
    public void TratarEfeitos()
    {
        if (objetosParaAtivar.Count > 0)
        {
            StartCoroutine(AtivarObjetos());
        }

        if (cenaParaCarregar != "")
        {
            StartCoroutine(CarregarCena());
        }
    }

    IEnumerator AtivarObjetos()
    {
        for (float i = 0; i < atrasoAtivacaoObjeto; i += Time.deltaTime)
        {
            yield return null;
        }

        foreach (GameObject go in objetosParaAtivar)
        {
            go.SetActive(true);
        }
    }

    IEnumerator CarregarCena()
    {
        for (float i = 0; i < atrasoCarregamentoCena; i += Time.deltaTime)
        {
            yield return null;
        }

        SceneManager.LoadScene(cenaParaCarregar);
    }
}
