using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
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
