using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 모든 블럭에 적용될 충돌체크 컴포넌트
 * 닿았을 때 처리
 * 
 */

public class BlockCollison : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       

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
        
        var distance = (this.gameObject.transform.position - other.gameObject.transform.position)*-1.0f;
        // 그 방향으로 움직일 수 없게 컨트롤러 이벤트를 호출한다
        if (distance == Vector3.left)
            EventManger.Instance.NotifyEvent(EventType.BLOCK_CONTACT_LEFT);
        if (distance == Vector3.right)
            EventManger.Instance.NotifyEvent(EventType.BLOCK_CONTACT_RIGHT);
       if (distance == Vector3.up)
            EventManger.Instance.NotifyEvent(EventType.BLOCK_CONSTRUCT_FINISH);

        if (other.gameObject.tag == "DeadLine")
            EventManger.Instance.NotifyEvent(EventType.GAME_OVER);
    }

    private void OnTriggerExit(Collider other)
    {
        var distance = (this.gameObject.transform.position - other.gameObject.transform.position) * -1.0f;
        if (distance == 2.0f * Vector3.left)
            EventManger.Instance.NotifyEvent(EventType.BLOCK_NOT_CONTACT_LEFT);
        if (distance == 2.0f * Vector3.right)
            EventManger.Instance.NotifyEvent(EventType.BLOCK_NOT_CONTACT_RIGHT);
    }



}
