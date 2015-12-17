using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;
using TreeSharpPlus;

public class PartyBehavior : MonoBehaviour {

    public Transform[] locations;
    public GameObject[] numberOfParticipants;
    public FullBodyBipedEffector[] eff;
    public InteractionObject[] obj;
    public InteractionObject[] objReset;
    //public InteractionObject objects;
    public InteractionSystem[] interacts;
    public Transform[] searchPoints;


    private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent2;
    // Use this for initialization
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        behaviorAgent2 = new BehaviorAgent(this.event2());
        BehaviorManager.Instance.Register(behaviorAgent);
        BehaviorManager.Instance.Register(behaviorAgent2);
        //behaviorAgent.StartBehavior();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            behaviorAgent.StartBehavior();

        }
        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            behaviorAgent2.StartBehavior();

        }
    }

    public Node ST_ApproachAndOrient(Transform target1, Transform player1pos, int player1Index, Transform target2, Transform player2pos, int player2Index)
    {
        Val<Vector3> p1pos = Val.V(() => player1pos.position);
        Val<Vector3> p2pos = Val.V(() => player2pos.position);
        Val<Vector3> position1 = Val.V(() => target1.position);
        Val<Vector3> position2 = Val.V(() => target2.position);
        return new Sequence(
            new SequenceParallel(
            numberOfParticipants[player1Index].GetComponent<BehaviorMecanim>().Node_GoTo(position1),
            numberOfParticipants[player2Index].GetComponent<BehaviorMecanim>().Node_GoTo(position2)),
            new SequenceParallel(
                numberOfParticipants[player1Index].GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),
                numberOfParticipants[player2Index].GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)));
    }

    protected Node ST_pickUpAndEatFood(Val<FullBodyBipedEffector> effector, Val<InteractionObject> obj, Transform objPos, GameObject daniel, Val<InteractionObject> objReset, Transform orientPos)
    {
        Val<Vector3> orient = Val.V(() => orientPos.position);
        Val<Vector3> position = Val.V(() => objPos.position);
        return new Sequence(
            new DecoratorForceStatus(RunStatus.Success,
        daniel.GetComponent<BehaviorMecanim>().Node_GoTo(position)),
             new DecoratorForceStatus(RunStatus.Success,
        daniel.GetComponent<BehaviorMecanim>().Node_OrientTowards(orient)),
        daniel.GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, obj),
        new LeafWait(2000),
        daniel.GetComponent<BehaviorMecanim>().ST_PlayGesture("EAT", AnimationLayer.Face, 1000),
        new LeafWait(2000),
        daniel.GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, objReset),
        new LeafWait(2000));


    }

    protected Node ST_Gesture(GameObject daniel, int loop)
    {
        //Val<Vector3> position = Val.V(() => target.position);
        return new DecoratorLoop(loop,
            new Sequence(
                daniel.GetComponent<BehaviorMecanim>().ST_PlayGesture("CHEER", AnimationLayer.Hand, 1000),
                new LeafWait(1000)));
    }

    void reshuffle(Transform[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            Transform tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    protected Node ST_ApproachAndWait(GameObject daniel, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(
             daniel.GetComponent<BehaviorMecanim>().Node_GoTo(position),
            new LeafWait(1000));

    }

    protected Node BuildTreeRoot()
    {
        ForEach<GameObject> middleStory = new ForEach<GameObject>((daniel) =>
        {
            
            return new DecoratorLoop(
                
                    new SequenceShuffle(

                        this.ST_pickUpAndEatFood(eff[0], obj[0], locations[0], daniel, objReset[0], locations[1]),//apple
                        this.ST_pickUpAndEatFood(eff[0], obj[1], locations[2], daniel, objReset[1], locations[1]),//orange
                        this.ST_pickUpAndEatFood(eff[1], obj[2], locations[3], daniel, objReset[2], locations[4]),//apple
                        this.ST_ApproachAndWait(daniel, locations[9]),
                       // this.ST_ApproachAndWait(daniel, locations[10]),
                       // this.ST_ApproachAndWait(daniel, locations[11]),
                       // this.ST_ApproachAndWait(daniel, locations[12]),
                       // this.ST_ApproachAndWait(daniel, locations[13]),
                       // this.ST_ApproachAndWait(daniel, locations[14]),
                           this.ST_Gesture(daniel, 6)));
                       

            }, numberOfParticipants);
        return new Sequence(middleStory);


    }

    void OnTriggerEnter()
    {
        Debug.Log("triggered switch");
       // behaviorAgent.StopBehavior();
       // behaviorAgent2.StartBehavior();
    }

    protected Node event2()
    {
        ForEach<GameObject> converse = new ForEach<GameObject>((daniel) =>
        {
            return
            new DecoratorPrintResult(
                new Sequence(
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADSHAKE", AnimationLayer.Face, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("BEINGCOCKY", AnimationLayer.Hand, 1000),
                numberOfParticipants[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADNOD", AnimationLayer.Face, 1000)));
        }, numberOfParticipants);
        return new Sequence(converse);
    }

    }


