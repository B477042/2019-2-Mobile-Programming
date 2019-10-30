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
    private enum command
    {
       LEFT=0,RIGHT,ROTATE
    }
    private Dictionary<command, bool> isAllowedTo=new Dictionary<command, bool>();
    

    
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
        //  EventManger.Instance.AddEvent(EventType.BLOCK_CONSTRUCT_FINISH, releaseControl);
        //EventManger.Instance.AddEvent(EventType.BLOCK_CONTACT_LEFT_WALL, notAllpwRotate);
        //EventManger.Instance.AddEvent(EventType.BLOCK_CONTACT_RIGHT_WALL, notAllpwRotate);

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
        
        TestPrint();
        //낙하
        movementComonent.MoveDown();

      
    }

    private void initDic()
    {
        
        isAllowedTo.Add(command.LEFT, true);
        isAllowedTo.Add(command.RIGHT, true);
        isAllowedTo.Add(command.ROTATE, true);
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
        isAllowedTo[command.LEFT] = false;
    }
    private void notAllpwToRight()
    {
        isAllowedTo[command.RIGHT] = false;
    }
    private void notAllpwRotate()
    {
        isAllowedTo[command.ROTATE] = false;
    }
    private void allowToLeft()
    {
        isAllowedTo[command.LEFT] = true;
    }
    private void allowToRight()
    {
        isAllowedTo[command.RIGHT] = true;
    }
    private void allowToRotate()
    {
        isAllowedTo[command.ROTATE] = true;
    }



    //prefab의 자식객체에 접근 되는지 test
    //문법 확인용으로 만든 함수. 나중에 삭제
    private void TestPrint()
    {
        Debug.Log("가진 자식의 수"+controllingObject.transform.childCount);
        for (int i = 0; i < controllingObject.transform.childCount; i++)
        {
            Debug.Log("자식들의 위치 "+(i+1)+" : "+controllingObject.transform.GetChild(i).transform.position );
            Debug.Log("자식들의 이름 " + (i + 1) + " : "+ controllingObject.transform.GetChild(i).gameObject.name);
        }
        if (controllingObject.transform.GetChild(1) != null)
            Destroy(controllingObject.transform.GetChild(1).gameObject);
        int a = 30, b = 40;
        List<int> origin = new List<int>();
       
        origin.Add(a);
        origin.Add(b);
        print(origin[0] + "<- origin a value, " + origin[1] + "<- b value");
        List<int> copy = new List<int>(origin);
        print(copy[0] + "<- Copy a value, " + copy[1] + "<- b value");
        copy[0] = 60;
        print(origin[0] + "<- origin a value, " + origin[1] + "<- b value");
        print(copy[0] + "<- Copy a value, " + copy[1] + "<- b value");
        print(a + "a<- value");
        a = 100;
        print(origin[0] + "<- origin a value, " + origin[1] + "<- b value");
        print(copy[0] + "<- Copy a value, " + copy[1] + "<- b value");
        print(a + "<- a value");

        origin = copy;
        print(origin[0] + "<- origin a value, " + origin[1] + "<- b value");
        print(copy[0] + "<- Copy a value, " + copy[1] + "<- b value");
        print(a + "<- a value");

        List<int> other = new List<int>();
        other.Add(900);
        other.Add(1000);

        copy[0] = 700;
        copy = other;
        print(origin[0] + "<- origin a value, " + origin[1] + "<- b value");
        print(copy[0] + "<- Copy a value, " + copy[1] + "<- b value");
        print(other[0] + "<- other a value, " + other[1] + "<- b value");
        print(a + "<- a value");
        other.Clear();
        print(origin[0] + "<- origin a value, " + origin[1] + "<- b value");
        print(copy[0] + "<- Copy a value, " + copy[1] + "<- b value");
        print(other[0] + "<- other a value, " + other[1] + "<- b value");
        print(a + "<- a value");
    }


    //입력을 처리한다
    //좌우 이동, 회전
    private void InputProcess()
    {
        if (!controllingObject) return;
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)&&isAllowedTo[command.LEFT]==true)
            {
                
                movementComonent.MoveLeft();
                return;
            }

             if (Input.GetKeyDown(KeyCode.RightArrow) && isAllowedTo[command.RIGHT] == true)
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
