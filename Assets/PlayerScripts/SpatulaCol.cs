using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class SpatulaCol : MonoBehaviour //NB Add this script to the spatula object before you make it a prefab!!!
{
    private GameObject spatula;

    // Start is called before the first frame update
    void Start()
    {
        spatula = this.gameObject;
        //spatula.AddComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(spatula);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

        }
    }


}
