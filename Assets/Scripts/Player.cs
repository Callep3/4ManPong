using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private Vector3 movement = new Vector3();
    [SerializeField] private float speed;

    private void Start()
    {
        speed = 2f;
    }

    [Client]
    private void Update()
    {
        if (!hasAuthority) return;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)
        || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.rotation.z == 0)
            {
                CmdMove("Vertical");
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
        || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.rotation.z == -90)
            {
                CmdMove("Horizontal");
            }
        }
    }
    
    private void CmdMove(String movementType)
    {
        if (movementType == "Vertical")
        {
            transform.position += new Vector3(0, Input.GetAxisRaw(movementType), 0) * (speed * Time.deltaTime);
        }
        if (movementType == "Horizontal")
        {
            transform.position += new Vector3(Input.GetAxisRaw(movementType), 0, 0) * (speed * Time.deltaTime);
        }
    }
}
