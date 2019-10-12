using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h=0.0f;//Height
    private float w=0.0f;//Width
    private float speed = 10.0f;
  
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputProcess();
    }
    public void InputProcess()
    {
        h=  Input.GetAxis("Vertical");
        w = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * w, Space.World);

        transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime * h, Space.World);



   
    }
  
}
