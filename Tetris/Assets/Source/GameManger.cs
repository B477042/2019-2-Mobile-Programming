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

    private int score { get; set; }
    private int popCount = 0;
    private int level = 1;
    private int exp = 0;
    private readonly int maxExp = 10;
    private bool power;
    

     void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        power = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Controller>();
        gameObject.AddComponent<Spawner>();
        gameObject.AddComponent<BlockConstructor>();
        gameObject.AddComponent<EventManger>();
        gameObject.AddComponent<BlockSignalReciver>();

        EventManger.Instance.AddEvent(EventType.LINE_POPED, addExp);
        EventManger.Instance.AddEvent(EventType.LINE_POPED, addPopCount);
        EventManger.Instance.AddEvent(EventType.LINE_POPED, addScore);
        EventManger.Instance.AddEvent(EventType.GAME_OVER, showResult);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void addPopCount()
    {
        popCount++;
    }
    private void addScore()
    {
        score += 100;
    }
    private void addExp()
    {
        exp++;
        if(exp>=maxExp)
        {
            level++;
            exp = 0;
        }
    }

    private void showResult()
    {
        power = false;
    }
    public bool GetPower()
    {
        return power;
    }


    
}
