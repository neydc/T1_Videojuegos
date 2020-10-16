using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Escalera : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator myAnim;

    private PlayerNinja controller;
    public BoxCollider2D platformGround;
    
    [HideInInspector]
    public bool onLadder= false;

    public float climbSpeed;

    public float exitHop;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        controller = GetComponent<PlayerNinja>();
    }



    private void OnTriggersStay2D(Collider2D collider)
    {
        if (collider.CompareTag("ladder"))
        {
            if (Input.GetAxisRaw("Vertical")!=0)
            {
                rb.velocity=new Vector2(rb.velocity.x,Input.GetAxisRaw("Vertical")*climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;
                platformGround.enabled = false;
                controller.usingLadder = onLadder;
            }else if (Input.GetAxisRaw("Vertical")==0&&onLadder)
            {
                rb.velocity = new Vector2(rb.velocity.x,0);
            }
        }
    }
}
