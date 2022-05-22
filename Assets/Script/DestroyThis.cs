using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    private float CountTime;
 
    // Start is called before the first frame update
    void Start()
    {
        CountTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;

        
        if(CountTime > 1.5f)
        {
            
            Destroy(this.gameObject);
        }
    }
}
