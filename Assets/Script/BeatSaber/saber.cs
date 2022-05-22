using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public LayerMask layer;      //検知するレイヤータグ名
    private Vector3 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1, layer)) 
        //(ワールド座標でのレイの開始地点,  レイの方向, レイが衝突を検知する最大距離, レイヤーマスクはレイキャストするときに選択的に衝突を無視するために使用します, トリガーに設定されているものも検索対象にするか)
        {
            if (Vector3.Angle(transform.position - previousPos, hit.transform.up) > 130)
            {
                Destroy(hit.transform.gameObject); //衝突したコライダーまたは Rigidbody の Transform
            }
        }
        previousPos = transform.position;
    }
}
