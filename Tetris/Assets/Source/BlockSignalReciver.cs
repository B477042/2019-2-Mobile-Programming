using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSignalReciver : MonoBehaviour
{
    List<EventType> signalList;

    private static BlockSignalReciver instance = null;
    public static BlockSignalReciver Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance != null)
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
        signalList = new List<EventType>();
        EventManger.Instance.AddEvent(EventType.BLOCK_SPAWNED, initSignal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //notify event 대신 여기로 보내가 만든다
    //추가로 누가 보냈는지 명시한다
    public void SendSignalToReciver(EventType eventType, GameObject sender )
    {
        //현재 조종 중인 obj의 신호만 받는다
        if (sender != Controller.Instance.controllingObject) return;
        //중복인지 검사한다
        if (signalList.Contains(eventType)) return;
        //처음 온 신호는 넣어둔다
        signalList.Add(eventType);
        //신호를 알린다
        EventManger.Instance.NotifyEvent(eventType);
    }
    //새 블럭이 만들어질 때, 
   private void initSignal()
    {
        signalList.Clear();
    }
    
}
