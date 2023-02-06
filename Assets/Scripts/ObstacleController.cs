using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private PlayerSpawnerController playerSpawnerScript;
    private GameObject playerSpawnerGO;

    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerScript=playerSpawnerGO.GetComponent<PlayerSpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) //burda other player oluyor... obstacle'a trigger olduðunda (deðdiðinde) ...
    {
        if (other.tag=="Player")
        {
            playerSpawnerScript.PlayerGotKilled(other.gameObject); //collide olduðunda bu metodu çalýþtýr yani playerý yok et
        }
    }
}
