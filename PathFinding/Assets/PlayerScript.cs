using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GridManager gm = null;//grid manager에 대한 정보를 가져온다. 
    Coroutine move_coroutine = null;
        
	// Use this for initialization
	void Start ()
    {
        gm = Camera.main.GetComponent<GridManager>() as GridManager;
        gm.BuildWorld(50, 50);
        //Camera에 있는 static 변수 main. 이것은 main camera
        //gm에 Main cam의 grid manager를 받아서 바로 사용하였다. 
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Wall" && hit.normal == Vector3.up) {
                    var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    wall.tag = "Wall";
                    wall.transform.position = hit.transform.position + Vector3.up;
                    return;
                }
                if (move_coroutine != null) StopCoroutine(move_coroutine);
                move_coroutine = StartCoroutine(gm.Move(gameObject, hit.point));                
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.transform.tag);
                //일반적으로 갈 수 있는 공간
                if (hit.transform.tag == "Plane")
                {
                    var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    //노란색칠. 벽을 만들고
                    wall.tag = "Wall";
                    wall.transform.position = gm.pos2center(hit.point);
                    gm.SetAsWall(wall.transform.position);
                }
            }
        }
    }    
}
