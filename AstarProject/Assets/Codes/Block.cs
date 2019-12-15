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
    private Renderer renderer;
    private BlockType myType;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        ChangeColor(Color.white);
        myType = BlockType.plain;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public Vector3 GetPos() { return transform.position; }
    public Quaternion GetRot() { return transform.rotation; }
    public void ChangeColor(Color newColor)
    {
        renderer.material.color = newColor;
    }
    public void ChangeToWall()
    {
        myType = BlockType.wall;
    }
    public void ChangeToPlain()
    {
        myType = BlockType.plain;
    }
}
