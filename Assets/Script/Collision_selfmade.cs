using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    
    ////あたり判定の計算
    //public bool CollisionCircle(Vector3 fA, float fAr,
    //                            Vector3 fB, float fBr)

    //{
    //    float fDx = fA.x - fB.x;
    //    float fDy = fA.y - fB.y;
    //    float fDz = fA.y - fB.z;
    //    float fSr = fAr  + fBr;
        
    //    return (fDx * fDx) + (fDy * fDy) + (fDz * fDz) <= fSr * fSr;
    //}

    //あたり判定の計算
    public bool CollisionCircle(float fAx, float fAy, float fAz, float fAr,
                         float fBx, float fBy, float fBz, float fBr)

    {
        float fDx = fAx - fBx;
        float fDy = fAy - fBy;
        float fDz = fAz - fBz;
        float fSr = fAr + fBr;
        return (fDx * fDx) + (fDy * fDy) + (fDz * fDz) <= fSr * fSr;
    }
}
