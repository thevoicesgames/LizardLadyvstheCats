using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class playermovement : MonoBehaviour {
  


    public CharacterController controller;
    public Vector3 speed;
    public static Vector3 target;
    public float walkspeed;
    public float gravity;
    public GameObject camera;
    public GameObject cameratarget;
    public GameObject camerabone;
    protected Animator animator;
    public bool backwards;
    bool jump;
    public GameObject mobilecontrols;
    int jumpdelay;
    int jumpcount;
    bool jumpready;
    public bool walljump;
    public static ik kin;
    int deathcount;
    public static bool shootable;
    public static bool rolling;
    bool rollready;
    int rollcount;
    float health;
    float twodregionz;
    public UnityEngine.UI.Image healthdisplay;
    public UnityEngine.UI.Text rollcounttex;
    public static bool gunactive;
    public int state;
    public const int tps = 1;
    public const int stickoverhead = 2;
    public const int sideplatformer = 3;
    public const int twodregion = 4;
    public GameObject overheadtarget;
    int updatenumber;
    int updatecount;
    bool crouch;
    bool crouchready;
    int crouchcount;
    public GameObject enemylookatpos;
    public GameObject cameraviewtarget;
    public GameObject gunaim;
    public static bool pushed;
   public static float horizontalinput;
   public static float verticalinput;
    public static float jumpinput;
   public static float crouchinput;
    public static bool dodgeinput;
    public static float shootinput;
    public static Vector3 pusheddirection;
    int pushedcount;
    bool dead;
   public bool jumppressed;
    int jumpbuffercount;
    public GameObject gunmaster;
    public GameObject gunaimposition;
    public GameObject gunreadyposition;
    public GameObject gun;
    bool walkleft;
    bool walkright;
    void OnCollisionEnter(Collision col)
    {
        if(!dead)
        if (col.gameObject.tag == "deathball")
        {
            die();
        }
    }
    void OnTriggerEnter(Collider other)
    {
       
        
        if (!dead)
        {

            if (other.tag == "hitter")
            {
                takedamage(1);

            }
            if (other.tag == "KNIFE")
            {
                takedamage(1);
                pushed = true;
                Vector3 directiond = new Vector3(other.gameObject.transform.forward.x, 0, other.gameObject.transform.forward.z);
                pusheddirection = directiond/7;

            }
            if (other.tag == "enemybullet")
            {
                takedamage(1);
                Destroy(other.gameObject);
            }
            if(other.tag == "deathball")
            {
                die();
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "flames")
        {
            health-=0.3f;
            if(health <=0)
            {
                die();
            }
        }
    }
    public void takedamage(int amount)
    {
        health -= amount;
        gameObject.GetComponent<AudioSource>().Play();
     }
    // Use this for initialization
    void Start () {
        health = 5;
        controller = GetComponent<CharacterController>();
        //cameracontrol.autoaim = true;
        gravity = 0.01f;
        walkspeed = 0.01f;
        animator = GetComponent<Animator>();
        jumpdelay = 5;
        jumpready = true;
        rollready = true;
        kin = GetComponent<ik>();
        target = gameObject.transform.position;
        crouchready = true;
       // Time.timeScale = 0.2f;
        
    }
    void die()
    {
        animator.speed = 1;
        animator.SetBool("dead", true);
        dead = true;
        shootable = false;
        GetComponent<ik>().ikActive = false;
        pushed = false;
        pushedcount = 0;
    }
    // Update is called once per frame
    public void accelerationregion()
    {
        animator.SetBool("jumping", true);
        jump = false;
        jumpcount = 0;
        jumpready = false;
        animator.speed = 1;
        if (state == sideplatformer)
            backwards = false;
        crouch = false;
        controller.height = 1.5f;
    }
    void MobileInput()
    {
        if (CrossPlatformInputManager.GetButton("Jump"))
        {
            if (!jumppressed)
            {
                jumpinput = 1;
                jumppressed = true;
                jumpbuffercount = 0;
            }
            else
            {
                jumpbuffercount++;
                if (jumpbuffercount < 10)
                {
                    jumpinput = 1;
                }
                else
                {
                    jumpinput = 0;
                    
                }
            }
        }

        else
        {
            jumpinput = 0;
            jumppressed = false;
        }
        if (CrossPlatformInputManager.GetButton("Shoot"))
            shootinput = 1;

        else shootinput= 0;

        if (CrossPlatformInputManager.GetButton("Dodge"))
            dodgeinput = true;

        else dodgeinput = false;
        horizontalinput = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalinput = CrossPlatformInputManager.GetAxis("Vertical");
     

    }
    void FixedUpdate()
    {
        horizontalinput = Input.GetAxis("Horizontal");
        verticalinput = Input.GetAxis("Vertical");
        if (Input.GetAxis("Jump")>0.1f)
        {
            if (!jumppressed)
            {
                jumpinput = 1;
                jumppressed = true;
                jumpbuffercount = 0;
            }
            else
            {
                jumpbuffercount++;
                if (jumpbuffercount < 10)
                {
                    jumpinput = 1;
                }
                else
                {
                    jumpinput = 0;

                }
            }

        }
        else
        {
            jumpinput = 0;
            jumppressed = false;
        }
        
      //  crouchinput = Input.GetAxis("crouch");
        dodgeinput = Input.GetButton("Cancel");
        shootinput = Input.GetAxis("Fire1");
        if (Input.GetJoystickNames().Length == 0 && !Input.mousePresent)
        {
            cameracontrol.autoaim = true;
            mobilecontrols.active = true;
            cameracontrol.mobile = true;
            MobileInput();
        }
        else
        {
            cameracontrol.autoaim = false;
            cameracontrol.mobile = false;
            mobilecontrols.active = false;
        }

        if (!dead)
        {

            if (state==twodregion)
            {
                twodregionz = gameObject.transform.position.z;
            }
            if (walljump && gunactive)
            {
                GetComponent<ik2>().active = true;
                kin.ikActive = false;
            }
            else
            {
                GetComponent<ik2>().active = false;
                if(!rolling)
                kin.ikActive = true;
            }

            healthdisplay.rectTransform.sizeDelta = new Vector2(health * 30, 30);
           
            RaycastHit groundray;
            
            if (speed.y <= 0)
                if (!crouch)
                {
                    if (Physics.Raycast(gameObject.transform.position, new Vector3(0, -1, 0), out groundray, 0.2f, LayerMask.GetMask("Default")))
                    {
                        if (!controller.isGrounded)
                        {


                            controller.Move(new Vector3(0, -(groundray.distance + 0.1f), 0));



                        }
                    }
                }
                else
                {
                    if (Physics.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.7f, gameObject.transform.position.z), new Vector3(0, -1, 0), out groundray, 1, LayerMask.GetMask("Default")))
                    {
                        if (!controller.isGrounded)
                        {


                            controller.Move(new Vector3(0, -(groundray.distance + 0.1f), 0));



                        }
                    }
                }
                
            if (rolling)
            {
                shootable = false;
                walkleft =false;
                walkright = false;
                backwards = false;
                kin.ikActive = false;
                gunactive = false;
                if (controller.height > 0.7f)
                    controller.height -= 0.1f;
                rollcount++;
                if (rollcount > 25)
                {
                    rolling = false;
                    animator.SetBool("rolling", false);
                    if (controller.isGrounded) kin.ikActive = true;
                    rollcount = 0;
                    crouch = false;
                    controller.height = 1.5f;
                }
            }
            else
                if (!rollready)
            {

                rollcount++;
                if (rollcount > 20)
                {
                    rollready = true;
                    rollcount = 0;
                }
            }

           // target = enemylookatpos.transform.position;
            target = gameObject.transform.position;

            if (controller.isGrounded)
            {
                if(!rolling)
                shootable = true;
                /*if (Input.GetAxis("lockon") > 0.1f && !rolling)
                {
                    
                    gunactive = true;
                    kin.ikActive = true;

                    //gun.transform.localPosition = gunaimposition.transform.localPosition;
                    //gun.transform.localRotation = gunaimposition.transform.localRotation;

                }
                else
                {
                    kin.ikActive = true;
                    gunactive = true;
                   
                   // gunmaster.transform.forward =Vector3.Slerp(gunmaster.transform.forward, gameObject.transform.forward,0.3f);
                    
                   // gun.transform.localPosition = gunreadyposition.transform.localPosition;
                    //gun.transform.localRotation = gunreadyposition.transform.localRotation;

                }*/
                walljump = false;
                animator.SetBool("walljump", false);
                if (!gunactive)
                {
                    kin.ikActive = false;
                   
                }
                else
                {
                    if (!rolling)
                    {
                        kin.ikActive = true;
                        shootable = true;
                    }
                }
                animator.SetBool("jumping", false);
                if (speed.y < -0.2f)
                {

                    animator.SetBool("landing", true);
                    animator.speed = 2;
                }
                else
                    animator.SetBool("landing", false);
                speed.y = 0;
                if (!rolling)
                {
                    Vector3 moveright;
                    Vector3 moveforward;
                    Vector2 directionnormaliseright;
                    Vector2 directionnormaliseforward;

                    int speedlimiter = 9;
                    if (crouch)
                        speedlimiter = 20;
                    if (state == tps)
                    {
                        directionnormaliseright = new Vector2(camera.transform.right.x, camera.transform.right.z);
                        directionnormaliseright.Normalize();
                        directionnormaliseforward = new Vector2(camera.transform.forward.x, camera.transform.forward.z);
                        directionnormaliseforward.Normalize();
                        moveright = new Vector3(directionnormaliseright.x * horizontalinput, 0, directionnormaliseright.y * horizontalinput) / speedlimiter;
                        moveforward = new Vector3(directionnormaliseforward.x * verticalinput, 0, directionnormaliseforward.y * verticalinput) / speedlimiter;
                        Vector2 tempdirection = new Vector2(horizontalinput,verticalinput);
                        tempdirection.Normalize();
                        if(tempdirection.x<0)
                        {
                            if(tempdirection.y<0.7f || tempdirection.y >-0.7f)
                            {
                                walkleft = true;
                                walkright = false;
                                backwards = false;
                            }
                            if(tempdirection.y >=0.7f)
                            {
                                walkleft = false;
                                walkright = false;
                                backwards = false;
                            }
                            if (tempdirection.y <= -0.7f)
                            {
                                walkleft = false;
                                walkright = false;
                                backwards = true;
                            }
                        }
                        if (tempdirection.x > 0)
                        {
                            if (tempdirection.y < 0.7f || tempdirection.y > -0.7f)
                            {
                                walkleft = false;
                                walkright = true;
                                backwards = false;
                            }
                            if (tempdirection.y >= 0.7f)
                            {
                                walkleft = false;
                                walkright = false;
                                backwards = false;
                            }
                            if (tempdirection.y <= -0.7f)
                            {
                                walkleft = false;
                                walkright = false;
                                backwards = true;
                            }
                        }

                        if (tempdirection.x == 0)
                        {
                            walkleft = false;
                            walkright = false;
                            if (tempdirection.y < 0)
                                backwards = true;
                            else
                                backwards = false;
                        }


                    }
                    if (state == stickoverhead)
                    {
                        directionnormaliseright = new Vector2(1, 0);

                        directionnormaliseforward = new Vector2(0, 1);

                        moveright = new Vector3(directionnormaliseright.x * horizontalinput, 0, directionnormaliseright.y * horizontalinput) / speedlimiter;
                        moveforward = new Vector3(directionnormaliseforward.x * verticalinput, 0, directionnormaliseforward.y * verticalinput) / speedlimiter;
                       

                        Vector3 direction = new Vector3(horizontalinput, 0, verticalinput);
                       if (Vector3.Angle(camera.transform.forward, direction) > 90)
                        {
                            backwards = true;
                        }
                        else
                            backwards = false;


                    }
                    else
                    if (state == sideplatformer)
                    {
                        directionnormaliseright = new Vector2(1, 0);

                        directionnormaliseforward = new Vector2(0, 1);

                        moveright = new Vector3(0, 0, 0);
                        moveforward = new Vector3(directionnormaliseforward.x * -horizontalinput, 0, directionnormaliseforward.y * -horizontalinput) / speedlimiter;
                        Vector3 direction = new Vector3(0, 0, -horizontalinput);
                        if (Vector3.Angle(camera.transform.forward, direction) > 90)
                        {
                            backwards = true;
                        }
                        else
                            backwards = false;

                        moveright = new Vector3(0, 0, 0);
                        RaycastHit wallray;
                        Vector3 forward = Vector3.Normalize(new Vector3(speed.x, 0, speed.z));
                        if (Physics.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), moveforward, out wallray, 1, LayerMask.GetMask("Default")))
                        {
                            if (wallray.distance < 0.8f)
                            {
                                
                                    moveforward = new Vector3(0, 0, 0);
                                
                                    speed.x = 0;
                                    speed.z = 0;
                                
                            }
                        }
                    }
                    else
                    if (state == twodregion)
                    {
                        directionnormaliseright = new Vector2(0, 1);

                        directionnormaliseforward = new Vector2(1, 0);

                        moveforward = new Vector3(0, 0, 0);
                        moveright = new Vector3(directionnormaliseforward.x * horizontalinput, 0, directionnormaliseforward.y * horizontalinput) / speedlimiter;
                        Vector3 direction = new Vector3(-horizontalinput, 0,0);
                        if (Vector3.Angle(camera.transform.forward, direction) > 90)
                        {
                            backwards = false;
                        }
                        else
                            backwards = true;

                       
                        RaycastHit wallray;
                        Vector3 forward = Vector3.Normalize(new Vector3(speed.x, 0, speed.z));
                        if (Physics.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), gameObject.transform.forward, out wallray, 1, LayerMask.GetMask("Default")))
                        {
                            if (wallray.distance < 0.8f)
                            {

                                moveforward = new Vector3(0, 0, 0);

                                speed.x = 0;
                                speed.z = 0;

                            }
                        }
                    }

                    else
                    {
                        directionnormaliseright = new Vector2(camera.transform.right.x, camera.transform.right.z);
                        directionnormaliseright.Normalize();
                        directionnormaliseforward = new Vector2(camera.transform.forward.x, camera.transform.forward.z);
                        directionnormaliseforward.Normalize();
                        moveright = new Vector3(directionnormaliseright.x * horizontalinput, 0, directionnormaliseright.y * horizontalinput) / speedlimiter;
                        moveforward = new Vector3(directionnormaliseforward.x * verticalinput, 0, directionnormaliseforward.y * verticalinput) / speedlimiter;
                        if (verticalinput < 0)
                            backwards = true;
                        else
                            backwards = false;
                        if (horizontalinput < 0 && verticalinput < -0.1f)
                        {
                            backwards = true;
                        }
                    }







                    if (!gunactive)
                    {
                        backwards = false;
                        walkright = false;
                        walkleft = false;
                    }



                    Vector3 temp2Direction = moveright + moveforward;
                    animator.speed = temp2Direction.magnitude * 15;
                    if (temp2Direction.magnitude < 0.01f)
                    {
                        animator.SetBool("standing", true);
                        animator.speed = 1;
                    }
                    else
                        animator.SetBool("standing", false);

                    if (backwards)
                    {
                        animator.SetBool("forwards", false);
                        animator.SetBool("fastwalk", false);
                        animator.SetBool("walkright", false);
                        animator.SetBool("walkleft", false);
                    }
                    else if (walkright)
                    {
                        animator.SetBool("forwards", false);
                        animator.SetBool("fastwalk", false);
                        animator.SetBool("walkright", true);
                        animator.SetBool("walkleft", false);
                    }
                    else if(walkleft)
                    {
                        animator.SetBool("forwards", false);
                        animator.SetBool("fastwalk", false);
                        animator.SetBool("walkright", false);
                        animator.SetBool("walkleft", true);
                    }
                    else
                    {

                        
                        animator.SetBool("forwards", true);
                        animator.SetBool("fastwalk", false);
                        animator.SetBool("walkright", false);
                        animator.SetBool("walkleft", false);

                    }
                    speed.x = temp2Direction.x;
                    speed.z = temp2Direction.z;

                    if (crouchready)
                    {
                        if (crouchinput > 0)
                        {
                            crouch = !crouch;
                            crouchcount = 0;
                            crouchready = false;
                        }
                    }
                    else
                    {
                        crouchcount++;
                        if (crouchcount > 10)
                            crouchready = true;
                    }

                    if (crouch)
                    {
                        if (controller.height > 0.7)
                        {
                            controller.height -= 0.1f;
                        }

                    }
                    else
                    {
                        if (controller.height < 1.5)
                        {
                            controller.height += 0.1f;
                            //controller.Move(new Vector3(0, 0.1f, 0));
                        }

                    }

                    if (jumpready)
                    {
                        if (jumpinput > 0)
                        {
                            if (!jump)
                            {
                                jump = true;
                                jumpcount = 0;
                                animator.SetBool("landing", true);

                                animator.speed = 1;
                            }
                        }

                        if (jump)
                        {
                            animator.SetBool("landing", true);
                            animator.speed = 3;
                            if (jumpcount < jumpdelay)
                            {
                                jumpcount++;

                            }
                            else
                            {
                                speed.y = 0.32f;
                                speed.x = speed.x * 2.5f;
                                speed.z = speed.z * 2.5f;
                                animator.SetBool("jumping", true);
                                jump = false;
                                jumpcount = 0;
                                jumpready = false;
                                animator.speed = 1;
                              if(state == sideplatformer||state == twodregion)
                                backwards = false;
                                crouch = false;
                                controller.height = 1.5f;
                            }
                        }
                    }
                    else
                    {
                        jumpcount++;
                        if (jumpcount > 5)
                        {
                            jumpready = true;
                            jumpcount = 0;
                        }
                    }
                    if (rollready)
                    {
                        if (dodgeinput)
                        {

                            if (speed.magnitude > 0)
                            {
                                rolling = true;
                                animator.SetBool("rolling", true);
                                Vector3 normal = Vector3.Normalize(speed);
                                speed.x = normal.x / 3;
                                speed.z = normal.z / 3;
                                kin.ikActive = false;
                                backwards = false;
                                rollcount = 0;
                                animator.speed = 5;
                                rollready = false;
                                shootable = false;
                            }
                            else
                            {
                                rolling = true;
                                animator.SetBool("rolling", true);
                                speed.x = gameObject.transform.forward.x/3;
                                speed.z = gameObject.transform.forward.z/3;
                                kin.ikActive = false;
                                backwards = false;
                                rollcount = 0;
                                animator.speed = 5;
                                rollready = false;
                                shootable = false;
                            }
                         

                        }
                    }

                }

            }



            else
            {
                walkleft = false;
                walkright = false;
                backwards = false;
                kin.ikActive = false;
                gunactive = false;
                shootable = false;
                if (!rolling)
                {
                    Vector3 moveright;
                    Vector3 moveforward;
                    Vector2 directionnormaliseright;
                    int speedlimiter = 25;
                    Vector2 directionnormaliseforward;
                    if (state == tps)
                    {
                        directionnormaliseright = new Vector2(camera.transform.right.x, camera.transform.right.z);
                        directionnormaliseright.Normalize();
                        directionnormaliseforward = new Vector2(camera.transform.forward.x, camera.transform.forward.z);
                        directionnormaliseforward.Normalize();


                        moveright = new Vector3(directionnormaliseright.x * horizontalinput, 0, directionnormaliseright.y * horizontalinput) / speedlimiter;
                        moveforward = new Vector3(directionnormaliseforward.x * verticalinput, 0, directionnormaliseforward.y * verticalinput) / (speedlimiter);

                        Vector3 direction = new Vector3(speed.x, 0, speed.z);
                        if (gunactive)
                        {
                            if (verticalinput < 0)
                                backwards = true;
                            else
                                backwards = false;
                            if (horizontalinput < 0 && verticalinput < -0.1f)
                            {
                                backwards = true;
                            }
                        }

                    }
                    if (state == stickoverhead)
                    {

                        directionnormaliseforward = new Vector2(0, 1);
                        directionnormaliseright = new Vector2(1, 0);


                        moveright = new Vector3(directionnormaliseright.x * horizontalinput, 0, directionnormaliseright.y * horizontalinput) / speedlimiter;
                        moveforward = new Vector3(directionnormaliseforward.x * verticalinput, 0, directionnormaliseforward.y * verticalinput) / (speedlimiter);
                        Vector3 direction = new Vector3(speed.x, 0, speed.z);
                        if(gunactive)
                        if (Vector3.Angle(camera.transform.forward, direction) > 90)
                        {
                            backwards = true;
                        }
                        else
                            backwards = false;
                    }
                    else
                    if (state == sideplatformer || state==twodregion)
                    {
                        
                        moveright = new Vector3(0, 0, 0);
                        if (walljump)
                        {
                            RaycastHit wallray;
                            if (Physics.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z), gameObject.transform.forward, out wallray, 1, LayerMask.GetMask("camerablockable")))
                            {
                                if (wallray.distance > 3)
                                {

                                    walljump = false;
                                    animator.SetBool("walljump", false);
                                    animator.SetBool("jumping", true);
                                    jumpready = false;
                                    jumpcount = 0;
                                    moveforward = new Vector3(0, 0, 0);
                                }
                                else
                                {

                                    float movedistance = 0.69f - wallray.distance;


                                    if (state == sideplatformer)
                                        controller.Move(new Vector3(0, 0, movedistance * -gameObject.transform.forward.z));
                                    else
                                    {
                                        float directions;
                                        if (wallray.point.x < gameObject.transform.position.x)
                                            directions = -movedistance*2;
                                        else
                                            directions = movedistance*2;
                                        controller.Move(new Vector3(movedistance * gameObject.transform.right.x, 0, 0));
                                    }
                                }
                            }
                            else
                            {
                               
                                walljump = false;
                                animator.SetBool("walljump", false);
                                animator.SetBool("jumping", true);
                                jumpready = false;
                                jumpcount = 0;
                                moveforward = new Vector3(0, 0, 0);
                            }

                            moveforward = new Vector3(0, 0, 0);

                            jumpcount++;
                            if(jumpcount > 20)
                            if (jumpinput > 0)
                            {

                                speed.y = 0.32f;
                             
                                animator.SetBool("jumping", true);
                              
                                jumpcount = 0;
                                jumpready = false;
                                animator.speed = 1;
                               
                               
                                backwards = false;
                                crouch = false;
                                controller.height = 1.5f;
                                animator.SetBool("walljump", false);
                                    walljump = false;
                                    animator.speed = 1;
                                    moveforward = -gameObject.transform.forward/2;
                                    if(state == twodregion)
                                    {
                                        moveforward.z = 0;
                                        if (moveforward.x < 0)
                                            moveforward.x = -0.5f;
                                        else
                                            moveforward.x = 0.5f;
                                    }
                            }

                        }
                        else
                        {
                            if (state == sideplatformer)
                            {
                                directionnormaliseforward = new Vector2(0, 1);
                                directionnormaliseright = new Vector2(1, 0);
                            }
                            else
                                directionnormaliseforward = new Vector2(-1, 0);
                            directionnormaliseright = new Vector2(0,-1);


                            moveforward = new Vector3(directionnormaliseforward.x * -horizontalinput, 0, directionnormaliseforward.y * -horizontalinput) / (speedlimiter*2);
                            ////walljump test
                            RaycastHit wallray;
                            if (Physics.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+0.1f, gameObject.transform.position.z), gameObject.transform.forward, out wallray, 1, LayerMask.GetMask("camerablockable")))
                            {
                                
                                if (wallray.distance <= 0.7f)
                                {
                                   
                                    speed.x = 0;
                                    speed.z = 0;
                                   
                                    moveforward = new Vector3(0, 0, 0);
                                    float movedistance= 0.69f - wallray.distance; 
                                    
                                   
                                    if (state == sideplatformer)
                                    controller.Move(new Vector3(0, 0, movedistance * -gameObject.transform.forward.z));
                                    else
                                                                   
                                    {
                                        float directions;
                                        if (wallray.point.x < gameObject.transform.position.x)
                                            directions = -movedistance*2;
                                        else
                                            directions = movedistance*2;
                                        controller.Move(new Vector3(movedistance * gameObject.transform.right.x, 0, 0));
                                    }
                                    if (speed.y < 0)
                                    {
                                      
                                        walljump = true;
                                        animator.SetBool("walljump", true);
                                        animator.SetBool("jumping", false);
                                        jumpready = false;
                                        jumpcount = 0;
                                       

                                    }
                                }
                            }

                        }


                    }

                    else
                    {
                        directionnormaliseright = new Vector2(camera.transform.right.x, camera.transform.right.z);
                        directionnormaliseright.Normalize();
                        directionnormaliseforward = new Vector2(camera.transform.forward.x, camera.transform.forward.z);
                        directionnormaliseforward.Normalize();


                        moveright = new Vector3(directionnormaliseright.x * horizontalinput, 0, directionnormaliseright.y * horizontalinput) / speedlimiter;
                        moveforward = new Vector3(directionnormaliseforward.x * verticalinput, 0, directionnormaliseforward.y * verticalinput) / (speedlimiter);
                    }

                    speed.x += moveright.x;
                    speed.z += moveright.z;
                    speed.x += moveforward.x;
                    speed.z += moveforward.z;
                    speed.x *= 0.9f;
                    speed.z *= 0.9f;
                }
                if (!walljump)
                { 
                    if (speed.y > -5)
                        speed.y -= gravity;
                }
                else
                {
                    speed.y = -0.01f;
                    float ybutton = verticalinput / 10;
                    if (ybutton < 0)
                        speed.y += ybutton;
                }
                animator.SetBool("landing", false);
                if (speed.y < -0.2f)
                {
                    
                   if(state == sideplatformer)
                    backwards = false;
                    if (crouch)
                    {
                        crouch = false;
                        controller.height = 1.5f;
                    }
                    /* }
                     if (speed.y < -gravity)
                     {*/
                    animator.SetBool("jumping", true);
                    if (!rolling)
                        animator.speed = 1;
                }
                if (!rolling)
                {
                    //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z), Vector3.up), 0.5f);
                }
            }
            if (!pushed)
            {
                if (speed.x != 0 || speed.z != 0)
                {
                    if (!backwards && !walkleft && !walkright)
                        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(speed.x, 0, speed.z), Vector3.up), 0.5f);
                    if(backwards)
                        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(-speed.x, 0, -speed.z), Vector3.up), 0.5f);
                    if (walkleft)
                        gameObject.transform.right = Vector3.Slerp(gameObject.transform.right,new Vector3(-speed.x,0,-speed.z),0.5f);
                        //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(-speed.z, 0, -speed.x/2), Vector3.up), 0.5f);
                    if (walkright)
                        gameObject.transform.right = Vector3.Slerp(gameObject.transform.right, new Vector3(speed.x, 0, speed.z), 0.5f);
                    //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(-speed.z, 0, speed.x/2), Vector3.up), 0.5f);
                }
                else
                    if (gunactive && !walljump && controller.isGrounded)
                    gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z), Vector3.up), 0.5f);
            }
            else
            {
                speed = pusheddirection;
                pushedcount++;
                if (pushedcount > 10)
                {
                    pushed = false;
                    pushedcount = 0;
                }
            }
            if (!jump)
                controller.Move(speed);
            if (state == sideplatformer)
                gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
            if (state == twodregion)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, twodregionz);
            cameratarget.transform.position = new Vector3(camerabone.transform.position.x, camerabone.transform.position.y, camerabone.transform.position.z);
            if (overheadtarget != null)
            {
                overheadtarget.transform.position = new Vector3(camerabone.transform.position.x, camerabone.transform.position.y, camerabone.transform.position.z);
            }
            if (health <= 0)
            {
                health = 0;
                die();
            }

            if (!gunactive)
                kin.ikActive = false;
            if (gunactive)
            {
               gunmaster.transform.LookAt(gunaim.transform.position);
            }
        }
        if(dead)
        {
            deathcount++;
            if(deathcount>20)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        
	}
}
