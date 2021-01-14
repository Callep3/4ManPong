using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.XR;

public class Player : NetworkBehaviour
{
    private Vector3 movement = new Vector3();
    [SerializeField] private float speed;
    private NetworkManager networkManager;
    private int amountOfPlayers;

    private void Start()
    {
        speed = 2f;
        networkManager = FindObjectOfType<NetworkManager>();
        transform.Rotate(0, 0, 0);
    }

    [Client]
    private void Update()
    {
        if (!hasAuthority) return;

        CmdMove();
    }
    
    private void CmdMove()
    {
        if (netId == 2 || netId == 3)
        {
            transform.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0) * (speed * Time.deltaTime);
        }
        if (netId == 4 || netId == 5)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * (speed * Time.deltaTime);
        }
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        amountOfPlayers = NetworkServer.connections.Count;
        
        if (netId == 4 || netId == 5)
        {
            transform.Rotate(0f, 0f, -90f);
        }
        
        Debug.Log(amountOfPlayers);
    }
}
