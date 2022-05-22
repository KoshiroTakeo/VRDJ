using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NotesCheck : MonoBehaviour
{
    int NotesNomber;
    public GameObject[] TouchNotes;

    float MoveSpeed;
    CollidDown Down;
    bool HandsUp;
    AudioClip HandsUpSE;
    AudioSource audioSource;

    private float CountTime;

 


    private void Start()
    {
        NotesNomber = 0;
        MoveSpeed = 0.01f;

        //HandsUp = Down.HitReturn;
        //TouchNotes = new GameObject[6];
       
        Debug.Log(HandsUp);
        CountTime = 2;

    }

    private void Update()
    {
        Vector3 pos = this.gameObject.transform.position;
        
        //現在の位置から1移動する
        //this.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z - MoveSpeed);

        switch (NotesNomber)
        {
            case 0:
                //Debug.Log("エラー:");
                break;

                //X軸
            case 1:
                Debug.Log("右");
                break;
            case 2:
                Debug.Log("左");
                break;

                //Y軸
            case 3:
                Debug.Log("上");
                break;
            case 4:
                Debug.Log("下");
                break;

                //Z軸
            case 5:
                Debug.Log("前");
                break;
            case 6:
                Debug.Log("後");
                break;

        }
        //HandsUp = Down.GetHit();

        if (HandsUp == true)
        {

            
            Debug.Log("破壊");
            Destroy(this);
        }

        //CountTime = CountTime - Time.deltaTime;

        //if (CountTime <= 0)
        //{
        //    Destroy(this);
        //}

    }
    
}
