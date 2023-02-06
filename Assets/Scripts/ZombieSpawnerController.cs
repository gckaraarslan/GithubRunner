using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerController : MonoBehaviour
{
    public GameObject zombieGO;
    public int zombieCount=0;
    public List<GameObject> zombieList = new List<GameObject>();

    public GameObject playerSpawnerGO;
    public PlayerSpawnerController playerSpawnerScript;

   public bool isZombieAttacking;

    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerScript= playerSpawnerGO.GetComponent<PlayerSpawnerController>();
        SpawnZombies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnZombies()
    {
        for (int i = 0; i < zombieCount; i++)
        {
            Quaternion zombieRotation = Quaternion.Euler(new Vector3(0,180,0)); //quaternion.identity'nin ters dönmüþ hali,baktýðý yönü terse çeviriyoruz.
            GameObject zombie = Instantiate(zombieGO,GetZombiePosition(),zombieRotation, transform);    
            ZombieController zombieScript= zombie.GetComponent<ZombieController>();
            zombieScript.playerSpawnerGO= playerSpawnerGO;
            zombieScript.zombieSpawnerScript = this;    //yaratýrken ihtiyacý olan þeyler bu ikisi

            zombieList.Add(zombie);
        }
    }
    public Vector3 GetZombiePosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        return newPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            GetComponent<BoxCollider>().enabled= false;
            playerSpawnerScript.ZombieDetected(gameObject);
            LookAtPlayers(other.gameObject);
            isZombieAttacking=true;
        }
    }

    private void LookAtPlayers(GameObject target)
    {
        Vector3 dir=transform.position- target.transform.position;
        Quaternion lookRotation=Quaternion.LookRotation(dir);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = lookRotation;
    }

    public void ZombieAttackThisCop(GameObject player,GameObject zombie)
    {
        zombieList.Remove(zombie);
        CheckZombieCount();
        playerSpawnerScript.PlayerGotKilled(player);
        Destroy(zombie);
    }
    private void CheckZombieCount() //bu metodun adýný ifAllZombiesAreDead gibi düþün...
    {
        if (zombieList.Count<=0)
        {
            playerSpawnerScript.AllZombiesKilled(); //playerý hareket ettir...
           
        }
    }
    public void ZombieGotShot(GameObject zombie)
    {
        zombieList.Remove(zombie);
        Destroy(zombie);
        CheckZombieCount();

    }



}
