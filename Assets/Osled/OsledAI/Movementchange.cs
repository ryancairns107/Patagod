using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementchange : MonoBehaviour
{
    public float moveforce = 0f;
    private Rigidbody rb;
    public Vector3 moved;
    public LayerMask hmm;
    public float maxdis = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moved = chose();
        transform.rotation = Quaternion.LookRotation(moved);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moved * moveforce;
        if (Physics.Raycast(transform.position, transform.forward, maxdis, hmm))
        {
            moved = chose();
            transform.rotation = Quaternion.LookRotation(moved);
        }
    }
    Vector3 chose()
    {
        System.Random ran = new System.Random();
        int i = ran.Next(0, 3);
        Vector3 Teep = new Vector3();
        if (i == 0)
        {
            Teep = transform.forward;
        }
        else if (i == 1)
        {
            Teep = -transform.forward;
        }
        else if (i == 2)
        {
            Teep = transform.right;
        }
        else if (i == 3)
        {
            Teep = -transform.right;
        }
        return Teep;




    }
}