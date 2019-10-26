using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//블럭이 할 수 있는 모든 움직임만 표현
//낙하, 회전, 가속 낙하, 내리 꽂기
public class BlockMovement : MonoBehaviour
{
    //public delegate void InputAction();
    //public static InputAction inputAction;
    private float speed = 1.0f;
    private float accelatedSpeed = 0.0f;
    private Vector3 lastPos = new Vector3();
    private Quaternion lastRot = new Quaternion();
   
    // Start is called before the first frame update
    void Start()
    {
        EventManger.Instance.AddEvent(EventType.BLOCK_ON_CONTACT, NotifyTrigger);
        //prefab으로 불러온 도형의 충돌 범위를 0.95f로 제한해둔다
        //gameObject.GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
        //for (int i = 0; i < gameObject.transform.childCount;i++)
        //{
        //    gameObject.transform.GetChild(i).GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
        //}
    }
    //public static Dictionary<KeyCode,InputAction>Dic=new Dictionary<KeyCode, InputAction>()
    //{
    //    { KeyCode.A,inputAction. MoveLeft},

    //}
   

    // Update is called once per frame
    void Update()
    {
        
        
    }
    //변경되기 직전의 transform정보를 저장해둔다.
    private void saveLastTransform()
    {
        lastPos = transform.position;
        lastRot = transform.rotation;
    }
    
    public void MoveDown()
    {
        saveLastTransform();
        transform.position+= Vector3.down*speed;
    }
    public void MoveLeft()
    {
        saveLastTransform();
        transform.position += Vector3.left* speed;
    }
    public void MoveRight()
    {
        saveLastTransform();
        transform.position += Vector3.right * speed;
    }
    //가면 안 되는 곳으로 이동하게 될 때 호출
    //벽에 부딪쳤을 때
    
    private void moveBack()
    {
        //버그날 확률이 높은 코드
        //최대한 빨리 개선할 것
       transform.position = lastPos  ;
        transform.rotation=lastRot;
    }

    //블럭을 아래로 내리꽂기
    public void StraightDown()
    {
        
    }
    //속도를 올리기, down arrow가 눌리는 동안 호출
    public void FasterDown()
    {
        accelatedSpeed += 0.01f;
        //눌리게 되면 계속 호출 되니까 값을 조정해서 곱해준다
        var correctedAccelation = accelatedSpeed / 10.0f;
        if (correctedAccelation <= 0.0f) correctedAccelation = 1.0f;
        saveLastTransform();
        transform.position += Vector3.down * speed * correctedAccelation;
        
    }
   

   public void Rotate()
    {
       transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
    }

    void OnTriggerEnter(Collider other)
    {
        //블럭이라면 Block on Contact 이벤트 호출
        if (other.gameObject.tag == "BLOCK")
            EventManger.Instance.NotifyEvent(EventType.BLOCK_ON_CONTACT);
        else if (other.gameObject.tag == "WALL")
            moveBack();
    }
    //event  manger에 알리는 용도로 아무것도 하지 않는다
    private void NotifyTrigger()
    {

    }

}
