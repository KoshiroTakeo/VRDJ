using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepHands : MonoBehaviour
{
    private int Stopnotes;
    
    // Start is called before the first frame update
    void Start()
    {
        Stopnotes = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Stopnotes);
        if(Stopnotes >= 1)
        {
            transform.position += Time.deltaTime * transform.forward * -8;
        }

        if(Stopnotes < -1)
        {
            Stopnotes = -1;
        }
        else if (Stopnotes > 1)
        {
            Stopnotes = 1;
        }

        Stopnotes++;
    }

    

}
