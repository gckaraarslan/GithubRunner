using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateType { multiplyType, additionType }
public class GateController : MonoBehaviour
{
    public int gateValue;
    public TMPro.TextMeshProUGUI gateText;
    public GateType gateType;
    private GameObject playerSpawnerGO;
    private PlayerSpawnerController playerSpawnerScript;
    bool hasGateUsed;
    private GateHolderController gateHolderScript;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerScript = playerSpawnerGO.GetComponent<PlayerSpawnerController>();
        gateHolderScript = transform.parent.gameObject.GetComponent<GateHolderController>();

        AddGateValueAndSymbol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hasGateUsed == false)
        {
            hasGateUsed = true;
            // karakteri çoğalt
            playerSpawnerScript.SpawnPlayer(gateValue, gateType);
            gateHolderScript.CloseGates();
            Destroy(gameObject);
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
