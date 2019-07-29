using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentoDeProjetil : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody2D rb2d;
    [Tooltip("Velocidade inicial do projetil")] public Vector3 velocidadeInicial = 100 * Vector3.right;

    // OnValidate é chamado sempre que você fizer uma alteração no inspector
    void OnValidate(){
        if(gameObject.GetComponent<Rigidbody>() == null && gameObject.GetComponent<Rigidbody2D>() == null){
            Debug.LogError("Projetil necessita de um rigidbody");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb2d = GetComponent<Rigidbody2D>();

        if(rb == null){
            if(rb2d == null){
                Debug.LogError("Projetil necessita de um rigidbody");
            }
            else{
                rb2d.velocity = velocidadeInicial;
            }
        }
        else{
            rb.velocity = velocidadeInicial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
