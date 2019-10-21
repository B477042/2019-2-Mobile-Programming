using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to Camera Object
//x->up,down, y=left,right, z=lean


    //mouse 조작 프로그램
    //만든시기 10월 16일, 마우스 입력

    //21일에 하는건 키보드 입력
public class FPSAim : MonoBehaviour
{
    [SerializeField]
    private  float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //way1
        //float x = Input.mousePosition.x - Screen.width / 2;
        //float y = Input.mousePosition.y - Screen.height / 2;
        //=Debug.Log. 위치 확인
        // print("x : "+x+" y : "+y+", w : "+ Screen .width+", s : "+Screen.height );

        //way1
        //y값을 가운데에 넣으면 y 축으로 회전을 해서 수평방향으로 움직이는걸로 보인다
        // transform.eulerAngles = new Vector3(-y*speed,x*speed,0);


        ////way2
        ////각 축의 마우스 이동량만큼을 반환시켜준다
        //float x = Input.GetAxis("Mouse X");
        //float y= Input.GetAxis("Mouse Y");



        ////way2
        ////마우스의 움직인 양만큼만 움직이니 더해줘야된다
        //transform.eulerAngles += new Vector3(-y * speed, x * speed, 0);

        //way3 10.21
        //keyboard
        //getkeyway. 값으로 움직임의 속도를 조절하게 된다
        //한번 누르면 땔 때까지 계속 입력이 주어지게 된다. frame단위로
        //1초 누르면 FPS가 60이면 60번 입력된다. 누르고 있는 만큼 계속 입력된다

        //    float x = 0.0f;
        //    float y = 0.0f;
        //    /* 
        //     input api에서 입력을 처리해주는 것
        //     여러 컴포넌트가 같은 프레임 안에선 같은 값을 가진다
        //     */
        //    if(Input.GetKey(KeyCode.W))
        //    {
        //        y += 1;
        //    }
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        y -= 1;
        //    }
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        x -= 1;
        //    }
        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        x += 1;
        //    }

        //    transform.eulerAngles += new Vector3(-y * speed, x * speed, 0);
        //}



        //way 4 getkey down
        float x = 0.0f;
        float y = 0.0f;
        /* 
         누르기 시작한 시점에서 호출이 된다.
         한번 누르면 한번 움직이니 연타해야 계속 움직인다
         
         */
        if (Input.GetKeyDown(KeyCode.W))
        {
            y += 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            y -= 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            x += 1;
        }

        transform.eulerAngles += new Vector3(-y * speed, x * speed, 0);
    }
}
