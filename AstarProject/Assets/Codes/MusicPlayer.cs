using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private List<MusicNodeLine> NodeLines;
    public int Score { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        initNodeLines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
