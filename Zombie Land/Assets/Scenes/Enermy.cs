using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    static int NumsOfEnermy;

    // Start is called before the first frame update
    void Start()
    {
        NumsOfEnermy++;
        this.transform.position = SprinkleOnMap();
        ChangeColorToNormal();
        //this.
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Set Object's Location by Random X-Z value
    Vector3 SprinkleOnMap()
    {
        float NewPosX = Random.Range(-5.0f, 5.0f);
        float NewPosZ = Random.Range(-5.0f, 5.0f);
        return new Vector3(NewPosX, 0.0f, NewPosZ);
    }

    static int GetNumsOfEnermy() { return NumsOfEnermy; }

    void ChangeColorToClose()
    {
        this.GetComponent<Material>().color = Color.red;
    }

     void ChangeColorToNormal()
    {
        this.GetComponent<Material>().color = Color.black;
    }

}

