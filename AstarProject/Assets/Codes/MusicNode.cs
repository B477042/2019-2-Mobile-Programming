using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//음악 노드
//하늘에서 내려오는 그것
public class MusicNode : MonoBehaviour
{
  private  Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor(Color newColor)
    {
        renderer.material.color = newColor;
    }
    public Vector3 GetPos()
    {
        return transform.position;
    }
    public void PopNode()
    {
        Destroy(this);
    }
    public void BeingPossed(MusicNodeController nodeController)
    {
        
    }
}
