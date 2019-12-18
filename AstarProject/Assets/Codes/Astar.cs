using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



   public enum SearchMode
    {
        FourWay,
        AllWay
    }
public struct BlockData
{
    public int Num; //객체 번호
    public float GCount; //출발 지점에서부터 이동한 거리, 초기값 -1
    public float HCount; //도착 지점까지 거리, 초기값 -1

    public Block Block;

    //모든 비교의 기준은 객체번호가 됩니다
    public static bool operator <(BlockData lhs, BlockData rhs)
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

    //적합도 반환
    public float GetFCount()
    {
        return GCount + HCount;
    }

    public void Copy(BlockData Other)
    {
        this.Num = Other.Num;
        this.Block = Other.Block;
        this.GCount = Other.GCount;
        this.HCount = Other.HCount;
    }

}

public class Astar : MonoBehaviour
{

    //이웃한 블럭들을 조사할 때, 만약 왼쪽 이웃이 벽이라면 left를 리턴 시키는 식으로 사용할 enum -> 기각
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
    

    [SerializeField] public LinkedList<BlockData> Path; //경로로 이루어진 리스트

    [SerializeField] private List<bool> wallList;

    private List<BlockData> BlockList; //모든 블럭들의 리스트 위치

    [SerializeField] private int n_horizontal, n_vertical; //map의 가로세로
                                                           //private SearchMode searchMode;
    private List<BlockData> OpenedList;
    private List<BlockData> VisitedList;

    private static Astar instance = null;
    public static Astar Instance { get { return instance; } }
    private void Awake()
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
        Path = new LinkedList<BlockData>();
        wallList = new List<bool>();
        BlockList = new List<BlockData>();
        VisitedList = new List<BlockData>();
        OpenedList = new List<BlockData>();
        //searchMode = SearchMode.FourWay;
        BuildMap(10, 10);

        //여기서 null reference가 일어난다
        StartSearch(findBlockByNum(0), findBlockByNum(89), SearchMode.FourWay);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildMap(int x, int y)
    {
        n_horizontal = x;
        n_vertical = y;
        for (int Y = 0; Y < n_vertical; Y++)
        {
            for (int X = 0; X < n_horizontal; X++)
            {
                AddBlockFromMapToBlockList(X, Y);
            }
        }
        //외부에서 데이터를 가져올 수 있게 만들것
        readWallList(AstarData.Instance.WallList);
        //  print((BlockList[10] < BlockList[20]));
    }

    //public LinkedList<BlockData> StartSearch(Vector3 Start, Vector3 Goal, SearchMode mode)
    //{
    //    /*
    //     *Step
    //     *  - 탈출 조건 
    //     *      1) index의 Fcount가 0이다. 
    //     *      2) 모든 탐색 결과, 길이 없다
    //     *      
    //     *  - 작동 구조
    //     *      1) index node를 Start의 시작 지점으로 잡는다. 
    //     *      2) index node로 갈 수 있는 지점들을 검사한다. 
    //                BlockData를 계산해준다. 
    //     *      3) 나온 값들은 list에 임시로 담아둔다. 
    //     *      4) 모든 방향으로 계산이 끝났다면, list를 정렬해준다. list.sort를 할 경우 오름차순으로 정렬이 된다
    //     *      5-1) 최적이 아닌경우, return
    //     *      5-2) path의 last가 index와 같다면, last를 pop 하고 return
    //     */

    //    bool bResult = false;

    //    //searchPath();



    //    if (Path.First != null) bResult = true;

    //    if (bResult) return Path;
    //    else return null;
    //}
    public LinkedList<BlockData>StartSearch(BlockData Start,BlockData Goal, SearchMode mode)
    {
        //A*를 이용하여 경로를 찾아준다
        calcCounts(Start, Goal, ref Start);
        Path.AddFirst(Start);
        OpenedList.Add(Start);
        //VisitedList.Add(Start);
        searchPath(Start, Goal, mode);
        paintPath();
        return Path;
    }
    private void searchPath(BlockData Current, BlockData Goal, SearchMode Mode)
    {
        //재귀 호출의 종료
        if (Path.Last.Value == Goal) return;
        //debug용, stack overflow방지
        //if (Path.Count > 120) {
        //    print("뻥!!!!");
        //        return; }
        /*
         - 작동 구조
         *      1) index node를 Current로 잡는다. 
         *      2) index node로 갈 수 있는 지점들을 검사한다. 
                    BlockData를 계산해준다. 
         *      3) 나온 값들은 list에 임시로 담아둔다. 
         *      4) 모든 방향으로 계산이 끝났다면, list를 정렬해준다. list.sort를 할 경우  적합성에 대해 오름차순으로 정렬이 된다
         *      
         *      5-1) 최적이 아닌경우, return
         *      5-2) path의 last가 index와 같다면, last를 pop 하고 return
         *        
         */
        VisitedList.Add(Current);
        OpenedList.Remove(Current);
       // Current;
        //Current지점에서 갈 수 있는 방향 리스트
        var ablePathList = cheackAbleDirection(Current, Mode);
       List<BlockData> ablePathListToBlockData = new List<BlockData>();
        //able path list를 이용하여 갈수 있는 방향의 블럭들을 리스트로 만들어준다
        foreach (var direction in ablePathList)
        {
            //방향들이 들어있는 리스트를 이용하여 갈 수 있는 방향의 블럭을 방향을 이용해 찾아온다.
            var temp = findBlockByDirection(Current, direction);
            //print("temp의 객체 번호 : " + temp.Num);

            //만약 방문노드에 있었다면 가능한 리스트에서 빼버린다
            if (VisitedList.Contains(temp))
            {
                ablePathList.Remove(direction);

            }

            else
            {
                if (!temp.Block.IsWall())
                {//ablePathListToBlockData.Add(temp);
                    calcCounts(Current, Goal, ref temp);
                    ablePathListToBlockData.Add(temp);
                    //적합도가 최적인 경우
                    //if (Path.Last.Value.GetFCount() <= temp.GetFCount())                
                    //    Path.AddLast(temp);


                    //else
                    //    Path.RemoveLast();
                    //searchPath(Path.Last.Value, Goal, Mode);
                }
            }
                
               //  tempPathList.Add(temp);
            
        }
        //갈 수 있는 방향의 리스트들을 구했으면 opened list에 넣어준다
        
        
        OpenedList .AddRange( ablePathListToBlockData);
        sortBlockDatasByCounts(ref ablePathListToBlockData);
        sortBlockDatasByCounts(ref OpenedList);
        //새로 갈 수 있게된 노드들 중에서 제일 적합성이 작은 것과 openedlist의 적합성이 제일 낮은 것을 비교
        //if (ablePathListToBlockData[0].GetFCount() < OpenedList[0].GetFCount())
        //{
        //    //경로에서 제거
        //    Path.RemoveLast();
        //    searchPath(Path.Last.Value, Goal, Mode);
        //}
        //else
        //{
        //    //가장 적합성이 낮은 것을 고른다
        //    Path.AddLast(OpenedList[0]);
        //    searchPath(Path.Last.Value, Goal, Mode);
        //}
        if (ablePathListToBlockData[0].GetFCount()>OpenedList[0].GetFCount())
        {
           // Path.RemoveLast();

        }
        else
        {
           // Path.AddLast(ablePathListToBlockData[0]);
        }
        searchPath(OpenedList[0], Goal, Mode);




        // sortBlockDatasByCounts(ref tempPathList);



        ////Path경로 linked list의 첫 칸이 비었을 경우. 즉, 아무것도 없을 때
        //if (Path.Count == 1)
        //{

        //    Path.AddLast(tempPathList.First());
        //}
        ////만약 Path 경로의 마지막 지점보다 적합성이 높다면 필요가 없다 return
        //if (Path.Last.Value.GetFCount() < tempPathList.First().GetFCount())
        //{
        //    Path.RemoveLast();
        //    searchPath(Path.Last.Value, Goal, Mode);
        //    //return;
        //}
        ////만약 갈 길이 막혔다면 그 길은 잘못됐다 pop
        //else if (tempPathList.Count==0)
        //{
        //    print("it blocked");
        //    return;
        //}
        // else
        // {

        //}
        //for(int i=Path.Count-1;i>=0;i+)
        //for(int i=0;i<tempPathList.Count;i++)
        //  {
        //      if (Path.Last.Value == Goal) return;
        //     // if (Path.Last.Value == tempPathList[i]) Path.RemoveLast();
        //        Path.AddLast(tempPathList[i]);
        //      searchPath(tempPathList[i], Goal, Mode);

        //  }
        //else
        //{
        //    Path.AddLast(tempPathList[0]);
        //    searchPath(Path.Last.Value, Goal, Mode);
        //}

    }

    private int vecToNum(Vector3 from)
    {
        return ((int) from.y * n_horizontal + (int) from.x);
    }

    private Vector3 numToVec(int from)
    {
        Vector3 temp = new Vector3(from % n_horizontal, from / n_horizontal, 0.0f);
        return temp;
    }

    //블럭을 만들자
    //입력된 좌표를 이용하여
    //
    private void AddBlockFromMapToBlockList(int X, int Y)
    {
        int index = Y * n_horizontal + X;

        Vector3 newPos = new Vector3(X, Y, 0.0f);

        BlockData newBlockData = new BlockData();
        var tempObj = Instantiate(Resources.Load("Block"), newPos, Quaternion.identity) as GameObject;
        newBlockData.Block = tempObj.GetComponent<Block>();
        // newBlockData.Block = Instantiate(Resources.Load("Block"), newPos, Quaternion.identity )as GameObject;
        newBlockData.Num = index;
        newBlockData.HCount = -1.0f;
        newBlockData.GCount = -1.0f;
        BlockList.Add(newBlockData);
    }

    private void calcCounts(BlockData Start, BlockData Goal, ref BlockData From) //시작 점, 도착점, 대상점
    {
        if (Start == null || Goal == null) return;
        From.GCount =  Vector3.Distance(Start.Block.GetPos(), From.Block.GetPos());
       // print("from Hcount => " + From.Hcount);
        From.HCount =  Vector3.Distance(Goal.Block.GetPos(), From.Block.GetPos());
      //  print("from Fcount => " + From.Fcount);
    }

    ////갈 수 없는 방향을  리스트로 짜서 리턴한다
    //private List<neighborDirection> cheackUnableDirection(BlockData Target, SearchMode Mode)
    //{
    //    List<neighborDirection> Result = new List<neighborDirection>();
    //    조사대상이 되는 BlockData의 num을 저장
    //    var temp = Target.Num;
    //    전체의 좌측 하단
    //    if (temp == 0)
    //    {
    //        Result.Add(neighborDirection.Down);
    //        Result.Add(neighborDirection.Left);
    //        if (Mode == SearchMode.AllWay)
    //        {
    //            Result.Add(neighborDirection.LDiagonalDown);
    //            Result.Add(neighborDirection.LDiagonalUp);
    //            Result.Add(neighborDirection.RDiagonalDown);
    //        }

    //    }
    //    전체의 좌측 상단
    //    else if (temp == (n_vertical - 1) * n_horizontal)
    //    {
    //        Result.Add(neighborDirection.Up);
    //        Result.Add(neighborDirection.Left);
    //        if (Mode == SearchMode.AllWay)
    //        {
    //            Result.Add(neighborDirection.LDiagonalDown);
    //            Result.Add(neighborDirection.LDiagonalUp);
    //            Result.Add(neighborDirection.RDiagonalUp);
    //        }

    //    }
    //    전체의 우측 하단
    //    else if (temp == n_horizontal)
    //    {
    //        Result.Add(neighborDirection.Right);
    //        Result.Add(neighborDirection.Down);
    //        if (Mode == SearchMode.AllWay)
    //        {
    //            Result.Add(neighborDirection.RDiagonalUp);
    //            Result.Add(neighborDirection.RDiagonalDown);
    //            Result.Add(neighborDirection.LDiagonalDown);
    //        }
    //    }
    //    전체의 우측 상단
    //    else if (temp == n_horizontal * n_vertical - 1)
    //    {
    //        Result.Add(neighborDirection.Right);
    //        Result.Add(neighborDirection.Up);
    //        if (Mode == SearchMode.AllWay)
    //        {
    //            Result.Add(neighborDirection.RDiagonalUp);
    //            Result.Add(neighborDirection.RDiagonalDown);
    //            Result.Add(neighborDirection.LDiagonalUp);
    //        }

    //    }
    //    else return null;

    //    return Result;
    //}

    //갈 수 있는 방향을  리스트로 짜서 리턴한다
    private List<neighborDirection> cheackAbleDirection(BlockData Target, SearchMode Mode)
    {
        List<neighborDirection> Result = new List<neighborDirection>();
        //조사대상이 되는 BlockData의 num을 저장
        var temp = Target.Num;
        //전체의 좌측 하단
        if (temp == 0)
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Right);
            if (Mode == SearchMode.AllWay)
                Result.Add(neighborDirection.RDiagonalUp);


        }
        
        //전체의 우측 하단
        else if (temp == n_horizontal)
        {
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Up);
            if (Mode == SearchMode.AllWay)
                Result.Add(neighborDirection.LDiagonalUp);
        }
        //전체의 좌측 상단
        else if (temp == (n_vertical - 1) * n_horizontal)
        {
            Result.Add(neighborDirection.Down);
            Result.Add(neighborDirection.Right);
            if (Mode == SearchMode.AllWay)
                Result.Add(neighborDirection.RDiagonalDown);

        }
        //전체의 우측 상단
        else if (temp == n_horizontal * n_vertical - 1)
        {
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Down);
            if (Mode == SearchMode.AllWay)
                Result.Add(neighborDirection.LDiagonalDown);

        }
        //전체의 최하단 라인. 밑으로 이동을 제한한다
        else if(temp<n_horizontal)
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.Left);
            if (Mode == SearchMode.AllWay)
            {
                Result.Add(neighborDirection.LDiagonalUp);                
                Result.Add(neighborDirection.RDiagonalUp);                
            }
        }
        //전체의 최좌측 라인. 왼쪽으로 이동을 제한한다
        else if(temp%n_horizontal==0)
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.Down);
            if (Mode == SearchMode.AllWay)
            {
                Result.Add(neighborDirection.RDiagonalUp);
                Result.Add(neighborDirection.RDiagonalDown);
            }
        }
        //전체의 최우측. 오른쪽으로 이동이 제한된다
        else if(temp%n_horizontal==n_horizontal-1)
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Down);
            if (Mode == SearchMode.AllWay)
            {
                Result.Add(neighborDirection.LDiagonalUp);
                Result.Add(neighborDirection.LDiagonalDown);
            }

        }
        //전체의 최상단. 위로 이동이 제한된다
        else if(temp%n_horizontal==n_vertical-1)
        {
            Result.Add(neighborDirection.Right);
            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Down);
            if (Mode == SearchMode.AllWay)
            {
                Result.Add(neighborDirection.LDiagonalDown);
                Result.Add(neighborDirection.RDiagonalDown);
            }
        }

        else
        {
            Result.Add(neighborDirection.Up);
            Result.Add(neighborDirection.Right);

            Result.Add(neighborDirection.Left);
            Result.Add(neighborDirection.Down);
            if (Mode == SearchMode.AllWay)
            {
                Result.Add(neighborDirection.LDiagonalUp);
                Result.Add(neighborDirection.LDiagonalDown);

                Result.Add(neighborDirection.RDiagonalUp);
                Result.Add(neighborDirection.RDiagonalDown);
            }

        }

        return Result;
    }

    //객체번호를 이용해서 Block을 찾아낸다
    private BlockData findBlockByNum(int Num)
    {
        BlockData result = BlockList[Num];

        return result;
    }

    private BlockData findBlockByDirection(BlockData Start, neighborDirection Direction)
    {
        BlockData Result = new BlockData();
        int Num = 0;
        switch (Direction)
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
                Num = Start.Num - 1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.Right:
                Num = Start.Num + 1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.LDiagonalUp:
                Num = Start.Num + n_horizontal - 1;
                Result = findBlockByNum(Num);
                break;
            case neighborDirection.LDiagonalDown:
                Num = Start.Num - n_horizontal - 1;
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

    //list로 받은 blockData 리스트에 대하여 BlockData. G count에 대한 오름차순 선택정렬
    private List<BlockData> sortBlockDatasByCounts(ref List<BlockData> target)
    {

       
            for (int i = 0; i < target.Count - 1; i++)
        { 
            for (int j = i + 1; j < target.Count; j++)
            {

                if (target[i].GetFCount() >= target[j].GetFCount())
                {
                    var temp = target[i];
                    target[i] = target[j];
                    target[j] = temp;
                }


            }
             
        }

        for (int i = 0; i < target.Count; i++)
        {
            if (target[i].Block.IsWall())
            {
                print("this is wall!" + target[i].Num);
                target.RemoveAt(i);
                i--;
            }
        }
        return target;
    }
    

       private void paintPath()
    {
        var index = Path.First;
        
        while(index!=null)
        {
            print("lets paint");
            index.Value.Block.ChangeToPathA();
            index = index.Next;
        }
    }



    //Astart Data에 선언해둔 것으로 부터 읽어 들입니다.
    private void readWallList(List<int> list)
    {
        foreach(var index in list)
        {
            BlockList[index].Block.ChangeToWall();
        }
    }
  
}

