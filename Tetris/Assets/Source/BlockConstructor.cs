using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConstructor : MonoBehaviour
{
    List<LineSpector> lines = new List<LineSpector>();
    

    // Start is called before the first frame update
    void Start()
    {
        //line spector 10개를 초기화
        for (int i = 0; i < 10; i++)
            lines.Add(new LineSpector());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //event type block construct finish
    void CheackLine()
    {
       var block= Controller.Instance.controllingObject;
        for(int index=0;index<block.transform.childCount;index++)
        {
            var lineNum = block.transform.GetChild(index).transform.position.y;
            if(lines[(int)lineNum-1].cheackLine())
            {
                lines[(int)lineNum - 1].popLine();
                
            }
        }
    }
    //event type line complete
    //완성된 라인을 터트리고 
    private void RebuildLines(int from)
    {
        

    }


}
