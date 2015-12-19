using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {


    public GameObject door;

    private Vector3 defaultRot;
    private Vector3 openRot;

    private bool open;
    // Use this for initialization
    void Start () {
        open = false;
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + 90 , defaultRot.z );
    }
	
	// Update is called once per frame
	void Update () {
	    if (open == true)
        {
            door.transform.Rotate(door.transform.rotation.x, door.transform.rotation.y + 90, door.transform.rotation.z);
            //door.transform.eulerAngles = Vector3.Slerp(openRot,  transform.eulerAngles,  Time.deltaTime * 2.0f);
            open = false;
        }
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Right Hand")
        {
            //Debug.Log("button pushed");
            //door.GetComponent<Animation>().Play();

            open = true;
        }
    }
}
