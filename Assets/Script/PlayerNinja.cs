using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNinja : MonoBehaviour

{
    public float velocidadCaminar = 5;
    public float velocidadCorrer = 10;
    private Animator animator;
    private Rigidbody2D rb;
    public float fuerzaSalto = 10f;


    [HideInInspector] public bool usingLadder = false;
    private Transform tr;

    public Text puntajetext;

    private SpriteRenderer sr;
    private const int Animacion_Quieto = 1;
    private const int Animacion_Saltar = 2;
    private const int Animacion_Correr = 3;
    private const int Animacion_Morir = 4;
    private const int Animacion_Disparar = 5;
    private const int Animacion_SubirEscalera = 6;
    private const int Animacion_Planear = 7;
    private const int Animacion_Deslizarse = 8;

    private bool trepar = false;
    private bool EstadoGanar = false;
    private bool Caminar = false;

    private int EstadoSalto = 0;
    private bool EstadoSaltoDoble = false;
    

    public bool EstadoMuerte = false;
    
    public Transform kunaiTransform;
    public GameObject kunai;

    public int vidas = 3;
    public int puntaje = 0;
    
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
        puntajetext.text = "Puntaje: " + puntaje + " Vida: " + vidas;

        if (EstadoGanar == false)
        {


            if (EstadoMuerte == false)
            {
                Debug.Log(rb.velocity.y);
                Caida();
                Muerte();
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
               
                else if (trepar && Input.GetKey(KeyCode.A)  )
                {
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(rb.velocity.x,0);
                    animator.SetInteger("Estado",Animacion_SubirEscalera);
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        rb.velocity=new Vector2(rb.velocity.x,velocidadCaminar);
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        rb.velocity=new Vector2(rb.velocity.x,-velocidadCaminar);
                    }                    
                    //CambiarAnimacion(Animacion_Disparar); 
      
                }
                else if (Input.GetKey(KeyCode.Z) )
                {
                    Planear();
                }
                else if (Input.GetKey(KeyCode.C) )
                {
                   CambiarAnimacion(Animacion_Deslizarse);
                }

                else
                {
                    EstarQuieto();
                    Debug.Log("Quieto");
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
     //   rb.velocity = new Vector2(rb.velocity.x, 0);
    
        rb.velocity = Vector2.up * fuerzaSalto;
        CambiarAnimacion(Animacion_Saltar);
        EstadoSalto++;
        if (EstadoSalto == 2)
        {
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

      /*Escalera
       if (!usingLadder)
        {
             CambiarAnimacion(Animacion_SubirEscalera);
             rb.velocity = new Vector2(0, rb.velocity.y);
        }*/
    }

    private void EstarQuieto()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        CambiarAnimacion(Animacion_Quieto);
    }
    private void Caida()
    {
        if (rb.velocity.y<-147)
        {
            EstadoMuerte = true;
            vidas = 0;
        }
    }
    
    private void Planear()
    {
        if (rb.velocity.y<-20)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                rb.velocity = new Vector2(rb.velocity.x, -30);
                CambiarAnimacion(Animacion_Planear);
            }
        }
    }

    public void Muerte()
    {
        if (vidas==0)
        {
            EstadoMuerte = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            trepar = false;
            rb.gravityScale = 25;
            CambiarAnimacion(Animacion_Quieto);
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            trepar = true;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Muerte")
        {
            vidas = vidas - 1;
           // EstadoMuerte = true;
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
