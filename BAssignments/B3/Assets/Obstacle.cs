using UnityEngine;
using System.Collections;
using System;

public class Obstacle : MonoBehaviour {

    public GameObject[] obstacles;
    private ObstacleController obstacleController;
    public Material notSelected;
    public Material selected;
	// Use this for initialization
	void Start () {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
               // Debug.DrawLine(ray.origin, hit.point);
            }

            if (hit.transform.tag == "Obstacle")
            {
                obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                for (int i = 0; i < obstacles.Length; i++)
                {
                    var obsVar = obstacles[i].GetComponent<ObstacleController>();
                    obsVar.selected = false;
                    obsVar.rend.material.color = notSelected.color;
                    //obsVar.material.color = new Color(0.5F, 0.5F, 0.9F, 1.0F); //grey
                }
                var obsVars = hit.transform.gameObject.GetComponent<ObstacleController>();
                obsVars.selected = true;
                obsVars.rend.material.color = selected.color;
                //obsVars.material.color = new Color(1.0F, 0.92F, 0.016F, 1.0F);//yellow
                // Debug.Log("found obstacle");
            }
        }
    }


}
