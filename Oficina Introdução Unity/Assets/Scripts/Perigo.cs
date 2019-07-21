using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Perigo : MonoBehaviour
{
    [Tooltip("Lista de tagss do objeto que causará algum efeito ao colidir com o perigo")] 
    public List<string> tagObjetoAReagir = new List<string>();
    [Tooltip("O nome da cena que será carregada quando algum objeto colidir com esse. \nDeixar em branco se não for carregar uma cena")]
    public string cenaParaCarregar;
    [Tooltip("O objeto que será ativado quando algum objeto colidir com esse. \nDeixar em branco se não for ativar nenhum objeto")]
    public List<GameObject> objetosParaAtivar = new List<GameObject>();
    [Tooltip("Quantos segundos a cena demorará para ser carregada. Caso a transição seja instantânea, deixe em branco")]
    public float atrasoCarregamentoCena;
    [Tooltip("Quantos segundos o objeto demorará para ser ativo. Caso a ativação seja instantâneo, deixe em branco")]
    public float atrasoAtivacaoObjeto;

    // Colisões com triggers =============================

    private void OnTriggerEnter(Collider collision)
    {
        foreach(string tagObjeto in tagObjetoAReagir)
        {
            if (tagObjeto == collision.gameObject.tag)
            {
                Destroy(collision.gameObject);

                TratarEfeitosSecundarios();
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tagObjeto in tagObjetoAReagir)
        {
            if (tagObjeto == collision.gameObject.tag)
            {
                Destroy(collision.gameObject);

                TratarEfeitosSecundarios();
                break;
            }
        }
    }

    // Colisões com colliders normais =============================

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string tagObjeto in tagObjetoAReagir)
        {
            if (tagObjeto == collision.gameObject.tag)
            {
                Destroy(collision.gameObject);

                TratarEfeitosSecundarios();
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tagObjeto in tagObjetoAReagir)
        {
            if (tagObjeto == collision.gameObject.tag)
            {
                Destroy(collision.gameObject);

                TratarEfeitosSecundarios();
                break;
            }
        }
    }

    // Realiza todas as operações além de destruir o game object
    public void TratarEfeitosSecundarios()
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
        for (float i = 0; i < atrasoAtivacaoObjeto ; i += Time.deltaTime)
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
        for(float i = 0 ; i < atrasoCarregamentoCena ; i += Time.deltaTime)
        {
            yield return null;
        }

        SceneManager.LoadScene(cenaParaCarregar);
    }
}
