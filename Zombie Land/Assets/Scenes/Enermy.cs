using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Enermy : MonoBehaviour
    {

        //List of Enermy Instance
        public static LinkedList<Enermy> LEnermies=new LinkedList<Enermy>();

        // Start is called before the first frame update
        void Start()
        {

            this.transform.position = SprinkleOnMap();
            ChangeColorToNormal();
            LEnermies.AddLast(this);
        
        //this.
    }

        // Update is called once per frame
        void Update()
        {

        }


        //Set Object's Location by Random X-Z value
        Vector3 SprinkleOnMap()
        {
            float NewPosX = Random.Range(-5.0f, 5.0f);
            float NewPosZ = Random.Range(-5.0f, 5.0f);
            return new Vector3(NewPosX, 0.0f, NewPosZ);
        }
        //return Vector3. Information about Location in 3D World
        Vector3 GetPosition() { return this.transform.position; }

       public  void ChangeColorToClose()
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }

      public  void ChangeColorToNormal()
        {
            this.GetComponent<Renderer>().material.color = Color.black;

        }




    }



