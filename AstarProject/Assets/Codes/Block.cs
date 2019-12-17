using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    wall,
    plain
}
public class Block : MonoBehaviour
{
    //private Renderer renderer;
    private BlockType myType;
    private Material wall;
    private Material plain;
   protected List<Material> paths;
    private bool isChanged;
    private float timer;
    private const float max_timer=0.4f;
    private void Awake()
    {
         myType = BlockType.plain;
        wall = Resources.Load("Wall") as Material;
        plain = Resources.Load("Plain") as Material;
        paths = new List<Material>();
        for (int i = 1; i < 5; i++)
        {
            string assetName = ("Path" + i);
             paths.Add(Resources.Load(assetName) as Material);
            print("Name : " + assetName);
        }
        isChanged = false;
        timer = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();

        //EventManger.Instance.AddEvent(EventType.CheackA, ChangeToPathA);
        //EventManger.Instance.AddEvent(EventType.CheackS, ChangeToPathS);
        //EventManger.Instance.AddEvent(EventType.CheackD, ChangeToPathD);
        //EventManger.Instance.AddEvent(EventType.CheackF, ChangeToPathF);


        // ChangeToPlain();
        //ChangeToPath();
    }

    // Update is called once per frame
    void Update()
    {
       if(isChanged)
        {
            timer += Time.deltaTime;
            if(timer>=max_timer)
            {
                ChangeToPlain();
                isChanged = false;
                timer = 0.0f;
            }
        }
    }
    public Vector3 GetPos() { return transform.position; }
    public Quaternion GetRot() { return transform.rotation; }

    public void ChangeToPathA()
    {
        isChanged = true;
        gameObject.GetComponent<Renderer>().material = paths[0];
        //print("lets paittt");
    }
    public void ChangeToPathS()
    {
        isChanged = true;
        gameObject.GetComponent<Renderer>().material = paths[1];
        //print("lets paittt");
    }
    public void ChangeToPathD()
    {
        isChanged = true;
        gameObject.GetComponent<Renderer>().material = paths[2];
        //print("lets paittt");
    }
    public void ChangeToPathF()
    {
        isChanged = true;
        gameObject.GetComponent<Renderer>().material = paths[3];
        //print("lets paittt");
    }
    public void ChangeToWall()
    {
        myType = BlockType.wall;
        gameObject.GetComponent<Renderer>().material= wall;
    }
    public void ChangeToPlain()
    {
        isChanged = true;
        myType = BlockType.plain;
        gameObject.GetComponent<Renderer>().material = plain;
        //print("paitn!");
    }
    public bool IsWall()
    {
        if (myType == BlockType.wall) return true;
        else return false;
    }
    
}
