using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Spawner에서 스폰된 오브젝트를 받아와서 조종한다
//일정 시간이 지나면 블럭은 아래로 내려간다
//싱글턴으로




public class Controller : MonoBehaviour
{

    //지금 조종하는 block. property처리
    public  GameObject controllingObject{get;set;}
    //조종하는 객체의 BlockMovement를 가리키는 것
    private BlockMovement movementComonent = null;
    

    //아래로 1칸씩 떨어지니까 속도는 1.0f를 기본값으로
    // private Vector3 droppingSpeed = Vector3.down;
    //밑으로 낙하하는 간격. 1.0f 시간 간격으로 낙하
     private float dropInterval = 1.0f;
     private float timer = 0.0f;


    

    
    private static Controller instance = null;
    public static Controller  Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        //조종하는 블럭 초기화
        controllingObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ReleaseControl함수를 Block On Contact이 실행될 때 실행하게 둡니다.
        EventManger.Instance.AddEvent(EventType.BLOCK_ON_CONTACT, ReleaseControl);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (controllingObject == null) return;
        timer += Time.deltaTime;
       // TestPrint();
        InputProcess();

        //timer가 아직 interval이 되지 않았을 때 return
        if (timer < dropInterval) return;
        timer = 0.0f;
        
        
        //낙하
        movementComonent.MoveDown();

      
    }
    //spawner에서 만들어진 블럭을 받아온다
    public void TakeControl(GameObject gameObject)
    {
        if (gameObject == null) return;
        controllingObject = gameObject;
        movementComonent=controllingObject.GetComponent<BlockMovement>();
    }
    //BlockConstructor에서 이 블럭이 바닥이나 다른 블럭에 닿았으니 컨트롤하지 못하게 제어권을 놓게 만든다
    //입력 인자로 받은 bool은 BlockConstructor에서 판단한 결과 값을 받아와서
    //닿았다=true면 작동하고, 안 닿았다 = false면 제어하게 둔다
   
    private void ReleaseControl()
    {
        if (!controllingObject) return;        
        controllingObject = null;
    }
     void SpeedUp()
    {
       // droppingSpeed += new Vector3(0.0f,1.0f,0.0f);
    }

  

    //prefab의 자식객체에 접근 되는지 test
    private void TestPrint()
    {
        Debug.Log("가진 자식의 수"+controllingObject.transform.childCount);
        for (int i = 0; i < controllingObject.transform.childCount; i++)
        {
            Debug.Log("자식들의 위치 "+(i+1)+" : "+controllingObject.transform.GetChild(i).transform.position );
            Debug.Log("자식들의 이름 " + (i + 1) + " : "+ controllingObject.transform.GetChild(i).gameObject.name);
        }

    }
   

        //입력을 처리한다
        //좌우 이동, 회전
        private void InputProcess()
    {
        if (!controllingObject) return;
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movementComonent.MoveLeft();
                return;
            }

             if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                movementComonent.MoveRight();
                return;
            }
             if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                movementComonent.Rotate();
                return;
            }
        }
        
    }
    


}
