using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNotes : MonoBehaviour
{
    GameObject Notes;
    float sin;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Notes, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        sin = Mathf.Sin(Time.time);
        if (sin <= 0) sin = -(sin);
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("当たった");
        Debug.Log(sin);
    }
}
