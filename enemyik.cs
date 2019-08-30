using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyik : MonoBehaviour
{


    public GameObject leftfooot;
    public GameObject rightfooot;
    public GameObject righthand;

    public GameObject rightknee;
    leftfoot lf;
    protected Animator animator;
    playermovement player;
    public bool ikActive = false;
    public bool looking;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    float footweight;
    float leftfootweight;
    float rightfootweight;
    float lookweight;
    RaycastHit hit;
    RaycastHit hit2;
    float lefthit;
    float righthit;
    float aimweight;
    public bool aiming;
    public Vector3 aimpos;
    void Start()
    {
        aimpos = new Vector3(playermovement.target.x, playermovement.target.y + 1, playermovement.target.z);
        looking = true;
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
            aimpos = new Vector3(playermovement.target.x, playermovement.target.y+1.15f, playermovement.target.z);

            /*if (Physics.Raycast(new Vector3(leftfooot.gameObject.transform.position.x, leftfooot.gameObject.transform.position.y + 1, leftfooot.gameObject.transform.position.z), new Vector3(0, -1, 0), out hit, 1.1f, LayerMask.GetMask("camerablockable")))
            {
                if ((leftfooot.transform.position.y + 1) - hit.distance < leftfooot.transform.position.y + 1)
                {
                    leftfootweight = 0.9f;
                    lefthit = hit.distance;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(leftfooot.gameObject.transform.position.x, leftfooot.gameObject.transform.position.y - (hit.distance - 1.1f), leftfooot.gameObject.transform.position.z));
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftfootweight);

                

                }
            }
            else
            {

                leftfootweight -= 1;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(leftfooot.gameObject.transform.position.x, leftfooot.gameObject.transform.position.y - (lefthit - 1.1f), leftfooot.gameObject.transform.position.z));
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


                   
                }
            }
            else
            {
                rightfootweight -= 0.1f;
                animator.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(rightfooot.gameObject.transform.position.x, rightfooot.gameObject.transform.position.y - (righthit - 1.1f), rightfooot.gameObject.transform.position.z));
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightfootweight);
            }*/

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
           
                animator.SetLookAtWeight(lookweight);
                animator.SetLookAtPosition(aimpos);

            

            // Set the right hand target position and rotation, if one has been assigned
           
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, aimweight);
               // animator.SetIKRotationWeight(AvatarIKGoal.RightHand, lookweight);
                animator.SetIKPosition(AvatarIKGoal.RightHand, aimpos);

                //animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            



            //if the IK is not active, set the position and rotation of the hand and head back to the original position



        }
    }
    private void FixedUpdate()
    {
        if (ikActive)
        {
            leftfootweight = 1;
            rightfootweight = 1;

        }
        else
        {
            if (lookweight > 0)
                lookweight -= 0.15f;
            if (leftfootweight > 0)
                leftfootweight -= 0.1f;
            if (rightfootweight > 0)
                rightfootweight -= 0.1f;
        }
        if (looking)
        {
            if (lookweight < 1)
            {
                lookweight += 0.1f;
            }

        }
        else
        {
            if (lookweight > 0)
                lookweight -= 0.15f;
        }

        if (aiming)
        {
            if (aimweight < 1)
            {
                aimweight += 0.1f;
            }
        }
        else
        {
            if (aimweight > 0)
            {

                aimweight -= 0.1f;
            }

        }
    }
}

