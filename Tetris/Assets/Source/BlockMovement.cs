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
    private float beforeAccelatedSpeed = 0.0f;
   
    // Start is called before the first frame update
    void Start()
    {
        EventManger.Instance.AddEvent(EventType.BLOCK_ON_CONTACT, NotifyTrigger);
        //prefab으로 불러온 도형의 충돌 범위를 0.95f로 제한해둔다
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
        for (int i = 0; i < gameObject.transform.childCount;i++)
        {
            gameObject.transform.GetChild(i).GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
        }
    }
    //public static Dictionary<KeyCode,InputAction>Dic=new Dictionary<KeyCode, InputAction>()
    //{
    //    { KeyCode.A,inputAction. MoveLeft},

    //}
   

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void MoveDown()
    {
        transform.position+= Vector3.down*speed;
    }
    public void MoveLeft()
    {
        transform.position += Vector3.left* speed;
    }
    public void MoveRight()
    {
        transform.position += Vector3.right * speed;
    }

    //블럭을 아래로 내리꽂기
    public void StraightDown()
    {
        
    }
    //속도를 올리기, down arrow가 눌리는 동안 호출
    public void FasterDown()
    {
        beforeAccelatedSpeed = speed;
        speed += 0.001f;
    }
    //원래 속도로, down arrow가 떨어지면 호출된다
    public void RestoreSpeed()
    {
        speed = beforeAccelatedSpeed;
    }

   public  void Rotate()
    {
       transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
    }

    void OnTriggerEnter(Collider other)
    {
        EventManger.Instance.NotifyEvent(EventType.BLOCK_ON_CONTACT);
    }
    //event  manger에 알리는 용도로 아무것도 하지 않는다
    private void NotifyTrigger()
    {

    }

}
