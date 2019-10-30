using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Assets / Scenes / Cube.prefab

    

   // public GameObject block = null;
    //블럭이 내려올 위치를 저장하는 property
   private Vector3 DropPoint { get { return new Vector3(0.0f, 11.0f, 0.0f); } }
    private bool power = true;

    private static Spawner instance = null;
    public static Spawner Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

    }

   
    
    // Start is called before the first frame update
    void Start()
    {
        EventManger.Instance.AddEvent(EventType.GAME_OVER, turnOffSpawner);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Controller.Instance.controllingObject==null) 
        {

            //ProductBlock();
            Controller.Instance.TakeControl(SpawnNewBlocks(BlocksEnum.Cube));
        }



    }

     private GameObject SpawnNewBlocks(BlocksEnum BlockName)
    {
        //if Block Name is UnVaild, return Null
        GameObject newBlocks= Instantiate(Resources.Load(BlockDic.Dic[BlockName], typeof(GameObject)) as GameObject);
        newBlocks.transform.position =DropPoint;
        EventManger.Instance.NotifyEvent(EventType.BLOCK_SPAWNED);
        return newBlocks;
        
    }
    //블럭을 랜덤하게 만들어주는 식
    //이렇게 만든 이유 : Random이 int형을 취급 안 해서
    private void ProductBlock()
    {
        if (!power) return;
        int rBlock = (int)Random.Range((float)BlocksEnum.Cube, (float)BlocksEnum.Yo + 1);
        switch(rBlock)
        {
            case 0:
                Controller.Instance.TakeControl( SpawnNewBlocks(BlocksEnum.Cube));
                break;
            case 1:
                Controller.Instance.TakeControl( SpawnNewBlocks(BlocksEnum.LeftStair));
                break;
            case 2:
                Controller.Instance.TakeControl(SpawnNewBlocks(BlocksEnum.LStick));
                break;
            case 3:
                Controller.Instance.TakeControl(SpawnNewBlocks(BlocksEnum.ReverseLStick));
                break;
            case 4:
                Controller.Instance.TakeControl(SpawnNewBlocks(BlocksEnum.RightStair));
                break;
            case 5:
                Controller.Instance.TakeControl(SpawnNewBlocks(BlocksEnum.Stick));
                break;
            case 6:
                Controller.Instance.TakeControl(SpawnNewBlocks(BlocksEnum.Yo));
                break;
        }

    }
    private void turnOffSpawner()
    {
        power = false;
    }
 

}
