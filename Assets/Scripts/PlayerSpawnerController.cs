using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    public GameObject playerGO;
    float playerSpeed = 5;
    float xSpeed;
    float maxXPosition=4f;
    public List<GameObject> playerList = new List<GameObject>();    //liste yapýyoruz ki öldükçe azaldýkça arttýkça vs... listede tutup sayýsý cartý curtu deðerlerine sahip olalým, onun üstünden algoritma yazýp senaryo tasarlayabiliriz de....
    bool isPlayersMoving;
    
    void Start()
    {
        isPlayersMoving = true;
          
     
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayersMoving==false)
        { return; } //eðer false ise burdan döndür (burayý return et) , yani aþaðýya hiç inme,çaðýrma...
        
        float touchX = 0;
        float newXValue = 0;
        if (Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Moved)
        {
            xSpeed = 250f;
            touchX=Input.GetTouch(0).deltaPosition.x/Screen.width; 
        }
        else if (Input.GetMouseButton(0))       //burasý bilgisayardan kontrol amaçlý yazýldý... mobil için gerekli deðil...
        {
            xSpeed = 100;
            touchX = Input.GetAxis("Mouse X");
        }
        newXValue=transform.position.x+xSpeed*touchX*Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -maxXPosition, maxXPosition);    //karakterimiz saðdan ve soldan dýþarý uçmasýn diye kelepçe (clamp)/kýsýtlama yapýyoruz
        Vector3 playerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = playerNewPosition;
    }

    public void SpawnPlayer(int gateValue,GateType gateType)
    {
        if (gateType==GateType.additionType)
        {
            for (int i = 0; i < gateValue; i++)
            {
                //instantiate bir nesneyi clonlamamýzý çoðaltmamýzý saðlýyor...
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPosition(), Quaternion.identity, transform);//burdaki transform yeni oluþacak playerlarý playerspawner (hierarchy'de) içinde oluþtursun diye koyuyoruz....
                playerList.Add(newPlayerGO);
            }
        }
        else if (gateType==GateType.multiplyType)
        {
            int newPlayerCount=(playerList.Count*gateValue)-playerList.Count;   //burda kaç tane create edeceðini, yani sayýyý veriyoruz...
            for (int i = 0; i < newPlayerCount; i++)    //burda o sayý adedince nesne/polis oluþtur diyoruz...
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPosition(), Quaternion.identity, transform);    //burda oluþturuyoruz...
                playerList.Add(newPlayerGO);            //burda listemize alýyoruz.
            }
        }      
    }



    public Vector3 GetPlayerPosition()
    {
        Vector3 position = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + position;
        return newPos;
    }
    private void OnTriggerEnter(Collider other) //other bilgisi içersinde diðer dokunan objenin bilgisi olacak (finish yani)
    {
        if (other.tag=="FinishLine")
        {
            Debug.Log("bittiiiiiiiiiiiiiiiiiiiiiiii");
            //StartAllCopsIdleAnim();
            isPlayersMoving= false;
           
        }
    }
    public void PlayerGotKilled(GameObject playerGO)
    {
        playerList.Remove(playerGO);
        Destroy(playerGO);
        CheckPlayersCount();
    }

    private void CheckPlayersCount()
    {
        if (playerList.Count<=0)
        {
            StopPlayer();
        }
    }

    public void ZombieDetected(GameObject target)
    {
        isPlayersMoving = false;
        LookAtZombies(target);
        //StartAllCopsShooting();
        AllZombiesKilled();
    }
    private void LookAtZombies(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir); //aradaki fark kadar döndür yönünü
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation= lookRotation;
    }
    private void LookAtForward()
    {
        transform.rotation = Quaternion.identity;
    }
    public void AllZombiesKilled()
    {
        LookAtForward();

        MovePlayer();   
    }
    public void MovePlayer()
    {
        //StartAllCopsRunAnim();
        isPlayersMoving = true;
    }
    public void StopPlayer()
    {
        isPlayersMoving= false; 
    }
    //public void StartAllCopsShooting()
    //{
    //    for (int i = 0; i <playerList.Count ; i++)
    //    {
    //        PlayerController cop = playerList[i].GetComponent<PlayerController>();
    //        cop.StartShooting();
    //    }
    //}
    //public void StartAllCopsRunAnim()   //burda çaðýrýyoruz...
    //{
    //    for (int i = 0; i < playerList.Count; i++)
    //    {
    //        PlayerController cop = playerList[i].GetComponent<PlayerController>();
    //        cop.StopShooting();
    //    }
    //}
    //public void StartAllCopsIdleAnim()
    //{
    //    for (int i = 0; i < playerList.Count; i++)
    //    {
    //        PlayerController cop = playerList[i].GetComponent<PlayerController>();
    //        cop.StartIdleAnim();
    //    }
    //}
}
