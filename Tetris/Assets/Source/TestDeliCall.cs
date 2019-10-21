using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeliCall : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        TestDeli.notify = TestDeli.LogK;
        TestDeli.notify();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


