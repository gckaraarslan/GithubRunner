using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateType { multiplyType,additionType}   //çarpma(katlama) tipi, ekleme tipi
public class GateController : MonoBehaviour
{
    public int gateValue;
    public TMPro.TextMeshProUGUI gateText;
    public GateType gateType;

    private GameObject playerSpawnerGO;
    private PlayerSpawnerController playerSpawnerScript;
    bool hasGateUsed;
    private GateHolderController gateHolderScript;  //aþaðýdaki tag'li çaðýrýp getcomponen<scriptname> yaptýðýmýzýn kýsa yolu bir nevi
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerScript = playerSpawnerGO.GetComponent<PlayerSpawnerController>();  //script bir component... PlayerSpawner TAG'li objeye attach edilen script
        gateHolderScript = transform.parent.gameObject.GetComponent<GateHolderController>(); //o instance'ý (gateHolderScript) bunu içine attýðýmýz objemiz içindeki gateholdercontroller scripti (objesi) ni çaðýrýyoruz...
       
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
            gateHolderScript.CloseGate();   // o script/obje içindeki closegate metodunu çaðýrýyoruz çarpýþma esnasýnda...
            Destroy(gameObject);    //gatecontroller'ýn game objesi kapýnýn kendisi... deðilen obje yok edilecek

            
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
