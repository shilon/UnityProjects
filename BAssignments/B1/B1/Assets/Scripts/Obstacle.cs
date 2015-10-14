using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public GameObject[] obstacles;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
            }
            if (hit.transform.tag == "Obstacle")
            {
                obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
                for (int i =0; i < obstacles.Length; i++)
                {
                   // string obsVar = obstacles[i].GetComponent(ObstacleController);
                }
                Debug.Log("found obstacle");
            }
        }
	}

    
}
