using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ZombieState
{
    Idle,
    Wandering,
    Tracing,
    Attacking
}

public class ZombieFSM : MonoBehaviour
{
    private ZombieState state = ZombieState.Idle;
    private Renderer renderer;
    private GameObject player;
    [SerializeField]
    float speed = 0.1f;

   private const float visibleRange = 10.0f;
    private const float attackRange = 5.0f;
    private float timer = 0.0f;
    private float nextActionTimer = 2.0f;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        player = GameObject.Find("/Player");//root부터 찾기
    }
    float calcDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
;    }

    

    // Update is called once per frame
    void Update()
    {
        float dist = calcDistanceToPlayer();
        //timer += Time.deltaTime;
        //if (timer < nextActionTimer) return;
        
        switch(state)
        {
            case ZombieState.Idle:
                timer += Time.deltaTime;
                renderer.material.color=Color.white;
                if(timer>3.0f)
                {
                    timer = 0.0f;
                    state = ZombieState.Wandering;
                    targetPos = transform.position;
                }
                break;
            case ZombieState.Wandering:
                timer += Time.deltaTime;
                
                
                if(timer>7.0f)
                {
                    targetPos = transform.position;
                    timer = 0.0f;
                    state = ZombieState.Idle;
                    break;

                }
                if(transform.position==targetPos)
                {
                    targetPos += new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f));

                }

                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
                renderer.material.color = Color.green;

                break;
            case ZombieState.Tracing:
                targetPos = player.transform.position;
              transform.position+=  Vector3.MoveTowards(transform.position, targetPos, 0.1f);
                renderer.material.color = Color.yellow;
                break;
            case ZombieState.Attacking:
               
                renderer.material.color = Color.red;
                break;
        }

        if (dist < visibleRange)
        {
            if (dist < attackRange)
                state = ZombieState.Attacking;
            else
                state = ZombieState.Tracing;
        }
        else
        {
            switch (state)
            {
                case ZombieState.Idle:
                case ZombieState.Wandering:
                    break;
                case ZombieState.Attacking:
                case ZombieState.Tracing:
                    state = ZombieState.Idle;
                    break;
            }
        }

    }

}
