using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineName
 {
        A=0,S,D,F
 }
public class MusicPlayer : MonoBehaviour
{
    
    private Dictionary<LineName, MusicNodeLine> dicLines=new Dictionary<LineName, MusicNodeLine>();
   
    [SerializeField] public int HP;
    [SerializeField] public int Score;
    [SerializeField] public int CurrentCombo;
    private const int MaxHP = 100;
    


    private static MusicPlayer instance = null;
    public static MusicPlayer Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        HP = MaxHP;
        Score = 0;
        CurrentCombo = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        initNodeLines();

        EventManger.Instance.AddEvent(EventType.CheackA, cheackLineA);
        EventManger.Instance.AddEvent(EventType.CheackS, cheackLineS);
        EventManger.Instance.AddEvent(EventType.CheackD, cheackLineD);
        EventManger.Instance.AddEvent(EventType.CheackF, cheackLineF);
        EventManger.Instance.AddEvent(EventType.Damaged,ReduceHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void initNodeLines()
    {
        dicLines.Add(LineName.A, new MusicNodeLine());
        dicLines.Add(LineName.S, new MusicNodeLine());
        dicLines.Add(LineName.D, new MusicNodeLine());
        dicLines.Add(LineName.F, new MusicNodeLine());
        
        
        for(int i=0;i<4;i++)
        {
            dicLines[(LineName)i].transform.position = new Vector3(-10.0f - 2.0f * i, 0.0f, 0.0f);
        }
    }
    public MusicNodeLine GetNodeLine(LineName name)
    {
        return (dicLines[name]);
    }
    public void PlayNode(LineName name)
    {
        dicLines[name].CraeteNode();
    }
    private void AddHP()
    {
        if (HP >= 100)
        {
            HP = 100;
            return;
        }
        HP++;
    }
    private void ReduceHP()
    {
        CurrentCombo = 0;
        HP -= 15;
    }
    private void AddScore(ComboResult combo)
    {
        switch(combo)
        {
            case ComboResult.Good:
                Score += 60;
                combo++;
                AddHP();
                break;
            case ComboResult.Great:
                Score += 80;
                combo++;
                AddHP();
                break;
            case ComboResult.Perfect:
                Score += 100;
                combo++;
                AddHP();
                break;
            default:
                break;
        }
    }
    public void Play()
    {
        
    }
    private void cheackLineA()
    {
        dicLines[LineName.A].TryPop();

    }
    private void cheackLineS()
    {
        dicLines[LineName.S].TryPop();
    }
    private void cheackLineD()
    {
        dicLines[LineName.D].TryPop();
    }
    private void cheackLineF()
    {
        dicLines[LineName.F].TryPop();
    }
}
