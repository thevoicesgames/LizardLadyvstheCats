using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class ik : MonoBehaviour
{
    public GameObject leftfooot;
    public GameObject rightfooot;
    public GameObject leftkneee;
    public GameObject rightknee;
    leftfoot lf;
    protected Animator animator;
    playermovement player;
    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookObj = null;
    float footweight;
    float leftfootweight;
    float rightfootweight;
    float lookweight;
    RaycastHit hit;
    RaycastHit hit2;
    float lefthit;
    float righthit;
  
    void Start()
    {
        animator = GetComponent<Animator>();
        lf = leftfooot.GetComponent<leftfoot>();
        lookweight = 1;
        leftfootweight = 0;
        rightfootweight = 0;
        player = GetComponent<playermovement>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {


            if (Physics.Raycast(new Vector3(leftfooot.gameObject.transform.position.x, leftfooot.gameObject.transform.position.y + 1, leftfooot.gameObject.transform.position.z), new Vector3(0, -1, 0), out hit, 1.1f, LayerMask.GetMask("camerablockable")))
            {
                if ((leftfooot.transform.position.y + 1) - hit.distance < leftfooot.transform.position.y + 1)
                {
                    leftfootweight =0.9f;
                    lefthit = hit.distance;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(leftfooot.gameObject.transform.position.x, leftfooot.gameObject.transform.position.y - (hit.distance - 1.1f), leftfooot.gameObject.transform.position.z));
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftfootweight);

                    /* animator.SetIKHintPosition(AvatarIKHint.LeftKnee, new Vector3(leftkneee.transform.position.x, leftkneee.gameObject.transform.position.y - (hit.distance - 1.08f), leftkneee.gameObject.transform.position.z));
                     animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, footweight);*/

                }
            }
            else
            {

                leftfootweight -= 1;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(leftfooot.gameObject.transform.position.x, leftfooot.gameObject.transform.position.y - (lefthit- 1.1f), leftfooot.gameObject.transform.position.z));
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftfootweight);
            }




            if (Physics.Raycast(new Vector3(rightfooot.gameObject.transform.position.x, rightfooot.gameObject.transform.position.y + 1, rightfooot.gameObject.transform.position.z), new Vector3(0, -1, 0), out hit2, 1.1f, LayerMask.GetMask("camerablockable")))
            {
                if ((rightfooot.transform.position.y + 1) - hit2.distance < rightfooot.transform.position.y + 1)
                {
                    righthit = hit2.distance;
                    rightfootweight = 1;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(rightfooot.gameObject.transform.position.x, rightfooot.gameObject.transform.position.y - (hit2.distance - 1.1f), rightfooot.gameObject.transform.position.z));
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightfootweight);


                    /*animator.SetIKHintPosition(AvatarIKHint.RightKnee, new Vector3(rightknee.transform.position.x, rightknee.gameObject.transform.position.y - (hit.distance - 1.08f), rightknee.gameObject.transform.position.z));
                    animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, footweight);*/
                }
            }
            else
            {
                rightfootweight -= 0.1f;
                animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(rightfooot.gameObject.transform.position.x, rightfooot.gameObject.transform.position.y - (righthit - 1.1f), rightfooot.gameObject.transform.position.z));
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightfootweight);
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
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(lookweight);
                    animator.SetLookAtPosition(lookObj.position);
                   
                }

                // Set the right hand target position and rotation, if one has been assigned
              if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, lookweight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, lookweight);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
           /* if (leftHandObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }*/


            //if the IK is not active, set the position and rotation of the hand and head back to the original position



        }
        }
    private void FixedUpdate()
    {
        if (ikActive)
        {
            if (lookweight < 0.7f)
            {
                lookweight += 0.5f;
            }
           
        }
        else
        {
            if (lookweight > 0)
                lookweight -= 0.15f;
            if (leftfootweight > 0)
                leftfootweight -=0.1f;
            if (rightfootweight > 0)
                rightfootweight -=0.1f;
        }
    }
}

