using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfProperties : MonoBehaviour
{
    private Transform Parent;
    private int ChildCount;
    private Rigidbody2D rbChild; 

    // Start is called before the first frame update
    void Start()
    {
        Parent = this.gameObject.transform;
        ChildCount = Parent.childCount;

        for (int i = 0; i < ChildCount; i++)
        {
            Parent.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
            rbChild = Parent.GetChild(i).gameObject.GetComponent<Rigidbody2D>();
            rbChild.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;

            Parent.GetChild(i).gameObject.AddComponent<EdgeCollider2D>();
             
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

