using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector3 = System.Numerics.Vector3;

public class Zombie : MonoBehaviour
{
    public float velocidad = 8f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    

    private bool PosicionA = false;

    private const int Animacion_Caminar = 0;
    private const int Animacion_Morir = 1;
    private bool muerte = false;
    
    private int puntaje = 0;
    private Animator animator;
    private BoxCollider2D bx;
    public GameObject ninja;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
        ninja = GameObject.Find("Ninja");
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (!muerte)
        {
            if (PosicionA == false)
            {
                sr.flipX = false;
                rb.velocity = new Vector2(velocidad, rb.velocity.y);
            }
            else
            {
                sr.flipX = true;
                rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            }
        }
        else
        {
            Debug.Log("sdsadasdasddsadasdasdsad");
            CambiarAnimacion(Animacion_Morir);
            bx.enabled = false;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Posicion2")
        {
            PosicionA = true;
        }

        if (collision.gameObject.tag == "Posicion1")
        {
            PosicionA = false;
        }

        if (!muerte)
        {
            if (collision.gameObject.tag == "Bala")
            {

                
                muerte = true;
            }
        }
        if(collision.gameObject.tag == "Bala")
        {
            ninja.GetComponent<PlayerNinja>().puntaje = ninja.GetComponent<PlayerNinja>().puntaje+10;
            muerte = true;
            Destroy(collision.gameObject);
            bx.enabled = false;
        }

    }
    private void CambiarAnimacion(int animation)
    {
        animator.SetInteger("estado", animation);
    }

}