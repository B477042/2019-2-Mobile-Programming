using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConstructor : MonoBehaviour
{
    List<LineSpector> lines;
    

    // Start is called before the first frame update
    void Start()
    {
        lines = new List<LineSpector>();

        //line spector 10개를 추가
        for (int i = 0; i < 10; i++)
        {

            lines.Add(gameObject.AddComponent<LineSpector>());
        }
        //print("this is constructor, " + lines.Count);
        EventManger.Instance.AddEvent(EventType.BLOCK_CONSTRUCT_FINISH,CheackLine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //event type block construct finish
    void CheackLine()
    {
        //get object from Controller(Controlling)
       var block= Controller.Instance.controllingObject;
        //then cheack Child Blocks. 
        for(int index=0;index<block.transform.childCount;index++)
        {
            //return, if child block  is null
            if (index > block.transform.childCount) return;
            if (block.transform.GetChild(index).gameObject == null) continue;
            //save height of child block to cheack line.
            //bring LineSpector that be in charged of child block's height
            //if that line is full, pop that line
            //and Rebuliding
            var lineNum = block.transform.GetChild(index).transform.position.y;
            if (lineNum < 0)
            {
                print("ERROR");
                return;
            }
            if(lines[(int)lineNum].IsLineFull())
            {
                lines[(int)lineNum].popLine();
                EventManger.Instance.NotifyEvent(EventType.LINE_POPED);
                RebuildLines((int)lineNum);
                index = 0;
            }
        }
    }
    //event type line complete
    //완성된 라인이 터진 후 줄 정리
    //깊은 복사로 구현할 것
    //재귀형식으로
    //from은 비어있는 줄의 번호다
    private void RebuildLines(int from)
    {
        //line이 비워져 있지 않다면 return
        if (!lines[from].IsLineEmpty()) return;

        int to = from + 1;//내려와야될 윗줄

        //to에 있는 데이터를 밑으로 끌어온다
        for(int index=0;index<lines[to].GetLineCapacity();index++)
         lines[from].InsertValue(lines[to],index);
      
        lines[to].popLine();

        RebuildLines(to);
    }


}
