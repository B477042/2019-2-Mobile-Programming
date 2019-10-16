using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Spawner에서 스폰된 오브젝트를 받아와서 조종한다
//일정 시간이 지나면 블럭은 아래로 내려간다
//싱글턴으로

public class Controller : MonoBehaviour
{

    //지금 조종하는 block
    static private GameObject controllingObject = null;
    //조종하는 객체의 BlockMovement를 가리키는 것
    static private BlockMovement movementComonent = null;
    

    //아래로 1칸씩 떨어지니까 속도는 1.0f를 기본값으로
    static private Vector3 droppingSpeed = Vector3.down;
    //밑으로 낙하하는 간격. 1.0f 시간 간격으로 낙하
    static private float dropInterval = 1.0f;
    static private float timer = 0.0f;
    //singleTone code
    private static GameManger instance = null;
    public static GameManger Instance
    {
        get
        {
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //timer가 아직 interval이 되지 않았을 때 return
        if (timer < dropInterval) return;
        timer = 0.0f;

        if(movementComonent==null)
        {
            movementComonent = controllingObject.GetComponent<BlockMovement>();
        }
        //낙하
        movementComonent.MoveDown();

      
    }
    //spawner에서 만들어진 블럭을 받아온다
    static public void TakeControl(GameObject gameObject)
    {
        if (gameObject == null) return;
        controllingObject = gameObject;
    }

    static public void SpeedUp()
    {
        droppingSpeed += new Vector3(0.0f,1.0f,0.0f);
    }

    //지금 controller가 block을 컨트롤 하는 중(null이 아니라면)이라면 true
    static public bool IsControlling()
    {
        if (controllingObject == null) return false;
        else return true;
    }
}
