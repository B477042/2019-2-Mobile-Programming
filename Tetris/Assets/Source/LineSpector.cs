using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//가로 1줄을 관리하는 것
//이것 10개가 모여서 blockConstructor
//blockConsturctor의 내부에 배열로 저장 됨
//블럭이 닿았다는 정보를 받으면 그 줄에 있는 게임 오브젝트를 가리킴


public class LineSpector : MonoBehaviour
{
    private List<GameObject> line = new List<GameObject>();

    static private int createdCount=0;
    private  int floor = 1;
    private readonly int maxCapacity = 10;
    private void Awake()
    {
        
        createdCount++;
        floor =  createdCount;
        line.Capacity=   maxCapacity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        EventManger.Instance.AddEvent(EventType.BLOCK_CONSTRUCTING, receiveBlocks);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //라인 전체를 터트린다
    public void popLine()
    {
        foreach(var index in line)
        {
            Destroy(index.gameObject);
            
        }
        line.Clear();

    }
    //block이 닿았다는 이벤트에 맞춰서 진행
    //evnet type block constructiong
    //floor와 같은 값의 vector y를 가진다면 리스트에 넣어준다
    private void receiveBlocks()
    {
        var tempBlock = Controller.Instance.controllingObject;
        for (int i = 0; i < tempBlock.transform.childCount; i++)
        {
            if (tempBlock.transform.GetChild(i).transform.position.y == floor)
            {
                line.Add(tempBlock.transform.GetChild(i).gameObject);
            }
                
         }
    }
    //라인의 갯수가 꽉찼다면(10개) return true
    //아니라면 false
    public bool cheackLine()
    {
        if (line.Count == maxCapacity)
            return true;
        return false;
    }
    
}
