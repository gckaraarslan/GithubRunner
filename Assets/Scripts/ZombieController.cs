using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject playerSpawnerGO;
    public ZombieSpawnerController zombieSpawnerScript;
    private bool isZombieAlive;
    void Start()
    {
        isZombieAlive = true;
    }

    // Update is called once per frame
    void FixedUpdate()      //fiziksel olaylar yaptýðýmýzda fixedupdate iyi olur
    {
        if (zombieSpawnerScript.isZombieAttacking==true)    
        {
            transform.position = Vector3.MoveTowards(transform.position, playerSpawnerGO.transform.position, Time.fixedDeltaTime * 1.5f);
        }
       
    }
    private void OnCollisionEnter(Collision collision)  //is trigger olmayanlar için 
    {
        if (collision.gameObject.tag=="Player" && isZombieAlive==true)
        {
            isZombieAlive = false;
            zombieSpawnerScript.ZombieAttackThisCop(collision.gameObject, this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Bullet")
        {
           zombieSpawnerScript.ZombieGotShot(this.gameObject);
        }
    }
}
