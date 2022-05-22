using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownNotes : MonoBehaviour
{
    public GameObject Notes;
    private float CountTime;

    // Start is called before the first frame update
    void Start()
    {
        CountTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime = CountTime - Time.deltaTime;

        if(CountTime <= 0)
        {
            Instantiate(Notes, this.transform.position, Quaternion.identity);
            CountTime = 2;
        }
        
    }
}
