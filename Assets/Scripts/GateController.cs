using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateType { multiplyType,additionType}   //�arpma(katlama) tipi, ekleme tipi
public class GateController : MonoBehaviour
{
    public int gateValue;
    public TMPro.TextMeshProUGUI gateText;
    public GateType gateType;

    private GameObject playerSpawnerGO;
    private PlayerSpawnerController playerSpawnerScript;
    bool hasGateUsed;
    private GateHolderController gateHolderScript;  //a�a��daki tag'li �a��r�p getcomponen<scriptname> yapt���m�z�n k�sa yolu bir nevi
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerScript = playerSpawnerGO.GetComponent<PlayerSpawnerController>();  //script bir component... PlayerSpawner TAG'li objeye attach edilen script
        gateHolderScript = transform.parent.gameObject.GetComponent<GateHolderController>(); //o instance'� (gateHolderScript) bunu i�ine att���m�z objemiz i�indeki gateholdercontroller scripti (objesi) ni �a��r�yoruz...
       
        AddGateValueAndSymbol();
    }

  
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && hasGateUsed==false)
        {
            hasGateUsed = true;
            playerSpawnerScript.SpawnPlayer(gateValue,gateType);
            gateHolderScript.CloseGate();   // o script/obje i�indeki closegate metodunu �a��r�yoruz �arp��ma esnas�nda...
            Destroy(gameObject);    //gatecontroller'�n game objesi kap�n�n kendisi... de�ilen obje yok edilecek

            
        }
    }
    private void AddGateValueAndSymbol()
    {
        switch (gateType)
        {
            case GateType.multiplyType:
                gateText.text = "X" + gateValue.ToString();
                break;
            case GateType.additionType:
                gateText.text = "+" + gateValue.ToString();
                break;
            default:
                break;
        }
    }
}
