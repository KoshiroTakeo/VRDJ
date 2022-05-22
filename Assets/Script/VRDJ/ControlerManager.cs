//################################################################
//
//オキュラスコントローラーの値をとる
//
//================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControlerManager : MonoBehaviour
{

    //###### コントローラーの情報取得 ##############################

    OVRInput.Controller RightCon; 
    OVRInput.Controller LeftCon;

    //==============================================================



    //###### 他スクリプトの使用と準備 ##############################

    Collision_selfmade cs_col;
    NotesDestroy cs_noteDestroy;

    //==============================================================



    //###### オブジェクトサイズ取得 ############################

    // コントローラー
    [SerializeField] GameObject RightController;
    [SerializeField] GameObject LeftController;



    ////コントローラー座標確認用
    //[SerializeField] GameObject _movecube;
    //[SerializeField] GameObject _movecube2;

    //==========================================================



    //###### その他デバック用変数 ##############################
    public float _RaySize;

    //==========================================================



    void Start()
    {
        //###### コントローラーの情報獲得 ########################

        RightCon = OVRInput.Controller.RTouch;
        LeftCon  = OVRInput.Controller.LTouch;

        //========================================================



        //##### Collisionの充て込み ##############################
        cs_col = new Collision_selfmade();
        cs_noteDestroy = new NotesDestroy();

        //========================================================



        //##### その他実行 #######################################
        GameObject g = GameObject.FindWithTag("Red");
        //GameObject[] g = GameObject.FindGameObjectsWithTag("Red");

        Debug.Log("Name : " + g.name);
        //Debug.Log("Name:" + g[0].name);
        //Debug.Log("Name:" + g[1].name);
        //Debug.Log("Name:" + g[2].name);

        //========================================================
    }



    void Update()
    {
        
        //###### コントローラーの情報取得 ####################################

        //手の強さ?を取得
        Vector3 moveRight = OVRInput.GetLocalControllerVelocity(RightCon);
        Vector3 moveLeft  = OVRInput.GetLocalControllerVelocity(LeftCon);
        //Debug.Log("移動量 R : " + moveRight);
        //Debug.Log("移動量 L : " + moveLeft );

        //手の加速度を取得
        Vector3 accRight = OVRInput.GetLocalControllerAcceleration(RightCon);
        Vector3 accLeft  = OVRInput.GetLocalControllerAcceleration(LeftCon);
        //Debug.Log("加速量 R : " + accRight);
        //Debug.Log("加速量 L : " + accLeft);

        //手の現在位置を取得
        Vector3 posRight = OVRInput.GetLocalControllerPosition(RightCon);
        Vector3 posLeft  = OVRInput.GetLocalControllerPosition(LeftCon);
        //Debug.Log("座標   R : " + posRight);
        //Debug.Log("座標   L : " + posLeft);

        //====================================================================





        //##### あたり判定 ###################################################

        //***** 光線で指定物のあたり判定を見る ********************************************************************************
        RaycastHit hitObject;

        if (Physics.Raycast(RightController.transform.position, RightController.transform.forward, out hitObject, _RaySize) ||
            Physics.Raycast(LeftController.transform.position, LeftController.transform.forward, out hitObject, _RaySize))
        {
            

            //***** 触れたノーツから振る舞いを変える ******************************
            switch (hitObject.collider.gameObject.tag)
            {
                case "Red": // タグ名
                    Debug.Log(hitObject.collider.gameObject.tag);


                    //このタグがついた物を叩いたとき、どっちの手で叩いたか、叩いた時間、叩いた強さ(acc:加速度)、
                    cs_noteDestroy.Destroy_This(true);

                   

                    break;
            }

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    

        //コントローラー同士
        if (cs_col.CollisionCircle(posRight, _RaySize,
                                  posLeft, _RaySize))
        {
            //Debug.Log("両腕が当たった");

        }
        
        //====================================================================





        //###### その他デバック試み ##########################################

        ////座標位置の視覚化****************************************************
        //Vector3 pos  = _movecube.gameObject.transform.position;
        //Vector3 pos2 = _movecube2.gameObject.transform.position;

        //_movecube.gameObject.transform.position  = new Vector3(pos.x = posRight.x, pos.y = posRight.y, pos.z = posRight.z);
        //_movecube2.gameObject.transform.position = new Vector3(pos2.x = posLeft.x, pos2.y = posLeft.y, pos2.z = posLeft.z);

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //デバックボタン
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("座標   R : " + posRight);
        }

        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            Debug.Log("座標   L : " + posLeft);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //====================================================================



       



        ////##### 各ボタンの確認 ###############################################
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

        ////======================================================================
    }



   
}
