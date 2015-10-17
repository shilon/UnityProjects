using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{

    public float speed;
    public bool selected;
    //public Material material;
    public Renderer rend;
    private Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        selected = false;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
       rend.material.color = new Color(0.5F, 0.5F, 0.5F, 1F);
    }

    void FixedUpdate()
    {
        if (selected == true) { 

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            //rb.AddForce(movement * speed);

            if (Mathf.Abs(moveHorizontal) > 0.0)
            {
                transform.position += Vector3.back * moveHorizontal * speed;
            }
            if (Mathf.Abs(moveVertical) > 0.0)
            {
                transform.position += Vector3.right * moveVertical * speed;
            }
    }
    }
}
