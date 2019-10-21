using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeliCall : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        TestDeli.notify = TestLog;
        TestDeli.notify();
        BroadPrn otherDeli;
        otherDeli = TestDeli.LogK;
        otherDeli();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TestLog()
    {
        print("monndonfp");
    }
}


