﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



   public enum SearchMode
    {
        FourWay,
        AllWay
    }

public class Astar : MonoBehaviour
{

    //이웃한 블럭들을 조사할 때, 만약 왼쪽 이웃이 벽이라면 left를 리턴 시키는 식으로 사용할 enum
    private enum neighborDirection
    {
        None,
        Up,
        Down,
        Left,
        Right,
        LDiagonalUp,
        LDiagonalDown,
        RDiagonalUp,
        RDiagonalDown
    }
    
    //private Dictionary<neighborDirection,Block> ;
private struct BlockData 
{
    public int Num;//객체 번호
    public int Hcount;//출발 지점까지 거리, 초기값 -1
    public int Fcount;//도착 지점까지 거리, 초기값 -1
    public Block Block;
     //모든 비교의 기준은 객체번호가 됩니다
        public static bool operator <(BlockData lhs,BlockData rhs)
    {
            if (lhs.Num < rhs.Num) return true;
            else return false;
    }
        public static bool operator >(BlockData lhs, BlockData rhs)
    {
            if (lhs.Num > rhs.Num) return true;
            else return false;
     }
        public static bool operator ==(BlockData lhs, BlockData rhs)
        {
            if (lhs.Num == rhs.Num) return true;
            else return false;
        }
        public static bool operator !=(BlockData lhs, BlockData rhs)
        {
            if (lhs.Num != rhs.Num) return true;
            else return false;
        }
        public static bool operator >=(BlockData lhs, BlockData rhs)
        {
            if (lhs.Num >= rhs.Num) return true;
            else return false;
        }
        public static bool operator <=(BlockData lhs, BlockData rhs)
        {
            if (lhs.Num <= rhs.Num) return true;
            else return false;
        }

    }
    
    [SerializeField]
    public LinkedList<Vector3> Path;//경로로 이루어진 리스트

    [SerializeField]
    private List<bool> wallList;

    [SerializeField]
    private List<BlockData> BlockList;//모든 블럭들의 리스트 위치

    [SerializeField]
    private int n_horizontal,n_vertical;//map의 가로세로
    //private SearchMode searchMode;

    // Start is called before the first frame update
    void Start()
    {
        Path = new LinkedList<Vector3>();
        wallList = new List<bool>();
        BlockList = new List<BlockData>();
        //searchMode = SearchMode.FourWay;
        BuildMap(10,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void BuildMap(int x,int y)
    {
        n_horizontal = x;
        n_vertical = y;
        for(int Y=0;Y<n_vertical;Y++)
        {
            for(int X=0;X<n_horizontal;X++)
            {
                addBlockFromMapToBlockList(X, Y);
            }
        }
      //  print((BlockList[10] < BlockList[20]));
    }
    public LinkedList<Vector3> StartSearch(  Vector3  Start  ,  Vector3 Goal, SearchMode mode)
    {
        /*
         *Step
         *  - 탈출 조건 
         *      1) index의 Fcount가 0이다. 
         *      2) 모든 탐색 결과, 길이 없다
         *      
         *  - 작동 구조
         *      1) index node를 Start의 시작 지점으로 잡는다. 
         *      2) index node로 갈 수 있는 지점들을 검사한다. 
                    BlockData를 계산해준다. 
         *      3) 나온 값들은 list에 임시로 담아둔다. 
         *      4) 모든 방향으로 계산이 끝났다면, list를 정렬해준다. list.sort를 할 경우 오름차순으로 정렬이 된다
         *      5-1) 최적이 아닌경우, return
         *      5-2) path의 last가 index와 같다면, last를 pop 하고 return
         */

        bool bResult = false;

        //searchPath();



        if (Path.First != null) bResult = true;

        if (bResult) return Path;
        else return null;
    }

    private void searchPath(BlockData Start, BlockData Goal, SearchMode mode)
    {
        /*
         - 작동 구조
         *      1) index node를 Start로 잡는다. 
         *      2) index node로 갈 수 있는 지점들을 검사한다. 
                    BlockData를 계산해준다. 
         *      3) 나온 값들은 list에 임시로 담아둔다. 
         *      4) 모든 방향으로 계산이 끝났다면, list를 정렬해준다. list.sort를 할 경우 오름차순으로 정렬이 된다
         *      5-1) 최적이 아닌경우, return
         *      5-2) path의 last가 index와 같다면, last를 pop 하고 return
         */
        var indexNode = Start;
        //Start지점에서 갈 수 있는 방향 리스트
        var ablePathList = cheackAbleDirection(indexNode);


    }

    private int vecToNum(Vector3 from)
    {
        return ((int)from.y * n_horizontal + (int)from.x);
    }
    private Vector3 numToVec(int from)
    {
        Vector3 temp = new Vector3(from % n_horizontal, from / n_horizontal, 0.0f);
        return temp;
    }
    //블럭을 만들자
    //입력된 좌표를 이용하여
    //
    private void addBlockFromMapToBlockList(int X, int Y)
    {
        int index = Y * n_horizontal + X;
       
        Vector3 newPos = new Vector3(X, Y, 0.0f);

        BlockData newBlockData=new BlockData();

        newBlockData.Block = Instantiate(Resources.Load("Block"), newPos, Quaternion.identity )as Block;
        newBlockData.Num = index;
        newBlockData.Fcount = -1;
        newBlockData.Hcount = -1;
        BlockList.Add(newBlockData);
    }
    private void calcCounts(Vector3 Start, Vector3 Goal, BlockData From)
    {
        if (Start == null || Goal == null) return;
        From.Hcount = (int)Vector3.Distance(Start, From.Block.GetPos());
        From.Fcount = (int)Vector3.Distance(Goal, From.Block.GetPos());

    }
    //갈 수 없는 방향을  리스트로 짜서 리턴한다
    private List<neighborDirection>cheackUnableDirection(BlockData Target)
    {
        List<neighborDirection> Result = new List<neighborDirection>();
        //조사대상이 되는 BlockData의 num을 저장
        var temp = Target.Num;
        //전체의 좌측 하단
        if (temp == 0)
        {
            Result.Add(neighborDirection.Down);
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.LDiagonalDown);
            Result.Add(neighborDirection.LDiagonalUp);
            Result.Add(neighborDirection.RDiagonalDown);

        }
        //전체의 좌측 상단
        else if (temp==(n_vertical-1)*n_horizontal)
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.LDiagonalDown);
            Result.Add(neighborDirection.LDiagonalUp);
            Result.Add(neighborDirection.RDiagonalUp);

        }
        //전체의 우측 하단
        else if (temp==n_horizontal)
        {
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.Down);
            Result.Add(neighborDirection.RDiagonalUp);
            Result.Add(neighborDirection.RDiagonalDown);
            Result.Add(neighborDirection.LDiagonalDown);
        }
        //전체의 우측 상단
        else if (temp==n_horizontal*n_vertical-1)
        {
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.RDiagonalUp);
            Result.Add(neighborDirection.RDiagonalDown);
            Result.Add(neighborDirection.LDiagonalUp);

        }
        else return null; 
        return Result;
    }
    //갈 수 있는 방향을  리스트로 짜서 리턴한다
    private List<neighborDirection> cheackAbleDirection(BlockData Target)
    {
        List<neighborDirection> Result = new List<neighborDirection>();
        //조사대상이 되는 BlockData의 num을 저장
        var temp = Target.Num;
        //전체의 좌측 하단
        if (temp == 0)
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.RDiagonalUp);


        }
        //전체의 좌측 상단
        else if (temp == (n_vertical - 1) * n_horizontal)
        {
            Result.Add(neighborDirection.Down);
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.RDiagonalDown);

        }
        //전체의 우측 하단
        else if (temp == n_horizontal)
        {
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.LDiagonalUp);
        }
        //전체의 우측 상단
        else if (temp == n_horizontal * n_vertical - 1)
        {
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Down);
            Result.Add(neighborDirection.LDiagonalDown);

        }
        else
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Right);
           
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Down);
              Result.Add(neighborDirection.LDiagonalUp);
            Result.Add(neighborDirection.LDiagonalDown);
          
            Result.Add(neighborDirection.RDiagonalUp);
            Result.Add(neighborDirection.RDiagonalDown);
        }
        return Result;
    }
    //객체번호를 이용해서 Block을 찾아낸다
    private BlockData findBlockByNum(int Num)
    {
        BlockData result=new BlockData();
        result=BlockList[Num];

        return result;
    }
    private BlockData findBlockByDirection(BlockData Start,neighborDirection Direction)
    {
        BlockData Result=new BlockData();
        int Num = 0;
        switch(Direction)
        {
            case neighborDirection.None:
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.Up:
                Num = Start.Num + n_horizontal;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.Down:
                Num = Start.Num - n_horizontal;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.Left:
                Num = Start.Num -1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.Right:
                Num = Start.Num + 1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.LDiagonalUp:
                Num = Start.Num + n_horizontal-1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.LDiagonalDown:
                Num = Start.Num - n_horizontal-1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.RDiagonalUp:
                Num = Start.Num + n_horizontal + 1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.RDiagonalDown:
                Num = Start.Num - n_horizontal + 1;
                Result = findBlockByNum(Num);
                break;
        }
        return Result;
       
    }
       
}

