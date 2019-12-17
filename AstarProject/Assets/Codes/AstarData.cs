using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarData : MonoBehaviour
{
    public List<int> WallList;
    public List<int> DestnationPos;
    private static AstarData instance = null;
    public static AstarData Instance { get { return instance; } }
   
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
        WallList =new List<int>{ 21,23,98,41,34,33,52,59,64,13,77,72,85,87,82,66,68,61,27,11,46};
        WallList.Sort();
        DestnationPos = new List<int> { };
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
