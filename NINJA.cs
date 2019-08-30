using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NINJA : baddie {
   
    Animator animator;
    bool activated;
    public float walkingspeed;
    public float activationdistance;
    public GameObject hitter;
    public bool twodee;
    float strafespeed;
    int strafeinterval;
    int strafecount;
    int attackingcount;
    public override void takehit(int strength)
    {
        if (!dead)
        {
            //GetComponent<enemyik>().looking = false;
            gameObject.GetComponent<AudioSource>().Play();
           
            health -= strength;
            animator.speed = 1;
            if (health <= 0)
                die();

           
         
            //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            //gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().carving = true;
       
        }
    }


    void die()
    {
        animator.speed = 0.2f;
        enemynumber--;
        hitter.active = false;
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        animator.SetBool("dead", true);
        dead = true;
        Collider coll = gameObject.GetComponent<Collider>();
        coll.enabled = false;
    }
    // Use this for initialization
    void Start () {
        enemynumber++;
        strafeinterval = Random.Range(10, 100);
        strafespeed = Random.Range(0, 0.5f);
        animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        {

            if (!dead)
            {

                if (!activated)
                {
                    if (Vector3.Distance(gameObject.transform.position, playermovement.target) < activationdistance)
                    {
                        activated = true;
                        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = walkingspeed;
                        animator.speed = walkingspeed / 3.5f;
                        animator.SetBool("RUN", true);
                        animator.SetBool("IDLE", false);
                    }
                }

                if (activated)
                {

                    Vector3 targetDir = playermovement.target - gameObject.transform.position;


                    if (attacking == true)
                    {
                        attackingcount++;
                        if(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.5f || attackingcount >300)
                        {
                            attackingcount = 0;
                            animator.SetBool("JUMP", false);
                            animator.SetBool("RUN", true);
                            hitter.active = false;
                            attacking = false;
                            animator.speed = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed / 3.5f;
                            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = walkingspeed;
                            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().acceleration = 100;

                        }

                    }
                    if (attacking == false)
                    {
                        gameObject.transform.Translate(strafespeed/3, 0, 0);
                        strafecount++;
                        if (strafecount > strafeinterval)
                        {
                            strafecount = 0;
                            
                            strafespeed = Random.Range(-0.2f, 0.2f);
                        }
                        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = walkingspeed;
                        animator.speed = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed / 3.5f;
                        hitter.active = false;
                        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target;
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) < 5f)
                        {
                            attacking = true;
                            animator.SetBool("JUMP", true);
                            animator.SetBool("RUN", false);
                            animator.speed = 1;
                            hitter.active = true;
                            Vector3 dest = gameObject.transform.position - playermovement.target;
                            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = walkingspeed * 2;
                            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().acceleration = 200;
                            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = gameObject.transform.position - (dest*3);


                        }
                    }

                }


                    }


                }
        if (twodee)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
        

    
}
