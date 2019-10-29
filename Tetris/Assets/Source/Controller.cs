using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Spawner에서 스폰된 오브젝트를 받아와서 조종한다
//일정 시간이 지나면 블럭은 아래로 내려간다
//싱글턴으로




public class Controller : MonoBehaviour
{

    //지금 조종하는 block. property처리
    public  GameObject controllingObject{ get; set; }
    //조종하는 객체의 BlockMovement를 가리키는 것
    private BlockMovement movementComonent = null;
    

   
    //밑으로 낙하하는 간격. 1.0f 시간 간격으로 낙하
     private float dropInterval = 1.0f;
     private float timer = 0.0f;

    //그 방향으로 움직일 수 있게 허가해주는 dic
    private enum dirrection
    {
       LEFT=0,RIGHT
    }
    private Dictionary<dirrection, bool> isAllowedTo=new Dictionary<dirrection, bool>();
    

    
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
        
        //4방위 dic을 초기화
        initDic();
        EventManger.Instance.AddEvent(EventType.BLOCK_CONTACT_LEFT, notAllowToLeft);
        EventManger.Instance.AddEvent(EventType.BLOCK_CONTACT_RIGHT, notAllpwToRight);
        EventManger.Instance.AddEvent(EventType.BLOCK_NOT_CONTACT_LEFT, allowToLeft);
        EventManger.Instance.AddEvent(EventType.BLOCK_NOT_CONTACT_RIGHT, allowToRight);
        EventManger.Instance.AddEvent(EventType.BLOCK_CONSTRUCT_FINISH, releaseControl);
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (controllingObject == null) return;
        timer += Time.deltaTime;
       // TestPrint();
        InputProcess();
        TestPrint();
        //timer가 아직 interval이 되지 않았을 때 return
        if (timer < dropInterval) return;
        timer = 0.0f;
        
        
        //낙하
        movementComonent.MoveDown();

      
    }

    private void initDic()
    {
        
        isAllowedTo.Add(dirrection.LEFT, true);
        isAllowedTo.Add(dirrection.RIGHT, true);
    }

    //spawner에서 만들어진 블럭을 받아온다
    public void TakeControl(GameObject gameObject)
    {
        if (gameObject == null) return;
        controllingObject = gameObject;
        movementComonent=controllingObject.GetComponent<BlockMovement>();
    }
    //BlockConstructor에서 이 블럭이 바닥이나 다른 블럭에 닿았으니 컨트롤하지 못하게 제어권을 놓게 만든다
    //놓기 전에 모든 컴포넌트에 rigidbody를 다 붙여준다
   
    private void releaseControl()
    {
        if (!controllingObject) return;
        

        controllingObject = null;
    }

    private void downFaster()
    {
        timer += 0.05f;
    }
    //블럭을 아래로 내리꽂기
    public void StraightDown()
    {

    }
    private void notAllowToLeft()
    {
        isAllowedTo[dirrection.LEFT] = false;
    }
    private void notAllpwToRight()
    {
        isAllowedTo[dirrection.RIGHT] = false;
    }
    private void allowToLeft()
    {
        isAllowedTo[dirrection.LEFT] = true;
    }
    private void allowToRight()
    {
        isAllowedTo[dirrection.RIGHT] = true;
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
        var pointer = controllingObject.gameObject;
        pointer.transform.position += new Vector3(0.0f,0.0f,10.0f);

    }
   

    //입력을 처리한다
    //좌우 이동, 회전
    private void InputProcess()
    {
        if (!controllingObject) return;
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)&&isAllowedTo[dirrection.LEFT]==true)
            {
                
                movementComonent.MoveLeft();
                return;
            }

             if (Input.GetKeyDown(KeyCode.RightArrow) && isAllowedTo[dirrection.RIGHT] == true)
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

        if (Input.GetKey(KeyCode.DownArrow))
        {
            downFaster();
            return;
        }

    }
    


}
