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
    void LateUpdate()   //ayn� update gibi �al���r,her frame'de girer ama update'ten sonra olu�ur bu...
    {
       
        if (target !=null)
        {
            transform.position = target.position+offset;    //offset de�eri main camera'n�n POSITION de�erlerine e�it olmal� ya editorden veya burdan... 

        }
         
    }
}
