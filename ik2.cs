using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class ik2 : MonoBehaviour
{
    public GameObject righthand;
    public GameObject rightfooot;
    public GameObject leftkneee;
    public GameObject rightknee;
    leftfoot lf;
    protected Animator animator;
    playermovement player;
    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    float footweight;
    float righthandeight;
    float rightfootweight;
    float lookweight;
    RaycastHit hit;
    RaycastHit hit2;
    float lefthit;
    float righthit;
   public  bool active;
    void Start()
    {
        animator = GetComponent<Animator>();
       
        lookweight = 1;
        righthandeight = 0;
        rightfootweight = 0;
        player = GetComponent<playermovement>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        active = true;
        if(active)
        if (animator)
        {


            if (Physics.Raycast(new Vector3(righthand.gameObject.transform.position.x, righthand.gameObject.transform.position.y, righthand.gameObject.transform.position.z-gameObject.transform.forward.z), gameObject.transform.forward, out hit, 1.1f, LayerMask.GetMask("camerablockable")))
            {
                if ((righthand.transform.position.z -gameObject.transform.forward.z)  - hit.distance < righthand.transform.position.z - gameObject.transform.forward.z)
                {
                    righthandeight = 1;
                    lefthit = hit.distance;
                    animator.SetIKPosition(AvatarIKGoal.RightHand, new Vector3(righthand.gameObject.transform.position.x, righthand.gameObject.transform.position.y, righthand.gameObject.transform.position.z- ( gameObject.transform.forward.z * (hit.distance/6))));
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,righthandeight);

                    /* animator.SetIKHintPosition(AvatarIKHint.LeftKnee, new Vector3(leftkneee.transform.position.x, leftkneee.gameObject.transform.position.y - (hit.distance - 1.08f), leftkneee.gameObject.transform.position.z));
                     animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, footweight);*/

                }
            }


            if (Physics.Raycast(new Vector3(rightfooot.gameObject.transform.position.x, rightfooot.gameObject.transform.position.y, rightfooot.gameObject.transform.position.z - gameObject.transform.forward.z), gameObject.transform.forward, out hit, 1.1f, LayerMask.GetMask("camerablockable")))
            {
                if ((rightfooot.transform.position.z - gameObject.transform.forward.z) - hit.distance < rightfooot.transform.position.z - gameObject.transform.forward.z)
                {
                    rightfootweight = 1;
                    lefthit = hit.distance;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(rightfooot.gameObject.transform.position.x, rightfooot.gameObject.transform.position.y, rightfooot.gameObject.transform.position.z - (gameObject.transform.forward.z * (hit.distance / 6))));
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightfootweight);

                    /* animator.SetIKHintPosition(AvatarIKHint.LeftKnee, new Vector3(leftkneee.transform.position.x, leftkneee.gameObject.transform.position.y - (hit.distance - 1.08f), leftkneee.gameObject.transform.position.z));
                     animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, footweight);*/

                }
            }

                if (lookObj != null)
                {
                    animator.SetLookAtWeight(lookweight);
                    animator.SetLookAtPosition(lookObj.position);

                }
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, lookweight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, lookweight);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, rightHandObj.rotation);
                }

                /*if (lf.standing)
                {
                    if (footweight < 1)
                        footweight += 0.1f;

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footweight);

                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(leftfooot.transform.position.x, lf.pos, leftfooot.transform.position.z));



                }

                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    if (footweight > 0)
                        footweight -= 0.1f;

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footweight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);

                }*/


                //if the IK is active, set the position and rotation directly to the goal. 


                // Set the look target position, if one has been assigned






                //if the IK is not active, set the position and rotation of the hand and head back to the original position



            }
    }
    private void FixedUpdate()
    {
        if (ikActive)
        {
            
        }
        else
        {
          
            if (righthandeight > 0)
                righthandeight -= 0.1f;
            if (rightfootweight > 0)
                rightfootweight -= 0.1f;
        }
    }
}

