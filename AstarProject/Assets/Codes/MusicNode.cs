using System.Collections;
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
        //path의 색상을 로드
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
    //path 타입에 맞게 생상을 바꿀 것
    public void ColorMatching()
    {
        gameObject.GetComponent<Renderer>().material = paths[0];
    }
    public Vector3 GetPos()
    {
        return gameObject.transform.position;
    }
    public void PopNode()
    {
      
        Destroy(gameObject);
    }

    public void Drop(float speed)
    {
        
        gameObject.transform.position+=Vector3.down*speed;
    }
}
