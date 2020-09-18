using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =new  Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
