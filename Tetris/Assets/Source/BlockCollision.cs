using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 모든 블럭에 적용될 충돌체크 컴포넌트
 * 닿았을 때 처리
 * 
 */

public class BlockCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        EventManger.Instance.AddEvent(EventType.LINE_WORK_COMPLETE, AddRigidbody);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //처리할 때 주의할 것
    //블럭이 무언가에 닿았을 때 호출되니까 그 타입을 분석한다
    //그리고 방향을 분석해서 맞는 이벤트를 호출한다
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManger.Instance.GetPower()) return;
        //이미 쌓아진 블럭들은 trigger를 껐지만 보험으로
        if (gameObject.tag == "StackedBlock") return;

      //  print("Hi : " + other.name+"i am" +name);
        var distanceX = (this.gameObject.transform.position.x - other.gameObject.transform.position.x)*-1.0f;
        var distanceY = (this.gameObject.transform.position.y - other.gameObject.transform.position.y);
     //   print("U and I distance X : " + distanceX + " Y : " + distanceY);
        // 그 방향으로 움직일 수 없게 컨트롤러 이벤트를 호출한다
        //벽과 만났을 경우
        if(other.tag=="WALL")
        {
            if (distanceX == Vector3.left.x)
            {
                BlockSignalReciver.Instance.SendSignalToReciver(EventType.BLOCK_CONTACT_LEFT, gameObject.transform.parent.gameObject);
                
            }

           else if (distanceX == Vector3.right.x)
            {
                BlockSignalReciver.Instance.SendSignalToReciver(EventType.BLOCK_CONTACT_RIGHT, gameObject.transform.parent.gameObject);
                
            }
        }
        
       //쌓아진 블럭들의 테그
        if (other.tag=="Stacked")
        {
            //아래 방향으로 만나지 않으면 필요가 없다
            if (distanceX != 0) return;
          
            BlockSignalReciver.Instance.SendSignalToReciver(EventType.BLOCK_CONSTRUCTING, gameObject.transform.parent.gameObject);
          
            BlockSignalReciver.Instance.SendSignalToReciver(EventType.LINE_WORK_COMPLETE, gameObject.transform.parent.gameObject);
        }
        else if(other.tag=="Bottom")
        {
            BlockSignalReciver.Instance.SendSignalToReciver(EventType.BLOCK_CONSTRUCTING, gameObject.transform.parent.gameObject);

            BlockSignalReciver.Instance.SendSignalToReciver(EventType.LINE_WORK_COMPLETE, gameObject.transform.parent.gameObject);
        }
            

        if (other.gameObject.tag == "DeadLine")
            //EventManger.Instance.NotifyEvent(EventType.GAME_OVER);
        BlockSignalReciver.Instance.SendSignalToReciver(EventType.GAME_OVER, gameObject.transform.parent.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
       // print("Bye~ " + other.name);
       // print("My position is x : " + transform.position.x + "Y : " + transform.position.y);
        var distanceX = (this.gameObject.transform.position.x - other.gameObject.transform.position.x);
       // print("Distance to : " + distanceX);
        if (distanceX ==  -2* Vector3.left.x)
         //   EventManger.Instance.NotifyEvent(EventType.BLOCK_NOT_CONTACT_LEFT);
        BlockSignalReciver.Instance.SendSignalToReciver(EventType.BLOCK_NOT_CONTACT_LEFT, gameObject.transform.parent.gameObject);
        if (distanceX ==  2* Vector3.right.x)
         //   EventManger.Instance.NotifyEvent(EventType.BLOCK_NOT_CONTACT_RIGHT);
        BlockSignalReciver.Instance.SendSignalToReciver(EventType.BLOCK_NOT_CONTACT_RIGHT, gameObject.transform.parent.gameObject);

        //EventManger.Instance.NotifyEvent(EventType.BLOCK_NOT_CONTACT_LEFT);
        //if ()
        //    EventManger.Instance.NotifyEvent(EventType.BLOCK_NOT_CONTACT_RIGHT);
    }

    //안착된 객체에 rigibody를 넣어준다
    public void AddRigidbody()
    {
        turnOffTrigger();
        if (gameObject.GetComponent<Rigidbody>()) return;
        gameObject.AddComponent<Rigidbody>();
        var rigi = gameObject.GetComponent<Rigidbody>();
        rigi.useGravity = false;
        rigi.constraints = RigidbodyConstraints.FreezeRotation;
        rigi.constraints= RigidbodyConstraints.FreezePosition;

    }
   private void turnOffTrigger()
    {
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
    private void turnOffCollision()
    {
        //this.
    }
}
