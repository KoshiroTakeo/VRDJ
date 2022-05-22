using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gloves : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previousPos;
    private bool ClapSE;
    public AudioSource ClapSound;

    //殴った時のパーティクル
    [SerializeField] GameObject HitParticle;
    float particleDelayTime = 0.2f;

    OVRInput.Controller RightCon; //右コントローラーの情報を取得
    OVRInput.Controller LeftCon;  //左コントローラーの情報を取得

    //クラップ処理
    [SerializeField] GameObject RightController;
    [SerializeField] GameObject LeftController;
    private bool Onclap;

    //collisionオブジェクト参照
    GameObject obj_col;
    Collision cs_col;

    void Start()
    {
        RightCon = OVRInput.Controller.RTouch;
        LeftCon = OVRInput.Controller.LTouch;

        ClapSound = GameObject.Find("ClapSound").GetComponent<AudioSource>();

        Onclap = false;

        LayerMask.NameToLayer("handsup");

        obj_col = GameObject.FindWithTag("Collision");
        cs_col = obj_col.GetComponent<Collision>();

    }


    void Update()
    {
        //手の??を取得
        Vector3 moveRight = OVRInput.GetLocalControllerVelocity(RightCon);
        Vector3 moveLeft = OVRInput.GetLocalControllerVelocity(LeftCon);
        //Debug.Log("右手の移動量" + moveRight);
        //Debug.Log("左手の移動量" + moveLeft );

        //手の加速度を取得
        Vector3 accRight = OVRInput.GetLocalControllerAcceleration(RightCon);
        Vector3 accLeft = OVRInput.GetLocalControllerAcceleration(LeftCon);
        //Debug.Log(accRight);

        //右手の現在位置を取得
        Vector3 posRight = OVRInput.GetLocalControllerPosition(RightCon);
        Vector3 posLeft = OVRInput.GetLocalControllerPosition(LeftCon);
        //Debug.Log(posRight);


        //Debug.Log(this.transform.position.x);

        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);




        //光線で指定物のあたり判定を見る
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, layer))
        {

            //HandsUp 処理********************************************************************
            //右腕を振り上げるとオブジェクトを消せる
            
                if (accRight.z >= 1.0 || accRight.y >= 1.0)
                {
                    ClapSound.Play();

                    //パーティクル
                    //Instantiate(HitParticle, this.transform);


                    Destroy(hit.transform.gameObject);
                    StartCoroutine(Vivration(0.2f, RightCon)); //(秒数, 振動させるコントローラー)
                }
            


               //左腕を振り上げるとオブジェクトを消せる
                if (accLeft.z >= 1.0 || accLeft.y >= 1.0)
                {
                    ClapSound.Play();

                    //パーティクル
                    //Instantiate(HitParticle, this.transform);

                    Destroy(hit.transform.gameObject);
                    StartCoroutine(Vivration(0.2f, LeftCon)); //(秒数, 振動させるコントローラー)
                }
            

            //**********************************************************************************



            //KeepHands 処理********************************************************************
            //あげた腕でノーツを保持する
            if (moveRight.y >= 0)
            {
                //Destroy(hit.transform.gameObject);
                StartCoroutine(Vivration(0.2f, RightCon)); //(秒数, 振動させるコントローラー)

            }
            //**********************************************************************************




        }



        //ClapHands 処理********************************************************************
        //if (cs_col.CollisionCircle(posRight.x, posRight.y, posRight.z, RightController.transform.localScale.x / 10,
        //                           posLeft.x, posLeft.y, posLeft.z, LeftController.transform.localScale.x / 10) && Onclap == false)
        //{
        //    ClapSound.Play();

        //    //パーティクル
        //    //Instantiate(HitParticle, this.transform);

        //    //Destroy(hit.transform.gameObject);
        //    StartCoroutine(Vivration(0.2f, LeftCon)); //(秒数, 振動させるコントローラー)

        //    // Debug.Log("当たってる");
        //    Onclap = true;
        //}
        //else if (cs_col.CollisionCircle(posRight.x, posRight.y, posRight.z, RightController.transform.localScale.x / 10,
        //                           posLeft.x, posLeft.y, posLeft.z, LeftController.transform.localScale.x / 10) && Onclap == true)
        //{
        //    // Debug.Log("当たってない");
        //    Onclap = false;
        //}
        //**********************************************************************************

        ////各ボタンの確認
        //if (OVRInput.GetDown(OVRInput.RawButton.A))
        //{
        //    Debug.Log("Aボタンを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.B))
        //{
        //    Debug.Log("Bボタンを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.X))
        //{
        //    Debug.Log("Xボタンを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.Y))
        //{
        //    Debug.Log("Yボタンを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.Start))
        //{
        //    Debug.Log("メニューボタン（左アナログスティックの下にある）を押した");
        //}

        //if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        //{
        //    Debug.Log("右人差し指トリガーを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        //{
        //    Debug.Log("右中指トリガーを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        //{
        //    Debug.Log("左人差し指トリガーを押した");
        //}
        //if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        //{
        //    Debug.Log("左中指トリガーを押した");
        //}

    }



    IEnumerator Vivration(float time, OVRInput.Controller controller)
    {

        //振動させる
        OVRInput.SetControllerVibration(1, 1, controller);

        //振動を止めるまで待機
        yield return new WaitForSeconds(time);

        //振動を止める
        OVRInput.SetControllerVibration(0, 0, controller);

    }


    //あたり判定の計算
    //bool CollisionCircle(float fAx, float fAy, float fAz, float fAr,
    //                     float fBx, float fBy, float fBz, float fBr)

    //{
    //    float fDx = fAx - fBx;
    //    float fDy = fAy - fBy;
    //    float fDz = fAz - fBz;
    //    float fSr = fAr + fBr;
    //    return (fDx * fDx) + (fDy * fDy) + (fDz * fDz) <= fSr * fSr;
    //}
}


