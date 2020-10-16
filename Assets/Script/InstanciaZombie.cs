using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaZombie : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zombie;
    public int contador =   0;   
    public Transform zombieTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        InstanciaZombieAleatorio();
        
        contador = contador +1;
    }

    private void InstanciaZombieAleatorio()
    {
        
        if (contador==3000)
        {
            Instantiate(zombie, zombieTransform.position, Quaternion.identity);
            contador = 0;
        }
      
    }
}
