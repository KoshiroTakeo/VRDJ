using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidDown : MonoBehaviour
{
    int ColiidNum;
    private bool Hit = false;
    public AudioClip HandsUpSE;
    AudioSource audioSource;

    //public bool HitReturn
    //{
    //    get { return this.Hit; }  //取得用
    //    private set { this.Hit = false; }　//値入力用
    //}
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ColiidNum = 6;
        Hit = false;
        

    }
    private void OnTriggerEnter(Collider collider)
    {
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        audioSource.PlayOneShot(HandsUpSE);
        Debug.Log("触った");
        Hit = true;
        Destroy(this.gameObject);
       
    }
    private void OnTriggerExit(Collider collider)
    {

        Debug.Log("離した");
    }

    public bool GetHit()
    {
        return Hit;
    }

   

}
