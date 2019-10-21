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
    public delegate void EventDelegate(EventType eventList, Component joinedComponent,object Params=null );
    private Dictionary<EventType,>

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
}
