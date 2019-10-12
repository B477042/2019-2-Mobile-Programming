using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour
{
    private void Awake()
    {
        print("Awaked");
    }

    // Start is called before the first frame update
    void Start()
    {
        //debug class에 static Log함수에 접근한 것
        Debug.Log("My Controller Started");
        //MonoBehavior의 member 함수. 내부적으로 Debug함수를 호출한다. 
        //MonoBehavior를 상속하지 않은 경우 Debug를 써야된다
        print("My Controller Started");

        //이 스크립트가 부착된 오브젝트를 찾을 땐 이렇게 안 찾아도 되는데 해보는거
        var cube = GameObject.Find("/Cube");
        //yourSphere에 mycube를 정확히 찾아준다
       // var cube = GameObject.Find("/YourSphere/MyCube");
        if (cube == null)
        {
            print("Can't find Cube");
        }
        else
        {
            print("name : " + cube.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
