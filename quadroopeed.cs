using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadroopeed : baddie
{
    public GameObject dest1;
    public GameObject dest2;
    Animator animator;
    public GameObject man;
    UnityEngine.AI.NavMeshAgent navvy;
    public bool inviewcone;
    public GameObject hitter;
    public GameObject lightposition;
    public bool upperlit;
    public bool lowerlit;
    Vector3 player;
    int state;
    const int patrolling=1;
    const int attacking = 2;
    const int scanning = 3;
    const int readying = 4;
    const int dead = 5;
    RaycastHit hit;
    int scancount;
  
    void die()
    {
        enemynumber--;
        navvy.enabled = false;
        animator.speed = 1;
        animator.SetBool("dead", true);
        state = dead;
        Collider coll = gameObject.GetComponent<Collider>();
        coll.enabled = false;
        hitter.SetActive(false);

    }
    public override void takehit(int strength)
    {
        health -= strength;
        if(health >0)
        attack(playermovement.target);
        else
        {
            die();
        }

    }
    public void attack(Vector3 target)
    {
        
        hitter.SetActive(true);
        state = attacking;
        player = target;
        navvy.destination = target;
        navvy.speed = 15;
        
        animator.speed = 12;
        animator.SetBool("scanning", false);
        animator.SetBool("patrolling", true);
        navvy.autoBraking = false;
    }
    public void gettingready()
    {
        navvy.autoBraking = false;
        state = readying;
        

        navvy.destination = gameObject.transform.position + (gameObject.transform.forward*30);
        navvy.speed = 20;
        animator.speed = 12;
        animator.SetBool("scanning", false);
        animator.SetBool("patrolling", true);

    }
    Vector3 lightpoint2;
    public void checkcollisionupper(Vector3 lightpoint)
    {
        lightpoint2 = lightpoint;

        if (state != dead)
            if (Physics.Raycast(lightposition.transform.position, lightpoint - lightposition.transform.position, out hit, 200, LayerMask.GetMask("Default")))
        {
               
           if (hit.collider.gameObject.tag == "Player")
            {
                upperlit = true;
                attack(hit.collider.gameObject.transform.position);
            }
            else
            {
                upperlit = false;
                
            }

        }
   

 
    }
    

    public void checkcollisionlower(Vector3 lightpoint)
    {

        
        if(state!=dead)
        if (Physics.Raycast(lightposition.transform.position, lightpoint - lightposition.transform.position, out hit, 200, LayerMask.GetMask("Default")))
        {
           
           /* if (hit.collider.gameObject.tag == "Player")
            {
                lowerlit = true;
                attack(hit.collider.gameObject.transform.position);

            }
            else
            {
                lowerlit = false;

            }*/
        }
    }

    // Use this for initialization
    void Start()
    {
        enemynumber++;
        state = patrolling;
        hitter.SetActive(false);
        // activationdistance = 200;
        health = 5;
        // walkingspeed = 10;
        navvy = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        navvy.speed = 5;
        navvy.acceleration = 1000;
        
        animator = man.gameObject.GetComponent<Animator>();
        navvy.destination = dest1.gameObject.transform.position;
        navvy.stoppingDistance = 0;
    }
    void scan()
    {
        navvy.autoBraking = true;
        state = scanning;
        navvy.speed = 0;
        animator.SetBool("scanning", true);
        animator.SetBool("patrolling", false);
        scancount = 0;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (state != dead)
        {
            if (state == patrolling)
            {
                if (other.gameObject == dest1)
                {
                    navvy.destination = dest2.gameObject.transform.position;
                    scan();

                }
                if (other.gameObject == dest2)
                {
                    navvy.destination = dest1.gameObject.transform.position;
                    scan();
                }

            }
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject.transform.position;
                gettingready();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.transform.position;
            gettingready();
        }
    }

    // Update is called once per frame
    void Update () {
    
	}
    void FixedUpdate()
    {
        
       if(state==attacking)
        {
            if(navvy.remainingDistance < 0.5f)
            {
                
                hitter.SetActive(false);
                navvy.destination = dest1.gameObject.transform.position;
                animator.speed = 1;
                scan();
            }
        }
        if (state == readying)
        {
            if (navvy.remainingDistance < 0.5f)
            {
                navvy.autoBraking = true;
                attack(player);
            }
        }
        if (state==scanning)
        {
            hitter.SetActive(false);
            scancount++;
            if(scancount>500)
            {
                navvy.speed = 5;
                animator.SetBool("scanning", false);
                animator.SetBool("patrolling", true);
                state = patrolling;
                
            }
        }
    }
}
