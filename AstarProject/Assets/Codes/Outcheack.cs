using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcheack : MonoBehaviour
{
    private BoxCollider collision;
    private void Awake()
    {
        collision = gameObject.GetComponent<BoxCollider>();
        collision.isTrigger = true;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter(Collider other)
    {
        print("OUTTTT!!");
        EventManger.Instance.Broadcast(EventType.Damaged);
        Destroy(other.gameObject);
        //if (other.tag == "MusicNode")
        //{
        //    if (other.GetComponent<MusicNode>())
        //        other.GetComponent<MusicNode>().PopNode();
        //    EventManger.Instance.Broadcast(EventType.Damaged);
        //}
    }
}
