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
            if (instance == null)
                instance = new GameManger();
            return instance;
        }
    }

    
    private GameManger()
    {
       
    }

    private void Awake()
    {
        
       
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        FindCloseEnermy();
    }



    void FindCloseEnermy()
    {
        //Set Index Node to Point Enermies(Linked List First Node)
        LinkedListNode<Enermy> indexNode = Enermy.LEnermies.First;
        LinkedListNode<Enermy> closeNode = null;
        float minDistance = 0.0f;

       

        //find Close Node. If Node index is null, break loop
        while(indexNode!=null)
        {
            //if linkedNode is First Node of List
            if(indexNode==Enermy.LEnermies.First)
            {
                minDistance = Player.GetPlayer().EstimateDistance(Player.GetPlayer(), indexNode.Value);
                Debug.Log("First Distanc is  "+ minDistance);
                //Save Close Enermy Node
                closeNode = indexNode;
                //move Node Index To Next Node       
                indexNode = indexNode.Next;
            }
            //Calculate Distance
            float tempDistance = Player.GetPlayer().EstimateDistance(Player.GetPlayer(), indexNode.Value);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                //Save Close Enermy Node
                closeNode = indexNode;
            }

            //move Node Index To Next Node
            indexNode = indexNode.Next;
            
        }

        closeNode.Value.ChangeColorToClose();
    }

    void InitializeEnermyColor()
    {

    }
}
