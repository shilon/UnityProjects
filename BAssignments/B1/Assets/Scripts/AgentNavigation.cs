using UnityEngine;
using System.Collections;

public class AgentNavigation : MonoBehaviour {

    NavMeshAgent agent;
    //public bool agentSelected;
    public Renderer Rend;
    public Material AgentnotSelected;
    public Material selectedAgent;
    public Camera cam;

    public bool isSelected;

    private void OnSelected()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = selectedAgent.color;
        cam.enabled = true;
    }

    private void OnUnselected()
    {
        isSelected = false;
        GetComponent<Renderer>().material.color = AgentnotSelected.color;
        cam.enabled = false;
    }
    // Use this for initialization
    void Start () {
       // agentSelected = false;
        agent = GetComponent<NavMeshAgent>();
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                }
            }
        }
    }
}
