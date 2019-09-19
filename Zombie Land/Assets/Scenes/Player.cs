using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
     
    Estimate Distance Between This(player) to Target(Enermy)
    return value = Distance
    */
    float EstimateDistance( Enermy  Target)
    {
       
        float result = 0.0f;

        Vector3 playerPos = this.transform.position;
        Vector3 targetPos = Target.transform.position;
        result = Mathf.Sqrt(
            Mathf.Abs(playerPos.x  -  targetPos.x)+     Mathf.Abs(playerPos.y - targetPos.y)+     Mathf.Abs(playerPos.z  -  targetPos.z)   );

        return result;
    }

}
