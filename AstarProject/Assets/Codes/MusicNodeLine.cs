using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//노드가 떨어지는 라인
/*
 그 라인만 담당한다. 
     */

    public enum ComboResult
{
    None=0,
    Miss,
    Good,
    Great,
    Perfect

}
public struct NodeLine
{
    public List<MusicNode> Line;
    public string LineName;
    public Vector3 LineZeroPoint;

}
public class MusicNodeLine : MonoBehaviour
{
    private const float DetectRange = 3.0f;

    private const float MissRange = 2.0f;

    private const float GoodRange = 1.0f;

    private const float GreatRange = 0.5f;

    private const float PerfectRange = 0.0f;

    //각 줄에 대한 Linkedlist
    [SerializeField] public LinkedList<GameObject> Line;

    public  Vector3 LinePos;
    public Vector3 LineSpawnPos;
    public  string LineName;
    //[SerializeField] public List<MusicNode> ALine;
    //[SerializeField] public List<MusicNode> DLine;
    //[SerializeField] public List<MusicNode> FLine;
    private static float speed;


    private void Awake()
    {
        Line =new LinkedList<GameObject>();
        //ALine=new List<MusicNode>();
        //DLine =new List<MusicNode>();
        //FLine=new List<MusicNode>();
         speed = 0.5f;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Line.Count == 0) return;
        foreach(var index in Line)
        {
            if (index.GetComponent<MusicNode>())
                index.GetComponent<MusicNode>().Drop(LinePos, speed);
        }
    }
    //버튼이 눌렸을 때, 그것의 콤보 결과 값을 리턴해준다. 
    private ComboResult CheackCombo()
    {
        float distance = Vector3.Distance(LinePos, Line.First.Value.GetPos());
        if (distance <= PerfectRange) return ComboResult.Perfect;
        else if (distance <= GreatRange) return ComboResult.Great;
        else if (distance <= GoodRange) return ComboResult.Good;
        else if (distance <= MissRange) return ComboResult.Miss;
        else return ComboResult.None;
    }
    //결과에 따라 팝 처리
    public void Pop(ComboResult result)
    {
        switch(result)
        {
            case ComboResult.None:
                return;
                break;
                //miss가 뜬다면 다시 누를 수 없어야 된다
            case ComboResult.Miss:
                
                break;
            case ComboResult.Good:
                Line.First.Value.GetComponent<MusicNode>().PopNode();
                Line.RemoveFirst();
                break;
            case ComboResult.Great:
                break;
            case ComboResult.Perfect:
                break;

        }
    }
    public GameObject CraeteNode()
    {
        var newNode = Resources.Load("MusicNode")as GameObject;
        newNode.transform.position = LineSpawnPos;
        Line.AddLast(newNode);
        return newNode;
    }
    

    public static  void increaseSpeed()
    {
        speed += 0.5f;
    }
    public static void DecreaseSpeed()
    {
        
        speed -= 0.5f;
        if (speed < 0.0f) speed = 0.0f;
    }
    public void InitLine(Vector3 LinePos, Vector3 SpawnPos, string Name)
    {
        this.LinePos = LinePos;
        this.LineSpawnPos = SpawnPos;
        this.LineName = Name;
    }
}
