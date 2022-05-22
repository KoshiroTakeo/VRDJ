using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Collider : MonoBehaviour
{
    //##### colliderコンポーネント使用 #####################################


    private void OnTriggerEnter(Collider collider)
    {
        // 3D同士が接触した瞬間の１回のみ呼び出される処理
        Debug.Log("物が当たった : OnTriggerEnter");
        // 20210130 確認、使えるがチャタリングして一瞬が多発してる
    }

    private void OnTriggerStay(Collider collider)
    {
        // 3D同士が接触している間、常に呼び出される処理
        Debug.Log("触れている : OnTriggerStay");
        //20210130 多分使えてる
    }


    //======================================================================
}
