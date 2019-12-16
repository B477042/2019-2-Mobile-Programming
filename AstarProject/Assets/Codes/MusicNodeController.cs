using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NodeLine
{
    public List<MusicNode> Line;
    public string LineName;
    public Vector3 LineZeroPoint;

}
public class MusicNodeController : MonoBehaviour
{
    private const float DetectRange = 3.0f;

    private const float MissRange = 2.0f;

    private const float GoodRange = 1.0f;

    private const float GreatRange = 0.5f;

    private const float PerfectRange = 0.0f;

    //각 줄에 대한 list
    [SerializeField] public List<MusicNode> SLine;
    [SerializeField] public List<MusicNode> ALine;
    [SerializeField] public List<MusicNode> DLine;
    [SerializeField] public List<MusicNode> FLine;
    private float speed;


    private void Awake()
    {
        SLine =new List<MusicNode>();
        ALine=new List<MusicNode>();
        DLine =new List<MusicNode>();
        FLine=new List<MusicNode>();
        speed = 0.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //주어진 라인에 대한 업데이트 처리
    void UpdateList(List<MusicNode> Target)
    {
        //foreach (var i in Target)
        //{
        //    i.Drop(speed);
        //}
    }

    private void increaseSpeed()
    {
        speed += 0.5f;
    }
}
