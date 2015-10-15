using UnityEngine;
using System.Collections;

public class AgentNavigation : MonoBehaviour {

    NavMeshAgent agent;
    public bool agentSelected;
    public Renderer Rend;

    // Use this for initialization
    void Start () {
        agentSelected = false;
        agent = GetComponent<NavMeshAgent>();
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agentSelected == true)
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
