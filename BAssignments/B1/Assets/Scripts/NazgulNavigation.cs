using UnityEngine;
using System.Collections;

public class NazgulNavigation : MonoBehaviour {

    NavMeshAgent nazgul;
    NavMeshObstacle Nazgul;
    //public bool agentSelected;
    public Renderer Rend;
    public Material nazgulNotSelected;
    public Material selectedNazgul;
    private bool moving;
    

    public bool isSelected;

    private void OnSelected()
    {
        isSelected = true;
        GetComponent<Renderer>().material.color = selectedNazgul.color;
       
    }

    private void OnUnselected()
    {
        isSelected = false;
        GetComponent<Renderer>().material.color = nazgulNotSelected.color;
        
    }
    // Use this for initialization
    void Start()
    {
        // agentSelected = false;
        nazgul = GetComponent<NavMeshAgent>();
        Rend = GetComponent<Renderer>();
        Rend.enabled = true;
        moving = false;
        nazgul.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        float velocity = nazgul.velocity.magnitude;
        if (isSelected == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                 
                }
            }
        }
    }
}