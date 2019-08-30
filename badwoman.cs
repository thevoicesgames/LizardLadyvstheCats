using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badwoman : baddie {

    public GameObject target;
    //public GameObject particles;
    public GameObject man;
    public GameObject hitter;
    public GameObject gunobject;
    public GameObject dest1;
    public GameObject dest2;
    enemygun gun;
   // public int health;
    int pausecounter;
    bool activated;
    bool paused;
    bool recoiling;
    public float activationdistance;
    public float destinationsdistance;
    int activatedcounter;
    Animator animator;
    UnityEngine.AI.NavMeshAgent navvy;
    int recoilcount;
    int stunlockduration;
    bool kicked;
    public float walkingspeed;
    int bulletinterval;
    int bulletcount;
    Vector3 knockbackdirection;
    public GameObject head;
    public bool twodee;
    bool pushing;
    int pushcount;
    public int state;
    const int flamethrower = 1;
    const int destinations = 2;
    int attackstate;
    float strafespeed;
    const int strafe = 3;
    const int standing = 4;
    const int retreating = 5;
    int attackstatecount;
    int changeattackinterval;
    float reatreatabldistance;
    GameObject destinat;
    // Use this for initialization
    void Start()
    {
        health = 2;
        if(state==1)
        {
            health = 30;
        }
        reatreatabldistance = Random.Range(5, 10);
        changeattackinterval = Random.Range(100, 500);
        attackstatecount = changeattackinterval;
        enemynumber++;
        bulletcount = 90;
        bulletinterval = Random.Range(30, 200);
        gun = gunobject.GetComponent<enemygun>();
        //health = 5;
        // walkingspeed = 10;
        navvy = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        navvy.speed = 0;
        navvy.acceleration = 50;
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("standing", true);
        animator.speed = 1;
        if(state == flamethrower)
        {
            gameObject.GetComponent<enemyik>().aiming = true;
        }
        if(state == destinations)
        {
            destinat = dest1;
        }
    }
    public override void takehit(int strength)
    {
        if (!recoiling && !dead)
        {
            if(!activated)
            {
                
                activated = true;
                //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                navvy.speed = walkingspeed;
                animator.speed = walkingspeed / 3.5f;
                animator.SetBool("standing", false);
                if (walkingspeed > 9)
                {
                    animator.SetBool("fastwalk", true);
                }
                else
                {
                    animator.SetBool("forwards", true);
                }


                this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target;
            }
            gameObject.GetComponent<AudioSource>().Play();
            stunlockduration = 30;
            health -= strength;
            if (health <= 0)
                die();
            else
            {
              /* if (attacking && attackstate != retreating)
                {
                    float ret = Random.Range(0, 1);
                    if (ret < 0.3f)
                        retreat();
                }*/

            }
            animator.speed = 1;
        
       
     
        }
    }
    void retreat()
    {
        attacking = true;
       
        reatreatabldistance = Random.Range(5  ,10);
        attackstate = retreating;
        attackstatecount = 0;
        animator.SetBool("forwards", true);

        animator.SetBool("standing", false);
        animator.SetBool("walkright", false);
        animator.SetBool("walkleft", false);
        navvy.speed = walkingspeed * 3;
        gameObject.GetComponent<enemyik>().aiming = false;
        animator.speed = (walkingspeed * 3) / 3.5f;
        float retreatdistance = Random.Range(10,20);
        Vector3 dest = gameObject.transform.position - ((gameObject.transform.forward) * retreatdistance);
        
      
        navvy.SetDestination(dest);
        navvy.angularSpeed = 100000;
    }
    void OnTriggerEnter(Collider other)
    {
        /* if(other.gameObject.tag == "bullet")
         {

             health--;
             Destroy(other.gameObject);
             paused = true;
             gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
             gameObject.GetComponent<Animator>().enabled = false;
             gameObject.GetComponent<AudioSource>().Play();
         }*/
         if(state == destinations)
        {
            if (other.gameObject == destinat)
            {
                attacking = true;
                animator.SetBool("fastwalk", false);
                animator.SetBool("forwards", false);
                animator.SetBool("standing", true);
                animator.speed = 1;
                navvy.speed = 0;
                gameObject.GetComponent<enemyik>().aiming = true;
                if (destinat == dest1)
                    destinat = dest2;
                else
                    destinat = dest1;
            }
        }
        if (other.gameObject.tag == "kickfoot")
        {
            takehit(1);
            stunlockduration = 30;
            kicked = true;
            knockbackdirection = gameObject.transform.position - other.gameObject.transform.position;
            knockbackdirection.y = 0;
            knockbackdirection.Normalize();
        }

    }
    void die()
    {
        enemynumber--;
       // gameObject.GetComponent<AudioSource>().Play();
        //Destroy(gameObject);
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled= false;
        animator.speed = 1;
        animator.SetBool("dead", true);
        Destroy(gun.gameObject);
        dead = true;
        Collider coll = gameObject.GetComponent<Collider>();
        gameObject.GetComponent<enemyik>().aiming = false;
        gameObject.GetComponent<enemyik>().ikActive = false;
        coll.enabled = false;
    }
    void pushtest()
    {
        if (!pushing)
        {
            if (Vector3.Distance(gameObject.transform.position, playermovement.target) < 1)
            {
                pushing = true;
                attacking = false;
                animator.speed = 1;
                animator.SetBool("pushing", true);
                animator.speed = 1;
                gameObject.GetComponent<enemyik>().ikActive = false;
                gameObject.GetComponent<enemyik>().aiming = false;
                pushcount = 0;
                attacking = false;

            }
        }
        else
        {
            pushcount++;
            if (pushcount > 10)
            {
                playermovement.pushed = true;
                playermovement.pusheddirection = gameObject.transform.forward / 10;
            }
            if (pushcount > 20)
            {
                pushing = false;
                animator.SetBool("pushing", false);
                gameObject.GetComponent<enemyik>().ikActive = true;
                gameObject.GetComponent<enemyik>().aiming = true;
            }
        }
    }
    void strafecontrol()
    {
        attackstatecount++;
        if (attackstatecount > changeattackinterval - 1)
        {
            attackstatecount = 0;
            changeattackinterval = Random.Range(50, 700);
            float tak = Random.Range(-1, 1);
            if (tak < -1)
            {
                attackstate = standing;
            }
            else
                attackstate = strafe;
            if (attackstate == strafe)
            {
                animator.SetBool("forwards", false);
                animator.SetBool("fastwalk", false);
                animator.SetBool("standing", false);



                strafespeed = Random.Range(-0.05f, 0.05f);
                if (strafespeed < 0)
                {
                    animator.SetBool("walkright", false);
                    animator.SetBool("walkleft", true);

                    animator.speed = -strafespeed * 10;

                }
                if (strafespeed > 0)
                {
                    animator.SetBool("walkleft", false);
                    animator.SetBool("walkright", true);

                    animator.speed = strafespeed * 10;
                }
                if (strafespeed < 0.001f && strafespeed > -0.001f)
                {
                    animator.SetBool("walkright", false);
                    animator.SetBool("walkleft", false);
                    animator.SetBool("standing", true);
                    animator.speed = 1;
                    strafespeed = 0;
                }

            }
            if (attackstate == standing)
            {
                animator.SetBool("walkright", false);
                animator.SetBool("walkleft", false);
                animator.SetBool("standing", true);
                animator.speed = 1;
            }
        }
        if (attackstate == strafe)
        {
            if (strafespeed < 0)
            {
                animator.SetBool("walkright", false);
                animator.SetBool("walkleft", true);
                RaycastHit hits;
                if (Physics.Raycast(head.transform.position, -gameObject.transform.right, out hits, 3))
                {
                    if (hits.distance < 1)
                    {
                        strafespeed = -strafespeed;
                        animator.SetBool("walkleft", false);
                        animator.SetBool("walkright", true);
                    }
                }
                // animator.speed = -strafespeed / 2;

            }
            if (strafespeed > 0)
            {
                animator.SetBool("walkleft", false);
                animator.SetBool("walkright", true);
                RaycastHit hits;
                if (Physics.Raycast(head.transform.position, gameObject.transform.right, out hits, 3))
                {
                    if (hits.distance < 1)
                    {
                        strafespeed = -strafespeed;
                        animator.SetBool("walkright", false);
                        animator.SetBool("walkleft", true);
                    }
                }

                // animator.speed = strafespeed / 2;
            }
            if (strafespeed == 0)
            {
                animator.SetBool("walkright", false);
                animator.SetBool("walkleft", false);
                animator.SetBool("standing", true);
                animator.speed = 1;
            }
            gameObject.transform.LookAt(playermovement.target, Vector3.up);
            gameObject.transform.Translate(strafespeed / 5, 0, 0);
        }
        if (attackstate == standing)
        {
            gameObject.transform.LookAt(playermovement.target, Vector3.up);
        }
    }


    void FixedUpdate()
    {

        if (!dead)
        {

            if (!activated)
            {
                if (Vector3.Distance(gameObject.transform.position, playermovement.target) < activationdistance)
                {
                    activated = true;
                    //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                    navvy.speed = walkingspeed;
                    animator.speed = walkingspeed / 3.5f;
                    animator.SetBool("standing", false);
                    if (walkingspeed > 9)
                    {
                        animator.SetBool("fastwalk", true);
                    }
                    else
                    {
                        animator.SetBool("forwards", true);
                    }
                   
           
            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target;
           }
         }
            if (activated)
            {
                if (state == 0)
                {
                    pushtest();
                    if (!attacking)
                    {
                        animator.SetBool("walkleft", false);
                        animator.SetBool("walkright", false);
                        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target;
                        RaycastHit hit;
                        Vector3 target = new Vector3(playermovement.target.x, playermovement.target.y + 1.15f, playermovement.target.z);

                        if (Physics.Raycast(head.transform.position, target-head.transform.position, out hit, 10000))
                        {

                            if (hit.collider.gameObject.tag == "Player")
                            {
                                attacking = true;
                                animator.SetBool("fastwalk", false);
                                animator.SetBool("forwards", false);
                                //animator.SetBool("standing", true);
                                animator.speed = 1;
                                navvy.speed = 0;
                                gameObject.GetComponent<enemyik>().aiming = true;
                                activatedcounter = 0;
                            }
                        }
                    }
                    else
                    {
                        if (attackstate != retreating)
                        {
                            if (attackstatecount > changeattackinterval - 1)
                            {
                                attackstatecount = 0;
                                changeattackinterval = Random.Range(50, 700);
                                float tak = Random.Range(-1, 1);
                                if (tak < -1)
                                {
                                    attackstate = standing;
                                }
                                else
                                    attackstate = strafe;
                                if (attackstate == strafe)
                                {
                                    animator.SetBool("forwards", false);
                                    animator.SetBool("fastwalk", false);
                                    animator.SetBool("standing", false);



                                    strafespeed = Random.Range(-0.2f, 0.2f);
                                    if (strafespeed < 0)
                                    {
                                        animator.SetBool("walkright", false);
                                        animator.SetBool("walkleft", true);

                                        animator.speed = -strafespeed * 10;

                                    }
                                    if (strafespeed > 0)
                                    {
                                        animator.SetBool("walkleft", false);
                                        animator.SetBool("walkright", true);

                                        animator.speed = strafespeed * 10;
                                    }
                                    if (strafespeed < 0.01f && strafespeed > -0.01f)
                                    {
                                        animator.SetBool("walkright", false);
                                        animator.SetBool("walkleft", false);
                                        animator.SetBool("standing", true);
                                        animator.speed = 1;
                                        strafespeed = 0;
                                    }

                                }
                                if (attackstate == standing)
                                {
                                    animator.SetBool("walkright", false);
                                    animator.SetBool("walkleft", false);
                                    animator.SetBool("standing", true);
                                    animator.speed = 1;
                                }
                            }
                            if (attackstate == strafe)
                            {
                                if (strafespeed < 0)
                                {
                                    animator.SetBool("walkright", false);
                                    animator.SetBool("walkleft", true);
                                    RaycastHit hits;

                                    if (Physics.Raycast(head.transform.position,-gameObject.transform.right, out hits, 3))
                                    {
                                        if (hits.distance < 1)
                                        {
                                            strafespeed = -strafespeed;
                                            animator.SetBool("walkleft", false);
                                            animator.SetBool("walkright", true);
                                        }
                                    }
                                    // animator.speed = -strafespeed / 2;

                                }
                                if (strafespeed > 0)
                                {
                                    animator.SetBool("walkleft", false);
                                    animator.SetBool("walkright", true);
                                    RaycastHit hits;
                                    if (Physics.Raycast(head.transform.position, gameObject.transform.right, out hits, 3))
                                    {
                                        if (hits.distance < 1)
                                        {
                                            strafespeed = -strafespeed;
                                            animator.SetBool("walkright", false);
                                            animator.SetBool("walkleft", true);
                                        }
                                    }

                                    // animator.speed = strafespeed / 2;
                                }
                                if (strafespeed == 0)
                                {
                                    animator.SetBool("walkright", false);
                                    animator.SetBool("walkleft", false);
                                    animator.SetBool("standing", true);
                                    animator.speed = 1;
                                }
                                gameObject.transform.LookAt(playermovement.target, Vector3.up);
                                gameObject.transform.Translate(strafespeed / 5, 0, 0);
                            }
                            if (attackstate == standing)
                            {
                                gameObject.transform.LookAt(playermovement.target, Vector3.up);
                            }
                            attackstatecount++;
                            bulletcount++;
                            activatedcounter++;
                            if (bulletcount > bulletinterval)
                            {
                                bulletinterval = Random.Range(30, 200);
                                bulletcount = 0;
                                gun.Shoot();

                            }

                            if(Vector3.Distance(playermovement.target,gameObject.transform.position)<reatreatabldistance)
                            {
                                retreat();

                            }

                            RaycastHit hit;
                            if (activatedcounter > 50)
                            {
                                Vector3 target = new Vector3(playermovement.target.x, playermovement.target.y + 1.15f, playermovement.target.z);

                                if (Physics.Raycast(head.transform.position, target - head.transform.position, out hit, 10000))
                                {

                                    if (hit.collider.gameObject.tag != "Player")
                                    {
                                        attacking = false;
                                     
                                            animator.SetBool("forwards", true);
                                        
                                        animator.SetBool("standing", false);
                                        animator.SetBool("walkright", false);
                                        animator.SetBool("walkleft", false);
                                        navvy.speed = walkingspeed;
                                        gameObject.GetComponent<enemyik>().aiming = false;
                                        animator.speed = walkingspeed / 3.5f;
                                    }
                                }
                            }
                        }

                    }
                    if(attackstate==retreating)
                    {
                        attackstatecount++;
                        if(Vector3.Distance(gameObject.transform.position,navvy.destination)<5||attackstatecount>500)
                        {
                            attackstate = strafe;
                            attacking = false;
                        
                                animator.SetBool("forwards", true);
                           
                            animator.SetBool("standing", false);
                            animator.SetBool("walkright", false);
                            animator.SetBool("walkleft", false);
                            navvy.speed = walkingspeed;
                            gameObject.GetComponent<enemyik>().aiming = false;
                            animator.speed = walkingspeed / 3.5f;
                        }
                    }
                }
                if(state == flamethrower)
                {
                    pushtest();
                    bulletcount++;
                   
                    if (bulletcount > bulletinterval)
                    {
                        bulletinterval = Random.Range(30, 100);
                       
                        bulletcount = 0;
                        gun.Shoot();

                    }
                    if (!attacking)
                    {
                        /*RaycastHit hits1;
                        if (Physics.Raycast(head.transform.position, -gameObject.transform.forward, out hits1, 20))
                        {
                            if (hits1.distance < 20)
                            {
                                if(hits1.collider.gameObject.tag=="baddie")
                                {
                                    attacking = true;
                                    animator.SetBool("forwards", false);
                                    animator.SetBool("fastwalk", false);
                                    animator.SetBool("standing", true);
                                    animator.speed = 1;
                                    navvy.speed = 0;
                                }
                            

                            }
                        }*/

                        attackstatecount++;
                        if (attackstatecount > changeattackinterval)
                        {
                            changeattackinterval = Random.Range(500, 1000);
                           attackstatecount = 0;
                            
                            strafespeed = Random.Range(-3,3);
                            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target + gameObject.transform.right * strafespeed;
                        }
                        if (strafespeed < 0)
                        {
                           
                            RaycastHit hits;
                            if (Physics.Raycast(head.transform.position-gameObject.transform.forward, -gameObject.transform.right, out hits, 10))
                            {
                                if (hits.distance < 1)
                                {
                                    strafespeed = -strafespeed;
                                 
                                }
                            }
                            // animator.speed = -strafespeed / 2;

                        }
                        if (strafespeed > 0)
                        {
                          
                            RaycastHit hits;
                            if (Physics.Raycast(head.transform.position-gameObject.transform.forward, gameObject.transform.right, out hits, 10))
                            {
                                if (hits.distance < 1)
                                {
                                    strafespeed = -strafespeed;
                               
                                }
                            }

                            // animator.speed = strafespeed / 2;
                        }
                      
                        //gameObject.transform.Translate(strafespeed/10, 0, 0);
                       if (Vector3.Distance(gameObject.transform.position,navvy.destination) < 3)
                        {
                            navvy.SetDestination(playermovement.target);
                        }
                        else
                            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target + gameObject.transform.right * strafespeed;

                      
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) < 3 && !attacking)
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, 400))
                            {

                                if (hit.collider.gameObject.tag == "Player")
                                {
                                    attackstatecount = 100000;
                                    attacking = true;
                                    animator.SetBool("forwards", false);
                                    animator.SetBool("fastwalk", false);
                                    animator.SetBool("standing", true);
                                    animator.speed = 1;
                                    navvy.speed = 0;
                                }
                            }
                            
                        }
                    }
                    if(attacking)
                    {

                        gameObject.transform.LookAt(playermovement.target);
                        //strafecontrol();
                        RaycastHit hit;
                        if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, 400))
                        {

                            if (hit.collider.gameObject.tag != "Player")
                            {
                                attackstatecount = 1000000;
                                attacking = false;
                                if (walkingspeed > 9)
                                {
                                    animator.SetBool("fastwalk", true);
                                }
                                else
                                {
                                    animator.SetBool("forwards", true);
                                }
                                animator.SetBool("walkleft", false);
                                animator.SetBool("walkright", false);
                                animator.SetBool("standing", false);
                                navvy.speed = walkingspeed;

                                animator.speed = walkingspeed / 3.5f;
                            }
                        }
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) > 3.5f)
                        {
                           
                            attackstatecount = 1000000;
                            attacking = false;
                            if (walkingspeed > 9)
                            {
                                animator.SetBool("fastwalk", true);
                            }
                            else
                            {
                                animator.SetBool("forwards", true);
                            }
                            animator.SetBool("walkleft", false);
                            animator.SetBool("walkright", false);
                            animator.SetBool("standing", false);
                            navvy.speed = walkingspeed;

                            animator.speed = walkingspeed / 3.5f;
                        }
                    }
                }
                if(state == destinations)
                {
                    pushtest();
                    if (!attacking)
                    {
                        navvy.SetDestination(destinat.transform.position);


                      
                            animator.SetBool("forwards", true);
                        
                        animator.SetBool("standing", false);
                        navvy.speed = walkingspeed;
                        gameObject.GetComponent<enemyik>().aiming = false;
                        animator.speed = walkingspeed / 3.5f;
                        

                        
                    }
                    if(attacking)
                    {
                        bulletcount++;
                        activatedcounter++;
                        if (bulletcount > bulletinterval)
                        {
                            bulletinterval = Random.Range(30, 200);
                            gun.Shoot();
                            bulletcount = 0;
                        }
                        gameObject.transform.LookAt(playermovement.target);
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) < destinationsdistance)
                        {
                            attacking = false;
                        }
                    }

                }

            }
            /*if (activated)
            {
                if (recoiling)
                {
                    if (kicked)
                    {
                        gameObject.transform.position = gameObject.transform.position + knockbackdirection;
                    }
                    recoilcount++;
                    if (recoilcount > stunlockduration)
                    {
                        recoiling = false;
                        animator.SetBool("recoiling", false);
                        // gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled  = true;
                        navvy.speed = walkingspeed;
                        animator.speed = walkingspeed / 3.5f;
                        recoilcount = 0;
                        kicked = false;
                    }

                }
                if (!recoiling)
                {
                    Vector3 targetDir = playermovement.target - gameObject.transform.position;


                    if (attacking == true)
                    {
                        hitter.active = true;
                        navvy.speed = 0;
                        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.z), Vector3.up), 0.01f);
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) > 1.5)
                        {
                            attacking = false;
                            animator.SetBool("attacking", false);
                            //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled  = true;


                        }
                    }
                    if (attacking == false)
                    {
                        navvy.speed = walkingspeed;
                        hitter.active = false;
                        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target;
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) < 1.5)
                        {
                            attacking = true;
                            animator.SetBool("attacking", true);
                            animator.speed = 1;
                            //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                            navvy.speed = 0;
                        }

                    }


                }


            }*/
            if(twodee)
            {
                gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
    // Update is called once per frame

}
