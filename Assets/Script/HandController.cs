using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandController : MonoBehaviour
{
    OVRInput.Controller RightCon; //右コントローラーの情報を取得
    OVRInput.Controller LeftCon;  //左コントローラーの情報を取得


    AudioSource ClapSound;

    public LayerMask layer;
    public float fRayDistance = 1;

    GameObject obj_col;
    Collision cs_col;
    bool bOnclap;
    bool bTiming;
    public float radHand;

    [SerializeField] GameObject HitParticle;

    void Start()
    {
        RightCon = OVRInput.Controller.RTouch;
        LeftCon = OVRInput.Controller.LTouch;

        ClapSound = GameObject.Find("ClapSound").GetComponent<AudioSource>();

        obj_col = GameObject.FindWithTag("Collision");
        cs_col = obj_col.GetComponent<Collision>();
        bOnclap = false;
        //Debug.Log("set_Controoooooooooooooooooooooooooool");
    }


    void Update()
    {
        //手の情報取得*********************************************************
        //手の移動量を取得
        Vector3 moveRight = OVRInput.GetLocalControllerVelocity(RightCon);
        Vector3 moveLeft = OVRInput.GetLocalControllerVelocity(LeftCon);
        //手の加速度を取得
        Vector3 accRight = OVRInput.GetLocalControllerAcceleration(RightCon);
        Vector3 accLeft = OVRInput.GetLocalControllerAcceleration(LeftCon);
        //手の現在位置を取得
        Vector3 posRight = OVRInput.GetLocalControllerPosition(RightCon);
        Vector3 posLeft = OVRInput.GetLocalControllerPosition(LeftCon);

        Vector3 Vec_AddY = new Vector3(0f, 1.0f, 0f);
        //Debug.DrawRay(posRight + Vec_AddY, transform.forward * 10, Color.red);


        //*********************************************************************

        //Debug.Log("右手位置" + posRight);
        //Debug.Log("左手位置" + posLeft);

        //腕の動きによる各処理****************************************************************
        RaycastHit hit; //光線で指定物のあたり判定を見る
        Ray LeftRay = new Ray(posLeft, transform.forward);
        Ray RightRay = new Ray(posRight, transform.forward);

        Debug.DrawRay(LeftRay.origin, posLeft + transform.forward * 10, Color.red);


        //右手**************************************************************************************
        if (Physics.Raycast(posRight /*+ Vec_AddY*/, transform.forward, out hit, fRayDistance, layer))
        {
            Debug.Log("右腕");
            //振り上げ 処理********************************************************************
            //右腕
            if (accRight.z >= 1.0 || accRight.y >= 1.0)
            {
                ClapSound.Play();

                //パーティクル
                Instantiate(HitParticle, this.transform);
                
                Destroy(hit.transform.gameObject);
                StartCoroutine(Vivration(0.2f, RightCon)); //(秒数, 振動させるコントローラー)
            }
            //**********************************************************************************



            //KeepHands 処理********************************************************************
            //あげた腕でノーツを保持する
            if (moveRight.y >= 0)
            {
                StartCoroutine(Vivration(0.2f, RightCon)); //(秒数, 振動させるコントローラー)
            }
            //**********************************************************************************
        }
        //*******************************************************************************************


        //左手**************************************************************************************
        if (Physics.Raycast(posLeft/* + Vec_AddY*/, transform.forward, out hit, fRayDistance, layer))
        {
            Debug.Log("左腕");
            //振り上げ 処理********************************************************************
            //左腕
            if (accLeft.z >= 1.0 || accLeft.y >= 1.0)
            {
                
                ClapSound.Play();

                //パーティクル
                Instantiate(HitParticle, this.transform);

                Destroy(hit.transform.gameObject);
                StartCoroutine(Vivration(0.2f, LeftCon)); //(秒数, 振動させるコントローラー)
            }
            //**********************************************************************************



            //KeepHands 処理********************************************************************
            //あげた腕でノーツを保持する
            if (moveRight.y >= 0)
            {
                StartCoroutine(Vivration(0.2f, RightCon)); //(秒数, 振動させるコントローラー)
            }
            //**********************************************************************************
        }
        //*******************************************************************************************



        //ClapHands 処理****************************************************************************
        //if (cs_col.CollisionCircle(posRight.x, posRight.y, posRight.z, radHand,
        //                           posLeft.x, posLeft.y, posLeft.z, radHand) && bOnclap == false)
        //{

        //    //Debug.Log("v当たってる");
        //    if (Physics.Raycast(posRight + Vec_AddY, transform.forward, out hit, fRayDistance, layer))
        //    {
        //        ClapSound.Play();
        //        //パーティクル
        //        Instantiate(HitParticle, this.transform);

        //        Destroy(hit.transform.gameObject);
        //        StartCoroutine(Vivration(0.2f, LeftCon)); //(秒数, 振動させるコントローラー)

                
        //    }
                
        //    bOnclap = true;
        //    bTiming = true;
        //}
        //else if (!cs_col.CollisionCircle(posRight.x, posRight.y, posRight.z, radHand,
        //                                posLeft.x, posLeft.y, posLeft.z, radHand) && bOnclap == true)
        //{
        //   // Debug.Log("v当たってない");
        //    bOnclap = false;
        //}
        //*******************************************************************************************
    }

    //振動を起こす処理*************************************************
    IEnumerator Vivration(float time, OVRInput.Controller controller)
    {
        //振動させる
        OVRInput.SetControllerVibration(1, 1, controller);
        //振動を止めるまで待機
        yield return new WaitForSeconds(time);
        //振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);
        //Debug.Log("振動");
    }
    //*****************************************************************
}
