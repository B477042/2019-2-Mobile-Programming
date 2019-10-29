using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeliCall : MonoBehaviour
{
    private int num = 100;
    // Start is called before the first frame update
    void Start()
    {
        TestDeli.notify = TestLog;
        TestDeli.notify();
        
        TestDeli.BroadPrn otherDeli;
        otherDeli = TestDeli.LogK;
        otherDeli();
        otherDeli = TestDeli.LogAwake;
        otherDeli();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TestLog()
    {
        print("monndonfp"+num);
    }
}


