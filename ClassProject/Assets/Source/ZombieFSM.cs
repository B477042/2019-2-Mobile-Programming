using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ZombieState
{
    Idle,
    Wondering,
    Tracking,
    Attacking
}

public class ZombieFSM : MonoBehaviour
{
    private ZombieState state = ZombieState.Idle;
    private Renderer renderer;
    private GameObject player;

   private const float visibleRange = 10.0f;
    private const float attackRange = 5.0f;
    private float timer = 0.0f;
    private float nextActionTimer = 3.0f;
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
        timer += Time.deltaTime;
        if (timer < nextActionTimer) return;
        switch(state)
        {
            case ZombieState.Idle:
                
                renderer.material.color=Color.white;
                break;
            case ZombieState.Wondering:
              
                renderer.material.color = Color.green;
                break;
            case ZombieState.Tracking:
              
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
                state = ZombieState.Tracking;
        }
        else
            state = ZombieState.Idle;

        timer = 0.0f;
    }
    
}
