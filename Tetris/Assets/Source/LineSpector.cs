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
    private  int floor = 0;
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
    //Block Constructor에서 호출해주는게 최적화에 더 좋을거 같다
    //모든 라인 스펙터에서 일일이 다 검사하게 되니까
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
    public bool IsLineFull()
    {
        if (line.Count == maxCapacity)
            return true;
        return false;
    }
    public bool IsLineEmpty()
    {
        return (line.Count == 0) ? true : false;
    }
   //Block Constructor의 ReBuilding에 쓰인다
   //other의 index 값(GameObject)를 가리키게 한다.
   //호출하는 곳은 ReBuilding으로 한정한다
    public void InsertValue(LineSpector other,int index)
    {
        //index number가 9보다 크면 return
        if (index > maxCapacity-1) return;

        line.Add(other.line[index]);
    }

    public int GetLineCount()
    {
        return line.Count;
    }
}
