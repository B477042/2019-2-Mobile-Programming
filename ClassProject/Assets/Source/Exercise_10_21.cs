using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//만든날자 10월 21일
//10*10 공간에 높이가 1~10사이의 bar를 만든다
public class Exercise_10_21 : MonoBehaviour
{
    //3차원 배열을 선언하는 방법 => 1000개의 gameobject를 저장할 수 있는 공간을 만든다
    private GameObject[,,] bars=new GameObject[10,10,10];
    private float timer = 0.0f;
    private void bulidBars()
    {
        //확인하기 위해서 y축으로 하나만 만들어본다
        for(int x=0;x<10;x++)
        {
            for (int z = 0; z < 10; z++)
            {
                //int h = Random.Range(0, 10);
                for(int y =0;y<10;y++)
                {
                    //이미 만들어져있다면 없애준다
                    if(bars[x,y,z]!=null)
                    {
                        //game object를 삭제 시킨다
                        Destroy(bars[x, y, z]);
                        //null로 다시 초기화
                        bars[x, y, z] = null;
                    }


                }
                
            }
        }

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                int h = Random.Range(0, 10);
                for (int y = 0; y < h; y++)
                {
                    var bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bar.transform.position = new Vector3(x, y, z);
                    bars[x, y, z] = bar;


                }

            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        bulidBars();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=3.0f)
        {
            bulidBars();
            timer = 0.0f;
        }
    }
}
