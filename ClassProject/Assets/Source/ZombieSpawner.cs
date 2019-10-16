using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

   static private int nZombies = 0;
    //Spawn Zombie by 3 secs
    private float timer = 0.0f;
    [SerializeField ]
    private GameObject zombiePrefab=null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=0.5f)
        {
            spawn();
            timer = 0.0f;
        }
    }

    private void spawn()
    {
        if (nZombies >= 4) return;
        //stack에 올라감. value type
        Vector3 pos = new Vector3(Random.Range(-10.0f,10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
        Instantiate(zombiePrefab, pos, Quaternion.identity);
        nZombies++;

        
    }
    public void destroyMe(GameObject obj)
    {
        Destroy(obj);
        nZombies--;
    }
}
