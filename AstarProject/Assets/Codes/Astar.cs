using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
   public enum BlocType
    {
        wall,
        plain
    }
   public enum SearchMode
    {
        FourWay,
        AllWay
    }
  public struct BlockData
    {
        public int Num;//객체 번호
        public float Hcount;//출발 지점까지 거리
        public float Fcount;//도착 지점까지 거리
       
        
    }


    public LinkedList<Vector3> Path;//경로로 이루어진 리스트
    private List<bool> wallList;
    private List<Vector3> roadList;
    private int horizontal,vertical;//map의 가로세로

    // Start is called before the first frame update
    void Start()
    {
        Path = new LinkedList<Vector3>();
        wallList = new List<bool>();
        roadList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        serachPath();


        if (Path.First != null) bResult = true;

        if (bResult) return Path;
        else return null;
    }

    private void serachPath()
    {
        /*
         - 작동 구조
         *      1) index node를 Start의 시작 지점으로 잡는다. 
         *      2) index node로 갈 수 있는 지점들을 검사한다. 
                    BlockData를 계산해준다. 
         *      3) 나온 값들은 list에 임시로 담아둔다. 
         *      4) 모든 방향으로 계산이 끝났다면, list를 정렬해준다. list.sort를 할 경우 오름차순으로 정렬이 된다
         *      5-1) 최적이 아닌경우, return
         *      5-2) path의 last가 index와 같다면, last를 pop 하고 return
         */


    }

    private int vecToNum(Vector3 from)
    {
        return ((int)from.y * horizontal + (int)from.x);
    }
    private Vector3 numToVec(int from)
    {
        Vector3 temp = new Vector3(from % horizontal, from / horizontal, 0.0f);
        return temp;
    }

    private 
}

