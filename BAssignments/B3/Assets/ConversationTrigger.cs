using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;
using TreeSharpPlus;

public class ConversationTrigger : MonoBehaviour {

    private BehaviorAgent behaviorAgent;
    public Transform[] locations;
    public GameObject[] numberOfParticipants;

    // Use this for initialization
    void Start () {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
       

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            behaviorAgent.StartBehavior();

        }
    }

     

void OnTriggerEnter()
    {
        Debug.Log("triggered switch");
       // behaviorAgent.StartBehavior();
    }

    protected Node BuildTreeRoot()
    {
        return
            new DecoratorPrintResult(
                new Sequence(
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADSHAKE", AnimationLayer.Face, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("BEINGCOCKY", AnimationLayer.Hand, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADNOD", AnimationLayer.Face, 1000)));
    }
    }
