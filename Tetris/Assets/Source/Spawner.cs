using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Assets / Scenes / Cube.prefab
  
    public GameObject block=null ;
    private void Awake()
    {
        //cube
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        //cube.GetComponent<Renderer>().material.color = new Color(255.0f, 0.0f, 0.0f);
        block = Instantiate( Resources.Load("Cube", typeof(GameObject)) as GameObject);
        block.transform.position =new  Vector3(0.0f, 20.0f, 0.0f);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        block.transform.position += /*block.transform.position*/  Vector3 .down*0.1f;
    }

    public GameObject SpawnNewBlocks(string BlockName)
    {
        //if Block Name is UnVaild, return Null
        GameObject newBlocks= Instantiate(Resources.Load(BlockName, typeof(GameObject)) as GameObject);
        return newBlocks;
            
    }
}
