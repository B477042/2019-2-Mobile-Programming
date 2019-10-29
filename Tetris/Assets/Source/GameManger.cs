using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//싱글턴으로
//게임이 끝나는 조건
//점수 현황
//LineManger 호출해서 Line 검사하기

public class GameManger : MonoBehaviour
{
    private static GameManger instance = null;
    public static GameManger Instance
    {
        get
        {
            return instance;
        }
    }

    private int score = 0;
    private int popCount = 0;
    private int level = 1;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
