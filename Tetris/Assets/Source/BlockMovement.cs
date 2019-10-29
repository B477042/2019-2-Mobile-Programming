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
    
    private Vector3 lastPos = new Vector3();
    private Quaternion lastRot = new Quaternion();
   
    // Start is called before the first frame update
    void Start()
    {
        
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
    
    //private void moveBack()
    //{
    //    //버그날 확률이 높은 코드
    //    //최대한 빨리 개선할 것
    //    transform.position = lastPos;
    //    transform.rotation=lastRot;
    //}

    
    
   

   public void Rotate()
    {
       transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
    }

   
   
}
