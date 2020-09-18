using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public float velocidad = 10f;
    private Rigidbody2D rb;

    public GameObject player;
    private Transform playerTransform;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,3f);
        player = GameObject.FindGameObjectWithTag("Ninja");
        playerTransform = player.transform;
        if (playerTransform.localScale.x>0)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
            transform.localScale = new Vector3(3f,9f,1);
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            transform.localScale = new Vector3(-3f,9f,1);
        }

    }
    

    // Update is called once per frame
    void Update()
    {
      

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Muerte")
        {
            Destroy(gameObject);
        }

    }
}
