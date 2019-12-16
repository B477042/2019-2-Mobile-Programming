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
    private void Awake()
    {
         myType = BlockType.plain;
        wall = Resources.Load("Wall") as Material;
        plain = Resources.Load("Plain") as Material;
        paths = new List<Material>();
        for (int i = 0; i < 4; i++)
        {
            string assetName = ("Path" + i);
             paths.Add(Resources.Load(assetName) as Material);
            print("Name : " + assetName);
        }
           
    }
    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();
       
       

        
       // ChangeToPlain();
        //ChangeToPath();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public Vector3 GetPos() { return transform.position; }
    public Quaternion GetRot() { return transform.rotation; }
    public void ChangeToPath()
    {
        gameObject.GetComponent<Renderer>().material = paths[1];
        print("lets paittt");
    }
    public void ChangeToWall()
    {
        myType = BlockType.wall;
        gameObject.GetComponent<Renderer>().material= wall;
    }
    public void ChangeToPlain()
    {
        myType = BlockType.plain;
        gameObject.GetComponent<Renderer>().material = plain;
        print("paitn!");
    }
    public bool IsWall()
    {
        if (myType == BlockType.wall) return true;
        else return false;
    }
    
}
