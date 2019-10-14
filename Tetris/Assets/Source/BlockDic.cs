using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public  enum  BlocksEnum
    {
        Cube=0,
        LeftStair,
        LStick,
        ReverseLStick,
        RightStair,
        Stick,
        Yo
    };


public class BlockDic : MonoBehaviour
{
    private static BlockDic instance = null;
    public static BlockDic Instance
    {
        get
        {
            return instance;
        }
    }



    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        // Debug.LogWarning("Game manger instance Called");

        DontDestroyOnLoad(this);
    }


    public static Dictionary<BlocksEnum,string> Dic=new Dictionary<BlocksEnum, string>()
 {
     { BlocksEnum.Cube,"Cube"},
     {BlocksEnum.LeftStair,  "LeftStair"},
     {BlocksEnum.LStick,  "LStick"},
     {BlocksEnum.ReverseLStick,"ReverseLStick" },
     {BlocksEnum.RightStair, "RightStair"},
     {BlocksEnum.Stick,  "Stick"},
     {BlocksEnum.Yo, "Yo"}
 };
    
    
}

