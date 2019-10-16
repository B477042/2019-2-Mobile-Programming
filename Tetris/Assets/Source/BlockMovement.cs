using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//블럭이 할 수 있는 모든 움직임만 표현
//낙하, 회전, 가속 낙하, 내리 꽂기
public class BlockMovement : MonoBehaviour
{
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void MoveDown()
    {
        transform.position+= Vector3.down*speed;
    }
    public void MoveLeft()
    {
        transform.position += Vector3.left * speed;
    }
    public void MoveRight()
    {
        transform.position += Vector3.right * speed;
    }

    //블럭을 아래로 내리꽂기
  public  void StraightDown()
    {
        
    }

   public  void Rotate()
    {
       transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
    }
    
}
