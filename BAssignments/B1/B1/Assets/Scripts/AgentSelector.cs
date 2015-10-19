using UnityEngine;
using System.Collections;

public class AgentSelector : MonoBehaviour
{

    public GameObject[] agents;
    public GameObject[] agentGroup;
    private AgentNavigation agentSelector;
    public Material AgentnotSelected;
    public Material selectedAgent;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Debug.DrawLine(ray.origin, hit.point);
            }
            if (hit.transform.tag == "Agent")
            {
                agents = GameObject.FindGameObjectsWithTag("Agent");
                for (int i = 0; i < agents.Length; i++)
                {
                    var obsVar = agents[i].GetComponent<AgentNavigation>();
                    obsVar.isSelected = false;
                    //obsVar.Rend.material.color = AgentnotSelected.color;
                    //obsVar.material.color = new Color(0.5F, 0.5F, 0.9F, 1.0F); //grey
                }
              
                
                var obsVars = hit.transform.gameObject.GetComponent<AgentNavigation>();
             
                obsVars.isSelected = true;
               // obsVars.Rend.material.color = selectedAgent.color;
               

                //obsVars.material.color = new Color(1.0F, 0.92F, 0.016F, 1.0F);//yellow
                // Debug.Log("found obstacle");
            }
        }
    }
}
