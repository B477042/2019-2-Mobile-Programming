using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
   private static GameManger instance = null;
    public static GameManger Instance
    {
        get
        {
            return instance;
        }
    }

    
   
    private void Awake()
    {
        if(instance!=null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
       // Debug.LogWarning("Game manger instance Called");

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnermy();
    }



    void FindNearestEnermy()
    {
        //Set Index Node to Point Enermies(Linked List First Node)
        LinkedListNode<Enermy> indexNode = Enermy.LEnermies.First;
        LinkedListNode<Enermy> closeNode = null;
        float minDistance = 0.0f;

       
        while(indexNode!=null)
        {

            if(closeNode==null)
            {
                indexNode.Value.ChangeColorToNormal();
                minDistance =Vector3.Distance(Player.GetPlayer().transform.position, indexNode.Value.transform.position);
                Debug.Log(minDistance);
                closeNode = indexNode;
                indexNode = indexNode.Next;
            }
            else
            {
                float tempDistance=Vector3.Distance(Player.GetPlayer().transform.position, indexNode.Value.transform.position);
                if(tempDistance<minDistance)
                {
                    closeNode.Value.ChangeColorToNormal();
                    minDistance = tempDistance;
                    Debug.Log(minDistance);
                    closeNode = indexNode;
                    indexNode = indexNode.Next;
                }
            }

        }
        closeNode.Value.ChangeColorToClose();


        ////find N Node. If Node index is null, break loop
        //while(indexNode!=null)
        //{
        //    //if linkedNode is First Node of List
        //    if(indexNode==Enermy.LEnermies.First)
        //    {
        //        indexNode.Value.ChangeColorToNormal();
        //        // minDistance = Player.GetPlayer().EstimateDistance(Player.GetPlayer(), indexNode.Value);
        //        minDistance = Vector3.Distance(Player.GetPlayer().transform.position, indexNode.Value.transform.position);
        //        Debug.Log("First Distanc is  "+ minDistance);
        //        //Save Close Enermy Node
        //        closeNode = indexNode;
        //        //move Node Index To Next Node       
        //        indexNode = indexNode.Next;
        //        continue;
        //    }
        //    //Calculate Distance
        //    // float tempDistance = Player.GetPlayer().EstimateDistance(Player.GetPlayer(), indexNode.Value);
        //  float   tempDistance = Vector3.Distance(Player.GetPlayer().transform.position, indexNode.Value.transform.position);
        //    //
        //    if (tempDistance < minDistance)
        //    {
                
        //        minDistance = tempDistance;
        //        //Change Color to Normal. 
        //        closeNode.Value.ChangeColorToNormal();
        //        //Save Close Enermy Node
        //        closeNode = indexNode;
        //    }

        //    //move Node Index To Next Node
        //    indexNode = indexNode.Next;
            
        //}

        
    //    closeNode.Value.ChangeColorToClose();
    }

    void InitializeEnermyColor()
    {

    }
}
