﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//음악 노드
//하늘에서 내려오는 그것
public class MusicNode : MonoBehaviour
{
    protected List<Material> paths;
    // Start is called before the first frame update
    private void Awake()
    {
        paths = new List<Material>();
        for (int i = 0; i < 4; i++)
            paths.Add(Resources.Load("Path" + i) as Material);
    }
    void Start()
    {
        ColorMatching();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColorMatching()
    {
        gameObject.GetComponent<Renderer>().material = paths[0];
    }
    public Vector3 GetPos()
    {
        return transform.position;
    }
    public void PopNode()
    {
        Destroy(this);
    }

    public void Drop(Vector3 goal,float speed)
    {
        transform.position-=Vector3.MoveTowards(GetPos(), goal, speed);
    }
}
