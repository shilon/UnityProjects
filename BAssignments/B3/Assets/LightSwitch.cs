using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

public class LightSwitch : MonoBehaviour {

    public GameObject lightbulb;
  
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnTriggerEnter()
    {
        Debug.Log("triggered switch");
        lightbulb.GetComponent<Light>().color = Color.green;
        //lightbulb.GetComponent<Light>().enabled = false;
    }
}
