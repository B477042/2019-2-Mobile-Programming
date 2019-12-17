using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInput : MonoBehaviour
{
    private static MusicInput instance = null;
    public static MusicInput Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

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

    private void InputProcess()
    {
        if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                EventManger.Instance.Broadcast(EventType.CheackA);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                EventManger.Instance.Broadcast(EventType.CheackS);
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                EventManger.Instance.Broadcast(EventType.CheackD);
            }
            else if(Input.GetKeyDown(KeyCode.F))
            {
                EventManger.Instance.Broadcast(EventType.CheackF);
            }
            else if(Input.GetKeyDown(KeyCode.O))
            {
                MusicNodeLine.IncreaseSpeed();
            }
            else if(Input.GetKeyDown(KeyCode.P))
            {
                MusicNodeLine.DecreaseSpeed();
            }
        }
    }
}
