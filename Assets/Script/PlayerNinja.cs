using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNinja : MonoBehaviour

{
    public float velocidadCaminar = 5;
    public float velocidadCorrer = 10;
    private Animator animator;
    private Rigidbody2D rb;
    public float fuerzaSalto = 10f;

   

    private Transform tr;
    

    private SpriteRenderer sr;
    private const int Animacion_Quieto = 1;
    private const int Animacion_Saltar = 2;
    private const int Animacion_Correr = 3;
    private const int Animacion_Morir = 4;
    private const int Animacion_Disparar = 5;

    private int EstadoSalto = 0;
    private bool EstadoGanar = false;
    private bool EstadoSaltoDoble = false;
    private bool Caminar = false;

    public bool EstadoMuerte = false;
    
    public Transform kunaiTransform;
    public GameObject kunai;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EstadoGanar == false)
        {


            if (EstadoMuerte == false)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    MovimientoDerecha();
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    MovimientoIzquierda();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) && !EstadoSaltoDoble)
                {
                    Saltar();
                }
                else if (Input.GetKeyDown(KeyCode.X) )
                {
                    CambiarAnimacion(Animacion_Disparar); 
                    Instantiate(kunai, kunaiTransform.position, Quaternion.identity);
      
                }

                else
                {
                    EstarQuieto();
                }
            }
            else
            {
                CambiarAnimacion(Animacion_Morir);
                Debug.Log("Muerte");
            }
        }
        else
        {
            Debug.Log("Usted es el ganador");
            CambiarAnimacion(Animacion_Quieto);
        }
    }

    private void CambiarAnimacion(int animation)
    {
        animator.SetInteger("Estado", animation);
    }

    private void Saltar()
    {
        rb.velocity = Vector2.up * fuerzaSalto;
        CambiarAnimacion(Animacion_Saltar);
        EstadoSalto++;
        if (EstadoSalto == 2)
        {
            Debug.Log("xxxx");
            EstadoSaltoDoble = true;
        }
    }

    private void MovimientoIzquierda()
    {
        tr.localScale = new Vector3(-2.427162f,2.06999f,1);
        rb.velocity = new Vector2(-velocidadCaminar, rb.velocity.y);
        CambiarAnimacion(Animacion_Correr);
      
        if (Input.GetKeyDown(KeyCode.UpArrow) && !EstadoSaltoDoble)
        {
            Saltar();
        }
    }

    private void MovimientoDerecha()
    {
        tr.localScale = new Vector3(2.427162f,2.06999f,1);
        rb.velocity = new Vector2(velocidadCaminar, rb.velocity.y);
        CambiarAnimacion(Animacion_Correr);
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && !EstadoSaltoDoble)
        {
            Saltar();
        }
    }

    private void EstarQuieto()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        CambiarAnimacion(Animacion_Quieto);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Muerte")
        {
            EstadoMuerte = true;
        }

        if (collision.gameObject.tag == "Suelo")
        {
            EstadoSaltoDoble = false;
            EstadoSalto = 0;
            Debug.Log("aaaaaa");
        }

        if (collision.gameObject.tag == "key")
        {
            Destroy(collision.gameObject);
            EstadoGanar = true;
        }
    }
    
}
