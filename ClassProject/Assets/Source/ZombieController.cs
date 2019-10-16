using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private float speed = 0.06f;

    private GameObject player;
    private ZombieSpawner zombieSpawner;


    void AWake()
    {
       

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        zombieSpawner = GameObject.Find("Main Camera").GetComponent<ZombieSpawner>();
        //위와 같은 코드
        var mainCamera = GameObject.Find("Main Camera");
        if(mainCamera)
        {
            zombieSpawner=mainCamera.GetComponent<ZombieSpawner>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        //현재 위치로부터 어느 위치로 움직일 것인가의 값을 반환해준다
       transform.position= Vector3.MoveTowards(transform.position, player.transform.position, speed);

        //zombie-player간의 거리를 측정
        float dist= Vector3.Distance(transform.position, player.transform.position);
        if(dist<1.0f)
        {
            //내 게임 객체를 죽여줘라.이 컴포넌트를 가진 obj를 죽여라. 이 경우 문제는 spawner의 nZombies에 접근할 수 없다
            //Destroy(this.gameObject);
            zombieSpawner.destroyMe(this.gameObject);
        }
    }

    


}
