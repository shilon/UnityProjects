using UnityEngine;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class LocomotionSimpleAgent : MonoBehaviour {
	Animator anim;
	NavMeshAgent agent;
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;
    private Time time;
    private bool _traversingLink;
    private OffMeshLinkData _currLink;
    //Animation animate;

    void Start ()
	{
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
        //animate = GetComponent<Animation>();
		agent.updatePosition = false;
        agent.autoTraverseOffMeshLink = false;
    }

    void Update()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if delta time is safe
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("move", shouldMove);
        anim.SetFloat("velx", velocity.x);
        anim.SetFloat("vely", velocity.y);



        //		LookAt lookAt = GetComponent<LookAt> ();
        //		if (lookAt)
        //			lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

        // Pull character towards agent
        if (worldDeltaPosition.magnitude > agent.radius)
            transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;

        		// Pull agent towards character
        		if (worldDeltaPosition.magnitude > agent.radius)
        			agent.nextPosition = transform.position + 0.9f*worldDeltaPosition;
        

        // when the animation is stopped, we've reached the other side. Don't use looping animations with this control setup
        if (agent.isOnOffMeshLink == true)
        {
            Debug.Log("ON mesh link");
            if (anim.GetBool("JumpUpHigh") == true)
            {
                //Debug.Log("Jump");

                anim.Play("Idle_ToJumpUpHigh", -1);
                //yield return new WaitForSeconds(anim.HasState(0, clip.length);
                if (anim.IsInTransition(0))
                {
                    transform.position = _currLink.endPos;
                    agent.CompleteOffMeshLink();
                    agent.Resume();
                }
            }
            if (anim.GetBool("JumpDownFlip") == true)
            {
               // Debug.Log("Jump Down");

                anim.Play("Idle_JumpDownHigh_Idle", -1);
                //yield return new WaitForSeconds(anim.HasState(0, clip.length);
                if (anim.IsInTransition(0))
                {
                   
                    //transform.position = _currLink.endPos;
                    agent.CompleteOffMeshLink();
                    anim.SetBool("JumpDownFlip", false);
                    agent.Resume();
                }
            }
        }
    
    }
    

	void OnAnimatorMove ()
	{
		// Update postion to agent position
		transform.position = agent.nextPosition;

//		// Update position based on animation movement using navigation surface height
		Vector3 position = anim.rootPosition;
		position.y = agent.nextPosition.y;
		transform.position = position;
	}
    void OnTriggerEnter(Collider col)
    {
       // Debug.Log("found trigger");
        if (col.gameObject.tag == "JumpUpTrigger")
        {
            anim.SetBool("JumpUpHigh", true);
        }
        if (col.gameObject.tag == "EndJumpUpTrigger")
        {
            anim.SetBool("JumpUpHigh", false);
        }
        if (col.gameObject.tag == "JumpDownFlip")
        {
            anim.SetBool("JumpDownFlip", true);
        }
        if (col.gameObject.tag == "EndJumpDownFlip")
        {
            anim.SetBool("JumpDownFlip", false);
        }
    }

}
