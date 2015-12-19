using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;
using TreeSharpPlus;
using RootMotion.FinalIK.Demos;
using UnityEngine.UI;

public class MainStory : MonoBehaviour
{

   
    public Transform[] locations;
    public GameObject[] numberOfParticipants;
    public GameObject[] zombies;
    public FullBodyBipedEffector[] eff;
    public InteractionObject[] obj;
    public InteractionObject[] objReset;
    public InteractionSystem[] interacts;
    public Transform[] searchPoints;
    public Camera[] cameras;


    private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent2;

    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {

    }

   
    

    protected Node Quote(string message)
    {
        return new LeafInvoke(() =>
        {
            numberOfParticipants[2].SetActive(true);
            GameObject.Find("quote").GetComponent<Text>().text = "\"" + message + "\"";
            //numberOfParticipants[2].SetActive(false);
        }, () => { });
    }

    protected Node ST_ApproachAndWait(GameObject daniel, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(
             daniel.GetComponent<BehaviorMecanim>().Node_GoTo(position),
            new LeafWait(1000));

    }

    protected Node LookAt(GameObject from, GameObject to)
    {
        BehaviorMecanim behavior = from.GetComponent<BehaviorMecanim>();
        return behavior.Node_OrientTowards(to.transform.position + new Vector3(0, 2, 0));
    }

    public Node ST_ApproachAndOrient(Transform target1, Transform player1pos,  Transform target2, Transform player2pos )
    {
        Val<Vector3> p1pos = Val.V(() => player1pos.position);
        Val<Vector3> p2pos = Val.V(() => player2pos.position);
        Val<Vector3> position1 = Val.V(() => target1.position);
        Val<Vector3> position2 = Val.V(() => target2.position);
        return new Sequence(
            new SequenceParallel(
            numberOfParticipants[0].GetComponent<BehaviorMecanim>().Node_GoTo(position1),
            numberOfParticipants[1].GetComponent<BehaviorMecanim>().Node_GoTo(position2)),
            new SequenceParallel(
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),
                numberOfParticipants[1].GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)));
    }

    protected Node ST_ShakeHands(InteractionObject handShake, Val<FullBodyBipedEffector> effector1, Val<FullBodyBipedEffector> effector2)
    {

        return new SequenceParallel(
            numberOfParticipants[0].GetComponent<BehaviorMecanim>().Node_StartInteraction(effector1, handShake),
            numberOfParticipants[1].GetComponent<BehaviorMecanim>().Node_StartInteraction(effector2, handShake),
            new LeafWait(1000));
    }

    protected Node Converse()
    {
        return
            new DecoratorPrintResult(
                new Sequence(
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 1000),
                numberOfParticipants[1].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADSHAKE", AnimationLayer.Face, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("BEINGCOCKY", AnimationLayer.Hand, 1000),
                numberOfParticipants[1].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADNOD", AnimationLayer.Face, 1000)));
    }

    protected Node SetCamera(int index)
    {
        return new LeafInvoke(() => {
            cameras[index - 1].gameObject.SetActive(false);
            cameras[index].gameObject.SetActive(true);
            return RunStatus.Success;
        },
        () => { });
    }

    protected Node ST_PushButton(Val<FullBodyBipedEffector> effector, Val<InteractionObject> obj, Transform objPos, int player1Index)
    {
        Val<Vector3> position = Val.V(() => objPos.position);
        //Val<Vector3> position1 = Val.V(() => orient.position);
        return new Sequence(
        numberOfParticipants[player1Index].GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position, 0.5f),
        //numberOfParticipants[player1Index].GetComponent<BehaviorMecanim>().Node_OrientTowards(position1),
        numberOfParticipants[player1Index].GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, obj),
        new LeafWait(2000));

        }
    protected Node BuildTreeRoot()
    {

        Sequence beginStory = new Sequence(


            this.ST_ApproachAndOrient(locations[0], numberOfParticipants[0].gameObject.transform, locations[1], numberOfParticipants[1].gameObject.transform),
            this.SetCamera(1),
            this.ST_ShakeHands(obj[0], eff[0], eff[0]),
            //this.ST_ShakeHands(numberOfParticipants[0].GetComponent<InteractionObject>(), eff[0], eff[0]),
            new SequenceParallel(
                new DecoratorLoop(5,
                this.Converse()),
                new Sequence(
                    this.Quote("Hey man, good to see you."),
                    new LeafWait(2000),
                    this.Quote("Isn't this a great party!"),
                    new LeafWait(2000),
                    this.Quote("Yeah, but lets see if we can kick it up a notch."),
                    new LeafWait(3000),
                    this.Quote("What do you mean?"),
                    new LeafWait(2000),
                    this.Quote("You heard the rumor about the abandoned shack over there right?"),
                    new LeafWait(3000),
                    this.Quote("No, tell me."),
                    new LeafWait(2000),
                    this.Quote("They say the owner was a deranged serial killer who would take his victims to that shack and do horrible things!"),
                    new LeafWait(4000),
                    this.Quote("When he was done he would store their bodies in that barn."),
                    new LeafWait(3000),
                    this.Quote("What that is crazy. I do not beleive it."),
                    new LeafWait(3000),
                    this.Quote("Well then I dare you go check out the barn."),
                    new LeafWait(3000),
                    this.Quote("All right let's do this!"),
                    new LeafWait(2000),
                    new LeafInvoke(() => {
                        numberOfParticipants[2].SetActive(false);
                    }, () => { })


        )));

        Sequence middleStory = new Sequence(
            this.SetCamera(2),
            this.ST_ApproachAndOrient(locations[2], numberOfParticipants[0].gameObject.transform, locations[3], numberOfParticipants[1].gameObject.transform),
            this.SetCamera(3),
             new SequenceParallel(
                new DecoratorLoop(2,
                this.Converse()),
                new Sequence(
                    this.Quote("Here it is."),
                    new LeafWait(2000),
                    this.Quote("Just open the door and see what is inside."),
                    new LeafWait(2000),
                    this.Quote("All right, but I am telling you it is nothing."),
                    new LeafWait(2000),
                     new LeafInvoke(() =>
                     {
                         numberOfParticipants[2].SetActive(false);
                     }, () => { })

            )),
             this.ST_PushButton(eff[0], obj[1], locations[4], 0));

        return new Sequence( middleStory);
    }
}
