using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_selfmade : MonoBehaviour
{

    //あたり判定の計算
    public bool CollisionCircle(Vector3 fA, float fAr,
                                Vector3 fB, float fBr)

    {
        float fDx = fA.x - fB.x; // X
        float fDy = fA.y - fB.y; // Y
        float fDz = fA.z - fB.z; // Z
        float fSr = fAr + fBr;   // 半径(radius)

        return (fDx * fDx) + (fDy * fDy) + (fDz * fDz) <= fSr * fSr;
    }

}
