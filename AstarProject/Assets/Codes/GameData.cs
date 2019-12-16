using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//게임에서 사용될 데이터들 모아둔 곳

    

public class GameData : MonoBehaviour
{
    public List<BlockType> WallList;
    public List<Vector3> MusicNodeLinePos;
    public Dictionary<ComboResult, int> ScoreData;
    private void Awake()
    {
        WallList = new List<BlockType>();
        MusicNodeLinePos = new List<Vector3>();
        ScoreData = new Dictionary<ComboResult, int>();
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
