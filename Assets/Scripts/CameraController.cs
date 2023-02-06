using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;  //distance between camera and character 
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()   //ayný update gibi çalýþýr,her frame'de girer ama update'ten sonra oluþur bu...
    {
       
        if (target !=null)
        {
            transform.position = target.position+offset;    //offset deðeri main camera'nýn POSITION deðerlerine eþit olmalý ya editorden veya burdan... 

        }
         
    }
}
