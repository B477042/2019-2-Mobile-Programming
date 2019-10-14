using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Assets / Scenes / Cube.prefab

    public GameObject block = null;
    //블럭이 내려올 위치를 저장하는 property
    private Vector3 DropPoint { get { return new Vector3(0.0f, 10.0f, 0.0f); } }

    private Vector3 droppingSpeed = Vector3.down * 0.05f;


    private void Awake()
    {
        //cube
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        //cube.GetComponent<Renderer>().material.color = new Color(255.0f, 0.0f, 0.0f);
        //Instanatiate 연습
        //block = Instantiate( Resources.Load("Cube", typeof(GameObject)) as GameObject);
        //block.transform.position =DropPoint;

        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        block = SpawnNewBlocks(BlocksEnum.LStick);

    }

    // Update is called once per frame
    void Update()
    {
        if(block!=null)
        if(block.transform.position.y>0)
        block.transform.position += /*block.transform.position*/  droppingSpeed;
        
    }

    public GameObject SpawnNewBlocks(BlocksEnum BlockName)
    {
        //if Block Name is UnVaild, return Null
        GameObject newBlocks= Instantiate(Resources.Load(BlockDic.Dic[BlockName], typeof(GameObject)) as GameObject);
        newBlocks.transform.position =DropPoint;
        return newBlocks;
        
            
    }
    

}
