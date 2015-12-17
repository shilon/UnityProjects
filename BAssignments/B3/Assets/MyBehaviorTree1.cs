using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree1 : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;
	public Transform wander3;
	public GameObject participant;

	private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent2;
    private BehaviorEvent event1;
    private Token token;
    public GameObject[] daniels;
    // Use this for initialization
    void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
        behaviorAgent2 = new BehaviorAgent(this.event2());
        BehaviorManager.Instance.Register (behaviorAgent);
        //event1 = new BehaviorEvent( daniels);

    }
	
	// Update is called once per frame
	void Update ()
	{
	    
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
           // behaviorAgent2.StartBehavior();
            
        }
        if (Input.GetKeyDown(KeyCode.T) == true)
        {
            //behaviorAgent.FinishEvent();
            behaviorAgent2.StopBehavior();

        }
        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            Debug.Log( behaviorAgent.Status);
            Debug.Log (behaviorAgent.CurrentEvent);
        }
    }

	protected Node ST_ApproachAndWait(Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}

	protected Node BuildTreeRoot()
	{
		return
			new DecoratorLoop(
                new DecoratorForceStatus(RunStatus.Success,
				    new Sequence(
					    this.ST_ApproachAndWait(this.wander1),
					    this.ST_ApproachAndWait(this.wander2),
					    this.ST_ApproachAndWait(this.wander3))));
	    }

    protected Node event2()
    {
        ForEach<GameObject> middleStory = new ForEach<GameObject>((daniel) =>
        {
            return
           new DecoratorLoop(
               new DecoratorForceStatus(RunStatus.Success,
                   new SequenceShuffle(
                       this.ST_ApproachAndWait(this.wander1),
                       this.ST_ApproachAndWait(this.wander2),
                       this.ST_ApproachAndWait(this.wander3))));

        }, daniels);
        return new Sequence(middleStory);
    }
   
}
