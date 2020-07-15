using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento : MonoBehaviour
{

    //----------------------------------------------------VARIÁVEIS----------------------------------------------------//
    public float Velocidade = 10;

    public float Girar = 200;

    private Rigidbody rigidbodyJogador;

    public Vector3 direcaoDoPulo = new Vector3(0, 1, 0);
    
    public float forcaDoPulo = 5.0f;
    
    public float DistanciaDoChao = 1;

    public float TempoPorPulo = 1.5f;

    public LayerMask LayersNaoIgnoradas = -1;

    private bool estaNoChao, contar = false;

    private float cronometro = 0;

    private Rigidbody corpoRigido;

    //------------------------------------------------------------------------------------------------------------------//

    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();

        rigidbodyJogador = GetComponent<Rigidbody>();
    }

    //------------------------------------------------------------------------------------------------------------------//

    void Update()
    {
        //-------------------------------------------------------------------PULAR--------------------------------------------------------//

        estaNoChao = Physics.Linecast(transform.position, transform.position - Vector3.up * DistanciaDoChao, LayersNaoIgnoradas);

            if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == true && contar == false)
            {
                corpoRigido.AddForce(direcaoDoPulo * forcaDoPulo, ForceMode.Impulse);

                estaNoChao = false;

                contar = true;
            }

            if (contar == true)
            {
                cronometro += Time.deltaTime;
            }
            if (cronometro >= TempoPorPulo)
            {
                contar = false;

                cronometro = 0;
            }

        //------------------------------------------------------MOVIMENTAÇÃO-------------------------------------------------//
        float translate = (Input.GetAxis("Vertical") * Velocidade) * Time.deltaTime;
        float rotate = (Input.GetAxis("Horizontal") * Girar) * Time.deltaTime;

        
        rigidbodyJogador.MovePosition(rigidbodyJogador.position + (transform.forward * translate));

        
        Vector3 rotation = transform.up * rotate;
        
        Quaternion deltaRotation = Quaternion.Euler(rotation);

        
        rigidbodyJogador.MoveRotation(rigidbodyJogador.rotation * deltaRotation);
    }
}
