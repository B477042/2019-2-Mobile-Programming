using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EventType
{
    //블럭이 바닥이나 다른 블럭과 닿았다
    BLOCK_ON_CONTACT = 0,
    //블럭이 벽에 닿았다
    BLOCK_ON_CONTACT_TO_WALL,
    //줄이 완성됐다
    LINE_COMPLETE,
    //줄이 터졌다. 위에 있는 것들은 내려와야 된다
    LINE_POPED,
    //게임오버
    GAME_OVER
}

public class EventManger : MonoBehaviour
{
    //해당되는 이벤트 타입에 참가할 스크립트를 넘겨 받는다.
    //이 delegate 형식을 지키는 함수를 실행 시키는 것
    //! delegate는 함수 포인터다. 함수포인터다.
    public delegate void EventDelegate();
    //EventType에서 실행시켜야 될 component의 함수(EventDelegate)를 리스트의 형태로서 저장해서, 이 이벤트에서 실행 시켜야 될 함수들을 저장한다
    //구동 방식은 dic에 등록된 eventType을 호출해서 list를 가져오고 list를 순회하면서 이벤트로 실행시킬 함수들을 다 불러서 실행 시킨다
    
    private Dictionary<EventType, List<EventDelegate>> eventDic = new Dictionary<EventType, List<EventDelegate>>();

  private static  EventManger instance = null;
   public static EventManger Instance { get { return instance; } }
    private void Awake()
    {
        if(instance !=null)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Dic에 해당되는 값(list)를 불러온다. 만약 이 값(list)가 null이라면 새로 추가시켜 주고
    //null이 아니라면 새로운 list를 만들어서 기존의 list 속성 값들을 받아온 후 마지막에 새로 들어온 eventDelegate 함수를 받아와서 저장한다
    //그리고 그것을 dic에 저장
    //호출 시점을 기준으로 하기에 when으로 입력인자 이름을 정함
    public void AddEvent(EventType eventType, EventDelegate eventDelegate)
    {
        List<EventDelegate> tempList = null;
        //TryGetValue 함수는 eventType의 value값 list를 tempList가 가리키게 해준다. 만약 value값이 있다면 true 반환
        //만약 value 값이 있다면 리스트에 그 함수를 추가해준다.
        if(eventDic.TryGetValue(eventType,out tempList))
        {
            tempList.Add(eventDelegate);
            return;
        }

        //이하는 처음으로 이벤트를 등록하는 경우다.
        //새로 list를 작성해서 dic에 넣어준다
        tempList = new List<EventDelegate>();
        tempList.Add(eventDelegate);
        eventDic.Add(eventType, tempList);
        
    }
    /*
     * 
     * 1) Dic에서 when의 값에 매칭된 eventDelegate의 list가 비었는지 검사한다
     * 1-1)비었으면 return
     * 2)list를 순회하면서 이벤트를 호출
     */
    public void NotifyEvent(EventType eventType)
    {
        List<EventDelegate> tempList = null;
        //matching되는 value값이 없다면 return
        print("Contact!!");
        if (!eventDic.TryGetValue(eventType, out tempList))  return;
        
        var listPointer = eventDic[eventType];

        foreach (var x in listPointer)
            if(x!=null)
            x();
        

    }

    

}
