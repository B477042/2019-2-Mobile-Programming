using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to Camera Object
//x->up,down, y=left,right, z=lean
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

        //way2
        //각 축의 마우스 이동량만큼을 반환시켜준다
        float x = Input.GetAxis("Mouse X");
        float y= Input.GetAxis("Mouse Y");
        //way1
        //y값을 가운데에 넣으면 y 축으로 회전을 해서 수평방향으로 움직이는걸로 보인다
        // transform.eulerAngles = new Vector3(-y*speed,x*speed,0);


        //way2
        //마우스의 움직인 양만큼만 움직이니 더해줘야된다
        transform.eulerAngles += new Vector3(-y * speed, x * speed, 0);
    }
}
