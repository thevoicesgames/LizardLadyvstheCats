using UnityEngine;
using System.Collections;

public class movetowardplayer : baddie {
    public GameObject target;
    //public GameObject particles;
    public GameObject man;
    public GameObject hitter;
 
    int pausecounter;
    bool activated;
    bool paused;
    bool recoiling;
    public float activationdistance;
    
   
    Animator animator;
    UnityEngine.AI.NavMeshAgent navvy;
    int recoilcount;
    int stunlockduration;
    bool kicked;
    public int walkingspeed;
    public bool twodee;
    Vector3 knockbackdirection;
	// Use this for initialization
	void Start () {
        //activationdistance = 100;
        //health = 5;
        // walkingspeed = 10;
        enemynumber++;
        navvy = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        navvy.speed = 0;
        navvy.acceleration = 10000;
        animator = man.gameObject.GetComponent<Animator>();
    }
    public override void takehit(int strength)
    {
        if (!recoiling && !dead)
        {
           // GetComponent<enemyik>().looking = false;
           gameObject.GetComponent<AudioSource>().Play();
            stunlockduration = 30;
            health -= strength;
            animator.speed = 1; 
            if (health <= 0)
                die();
             
            animator.SetBool("recoiling", true);
            recoiling = true;
            enabled = true;
            //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            //gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().carving = true;
            navvy.speed = 0;
            recoilcount = 0;
            hitter.active = false;
           if(!activated)
            {
                activated = true;
                animator.SetBool("walking", true);
            }
        }
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
        if(other.gameObject.tag == "kickfoot")
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
        animator.speed = 1;
        enemynumber--;
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        animator.SetBool("dead", true);
        dead = true;
        Collider coll = gameObject.GetComponent<Collider>();
        coll.enabled = false;
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
                    gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                    navvy.speed = walkingspeed;
                    animator.speed = walkingspeed/3.5f;
                    animator.SetBool("walking", true);
                }
            }

            if (activated)
            {
                if (recoiling)
                {
                    if(kicked)
                    {
                        gameObject.transform.position = gameObject.transform.position + knockbackdirection;
                    }
                    recoilcount++;
                    if (recoilcount > stunlockduration)
                    {
                        recoiling = false;
                        animator.SetBool("recoiling", false);
                       // gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().carving = false;
                        //gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;

                       
                       
                       // gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled  = true;
                        navvy.speed = walkingspeed;
                        animator.speed = walkingspeed / 3.5f;
                        recoilcount = 0;
                        kicked = false;
                       // GetComponent<enemyik>().looking = true;
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
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) > 2f )
                        {
                            attacking = false;
                            animator.SetBool("attacking", false);
                           // gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().carving = false;
                            //gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
                           //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled  = true;
                           
                            animator.speed = walkingspeed / 3.5f;
                            navvy.speed = walkingspeed;
                        }
                    }
                    if (attacking == false)
                    {
                        navvy.speed = walkingspeed;
                        hitter.active = false;
                        this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playermovement.target;
                        if (Vector3.Distance(gameObject.transform.position, playermovement.target) < 1.2f)
                        {
                            attacking = true;
                            animator.SetBool("attacking", true);
                            animator.speed = 1;
                            
                           // gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                            //gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().carving = true;
                            //gameObject.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                            navvy.speed = 0;
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
        // Update is called once per frame
   
    }
