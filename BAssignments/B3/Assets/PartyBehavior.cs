using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;
using TreeSharpPlus;

public class PartyBehavior : MonoBehaviour {

    public Transform[] locations;
    public GameObject[] wanderers;
    public GameObject[] eaters;
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
           // behaviorAgent2.StartBehavior();

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
            wanderers[player1Index].GetComponent<BehaviorMecanim>().Node_GoTo(position1),
            wanderers[player2Index].GetComponent<BehaviorMecanim>().Node_GoTo(position2)),
            new SequenceParallel(
                wanderers[player1Index].GetComponent<BehaviorMecanim>().Node_OrientTowards(p2pos),
                wanderers[player2Index].GetComponent<BehaviorMecanim>().Node_OrientTowards(p1pos)));
    }

    protected Node ST_pickUpAndEatFood(Val<FullBodyBipedEffector> effector, Val<InteractionObject> obj, Transform objPos, int player1Index, Val<InteractionObject> objReset, Transform orient)
    {
        Val<Vector3> position = Val.V(() => objPos.position);
        Val<Vector3> position1 = Val.V(() => orient.position);
        return new Sequence(
        eaters[player1Index].GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position, 0.5f),
        eaters[player1Index].GetComponent<BehaviorMecanim>().Node_OrientTowards(position1),
        eaters[player1Index].GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, obj),
        new LeafWait(2000),
        new DecoratorLoop(3,
        eaters[player1Index].GetComponent<BehaviorMecanim>().ST_PlayGesture("EAT", AnimationLayer.Face, 3000)),
        new LeafWait(3000),
        eaters[player1Index].GetComponent<BehaviorMecanim>().Node_StartInteraction(effector, objReset),
        new LeafWait(3000),
        this.ST_Gesture(eaters[player1Index], 2));


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
             daniel.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position, 1.0f),
            new LeafWait(1000));

    }

    protected Node BuildTreeRoot()
    {

        ForEach<GameObject> dance = new ForEach<GameObject>((daniel) =>
        {
            return new DecoratorLoop(this.ST_Gesture(daniel, 2));
        }, eaters);

        Sequence Eating = new Sequence(
            new DecoratorLoop(
             new SelectorParallel(
                this.ST_pickUpAndEatFood(eff[1], obj[0], locations[0], 0, objReset[0], locations[1]),
                this.ST_pickUpAndEatFood(eff[1], obj[1], locations[2], 1, objReset[1], locations[1]),
                this.ST_pickUpAndEatFood(eff[1], obj[2], locations[3], 2, objReset[2], locations[4]),
                this.ST_pickUpAndEatFood(eff[1], obj[3], locations[5], 3, objReset[3], locations[4]),
                this.ST_pickUpAndEatFood(eff[1], obj[4], locations[6], 4, objReset[4], locations[7]),
                this.ST_pickUpAndEatFood(eff[1], obj[5], locations[8], 5, objReset[5], locations[7])

            )));


        ForEach<GameObject> middleStory = new ForEach<GameObject>((daniel) =>
        {
           
            return new DecoratorLoop(
                
                    new SequenceShuffle(

                        this.ST_ApproachAndWait(daniel, locations[9]),
                        this.ST_ApproachAndWait(daniel, locations[10]),
                        this.ST_ApproachAndWait(daniel, locations[13]),
                        this.ST_ApproachAndWait(daniel, locations[14]),
                        this.ST_ApproachAndWait(daniel, locations[15]),
                        this.ST_Gesture(daniel, 2)));
                       

            }, wanderers);
        return new SequenceParallel(middleStory, Eating, dance);


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
                wanderers[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("ACKNOWLEDGE", AnimationLayer.Face, 1000),
                wanderers[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADSHAKE", AnimationLayer.Face, 1000),
                wanderers[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("BEINGCOCKY", AnimationLayer.Hand, 1000),
                wanderers[0].GetComponent<BehaviorMecanim>().ST_PlayGesture("HEADNOD", AnimationLayer.Face, 1000)));
        }, wanderers);
        return new Sequence(converse);
    }

    }


