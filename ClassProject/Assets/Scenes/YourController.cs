using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourController : MonoBehaviour
{
    private void Awake()
    {
        print("your Controller Awake");
       
        

    }
    // Start is called before the first frame update
    void Start()
    {
        print("Your Controller Start");
        //world에 있는 Cube라는 이름의 GameObject를 찾아준다
        //return value = GameObject
        //var KeyWord. 이미 선언된 자료형으로 자동으로 연결. var로 선언한 객체는 처음 지정해준 객체만 지정할 수 있다
        //var의 입력인자에 대해 찾았으면 Cube랑 연결 될거고 못 찾으면 Null이 올 것이다
        var cube = GameObject.Find("Cube");
        //이거 레퍼런스임(int?)a
        //if (cube == null)
        //{
        //    print("Cant find Cube");
        //}
        //else
        //{
        //   //if(cube.GetComponent<MyController>())
        //   // {
        //   //     print("This cube has my Controller");
        //   // }
        //   //else
        //   // {
        //   //     print("This cube has not my Controller");
        //   // }
        //}
        //Awake에서 못 찾을줄 알았다고 함. 사실 start함수에 넣어줄려고 했다
        //Script에 component가 잘 연결 됐는지 볼려면 Start에만 넣어줘야된다


        //교수님코드
        var myController = cube.GetComponent<MyController>();
        if (!myController) return;
        gotcha();
       print( myController.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotcha()
    {
        print("U got me");
    }
}
