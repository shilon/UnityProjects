using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using System;
using RootMotion.FinalIK;

public class IdleBehavior : MonoBehaviour {

    public GameObject participant;
    public Transform lightSwitch;
    public GameObject lamp;
    //public FullBodyBipedIK ik;
    public FullBodyBipedEffector eff;
    public InteractionObject obj;

    private BehaviorAgent behaviorAgent;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    protected Node TurnOnLight(Val<FullBodyBipedEffector> effector, Val<InteractionObject> obj, Transform lightSwitch)
    {
        Val<Vector3> position = Val.V(() => lightSwitch.position);
        return new Sequence(
            participant.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position, 2.0f),
            participant.GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, obj));
           // participant.GetComponent<BehaviorMecanim>().Node_WaitForFinish(effector),
           // participant.GetComponent<BehaviorMecanim>().Node_StopInteraction(effector));

    }

    protected Node ST_Gesture()
    {
        //Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().ST_PlayGesture("BREAKDANCE", AnimationLayer.Body, 1000));
    }

    protected Node BuildTreeRoot()
    {
        // Val<FullBodyBipedEffector> effector = Val.V(() => ik.solver.rightHandEffector);
        return
                // new DecoratorLoop(
                new Sequence(
                    new DecoratorLoop(5, 
                    this.ST_Gesture()));
                    //this.TurnOnLight(eff, obj, lightSwitch));
    }

	
	
}
