using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidCheck : MonoBehaviour
{
    int ColiidNum;
    private void Start()
    {
        ColiidNum = 6;
    }
    private void OnTriggerEnter(Collider collider)
    {
        
        Debug.Log("触った");
    }
    private void OnTriggerExit(Collider collider)
    {
        
        Debug.Log("離した");
    }
}
