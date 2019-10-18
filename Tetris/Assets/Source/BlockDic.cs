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


public  class BlockDic : MonoBehaviour
{
    

    public static  Dictionary<BlocksEnum,string> Dic=new Dictionary<BlocksEnum, string>()
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

