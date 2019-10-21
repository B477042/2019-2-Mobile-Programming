using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  public delegate void BroadPrn();
public class TestDeli : MonoBehaviour
{

  
    public static BroadPrn notify;

    void Awake()
    {
        //// 단순히 함수를 대입한다.
        //onAwake = LogAwake;
        //if (onAwake != null) onAwake.Invoke();

        //// 람다식을 += 를 사용해 onAwake 델리게이트에 추가한다.
        //CheckForAwake otherAwake = () => Debug.Log("Awake!! Twice!!");
        //onAwake += otherAwake;
        //if (onAwake != null) onAwake.Invoke();

        //// 람다식을 -= 를 사용해 onAwake 델리게이트에서 지운다.
        //onAwake -= otherAwake;
        //if (onAwake != null) onAwake.Invoke();
        

        //// null 을 넣어 빈 델리게이트를 표현한다.
        //onAwake = null;
    }

    void Start()
    {
        //if (onAwake != null) onAwake.Invoke();
    }

    void LogAwake()
    {
        Debug.Log("Awake!!");
    }

    public static void LogK()
    {
        print("KKKK");
    }
    private void Lee()
    {
        print("이학주!");
    }
    void Wawoo()
    {
        print("워우워우워");
    }
}
