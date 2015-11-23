using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

public class Handshake : MonoBehaviour {

    public class InteractionC2CDemo : MonoBehaviour
    {

        // GUI for testing

        public InteractionSystem character1, character2; // The InteractionSystems of the characters
        public InteractionObject handShake; // The HandShake InteractionObject

        void LateUpdate(InteractionSystem character1, InteractionSystem character2, InteractionObject handShake)
        {
            // Positioning the handshake to the middle of the hands
            Vector3 handsCenter = Vector3.Lerp(character1.ik.solver.rightHandEffector.bone.position, character2.ik.solver.rightHandEffector.bone.position, 0.5f);
            handShake.transform.position = handsCenter;
        }

    }
}
