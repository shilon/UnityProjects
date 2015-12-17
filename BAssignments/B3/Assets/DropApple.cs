using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;
using TreeSharpPlus;

public class DropApple : MonoBehaviour {

    //public void ResetInteractionObject(){

    //}
    public InteractionObject apple;
    private FullBodyBipedIK ik;
    private float holdWeight;
   

    IEnumerator OnInteractionEnd()
    {
        ik = apple.GetComponent<FullBodyBipedIK>();
        Debug.Log("interaction ends");
        StartCoroutine(Drop());
        while (holdWeight < 1f)
        {
            holdWeight += Time.deltaTime;
            yield return null;
        }
    }


    void Update()
    {
        if (ik == null) return;

        if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Drop());
    }

    IEnumerator Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;

        while (holdWeight > 0f)
        {
            holdWeight -= Time.deltaTime * 3f;
            yield return null;
        }

    }
}
