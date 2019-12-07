using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject other = GameObject.Find("Cube");
        float distance= Vector3.Distance(gameObject.transform.position,other.transform.position);
        Debug.Log("distance : " + distance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
