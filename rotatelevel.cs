using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatelevel : MonoBehaviour {
    public bool sphere;
    public bool cube;
    bool left;
    
    public GameObject spheregravityregion;
    public GameObject cubegravityregion;
    public GameObject player;
    public UnityEngine.UI.Text tex;
    public UnityEngine.UI.Text tex2;
    playermovement players;
    public bool incubegravity;
    public bool inspheregravity;
    public bool reversesphere;
    public bool cylinder;
    public bool fixedplane;
    public bool sideways;
    // Use this for initialization
    void Start () {
        players = player.GetComponent<playermovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (players.state == playermovement.sideplatformer)
        {
            if (sphere && !cube)
            {
               
                    Vector3 direction = playermovement.target - spheregravityregion.gameObject.transform.position;
                    direction.Normalize();
                    if (reversesphere)
                    {
                        direction = -direction;
                    }
                    //Quaternion qua = Quaternion.Slerp(Quaternion.)
                    float down = Vector3.Angle(direction, Vector3.up);
                    Vector3 cros = Vector3.Cross(direction, Vector3.up);

                    down = down * Mathf.Deg2Rad;
                    if (cros.x > 0)
                    {
                        left = false;

                    }
                    if (cros.x < 0)
                    {
                        left = true;

                    }
                    if (left)
                    {
                        down = -down;
                    }
                    gameObject.transform.RotateAround(playermovement.target, new Vector3(1, 0, 0), down * 10);
                    if (!players.controller.isGrounded && !playermovement.rolling)
                    {
                        Quaternion rotation = Quaternion.Euler(down * 10, 0, 0);
                        players.speed = rotation * players.speed;
                    }
                                

            }
            if (cube)
            {

                if (cubegravityregion.transform.up != Vector3.up)
                {
                    

                    
                    Vector3 direction = (gameObject.transform.up - Vector3.up) - cubegravityregion.transform.up;
                    // Vector3 direction =  (gravityregion.transform.up-gameObject.transform.up)-Vector3.up;
                    direction.Normalize();

                    float down = Vector3.Angle(cubegravityregion.transform.up, Vector3.up);
                    Vector3 cros = Vector3.Cross(cubegravityregion.transform.up, Vector3.up);

                    down = down * Mathf.Deg2Rad;

                    if (cros.x > 0)
                    {
                        left = false;

                    }
                    if (cros.x < 0)
                    {
                        left = true;

                    }

                    
                    if (left)
                    {
                        down = -down;
                    }
                    gameObject.transform.RotateAround(playermovement.target, new Vector3(1, 0, 0), down*5);
                    if (!players.controller.isGrounded)
                    {
                        Quaternion rotation = Quaternion.Euler(down*5, 0, 0);
                        players.speed = rotation * players.speed;
                    }
                }
            }
        }





            if (players.state == playermovement.stickoverhead || players.state == playermovement.tps)
            {
            if (sphere && !cube)
            {
                if (!cylinder)
                {
                    Vector3 direction = playermovement.target - spheregravityregion.gameObject.transform.position;
                    direction.Normalize();
                    if (reversesphere)
                    {
                        direction = -direction;
                    }
                    //Quaternion qua = Quaternion.Slerp(Quaternion.)
                    float down = Vector3.Angle(direction, Vector3.up);
                    Vector3 cros = Vector3.Cross(direction, Vector3.up);

                    down = down * Mathf.Deg2Rad;
                    if (cros.x > 0)
                    {
                        left = false;

                    }
                    if (cros.x < 0)
                    {
                        left = true;

                    }
                    /* if (left)
                     {
                         down = -down;
                     }*/
                    gameObject.transform.RotateAround(playermovement.target, new Vector3(cros.x, cros.y, cros.z), down * 10);
                    if (!players.controller.isGrounded && !playermovement.rolling)
                    {
                        Quaternion rotation = Quaternion.Euler((down * 10) * cros.x, (down * 10) * cros.y, (down * 10) * cros.z);
                        players.speed = rotation * players.speed;
                    }
                }

                else
                {
                    if (!sideways)
                    {
                        Vector3 direction = new Vector3(playermovement.target.x, playermovement.target.y, 0) - new Vector3(spheregravityregion.gameObject.transform.position.x, spheregravityregion.gameObject.transform.position.y, 0);

                        direction.Normalize();
                        if (reversesphere)
                        {
                            direction = -direction;
                        }
                        //Quaternion qua = Quaternion.Slerp(Quaternion.)
                        float down = Vector3.Angle(direction, Vector3.up);
                        Vector3 cros = Vector3.Cross(direction, Vector3.up);

                        down = down * Mathf.Deg2Rad;
                        if (cros.z > 0)
                        {
                            left = false;

                        }
                        if (cros.z < 0)
                        {
                            left = true;

                        }
                        if (left)
                        {
                            down = -down;
                        }
                        gameObject.transform.RotateAround(playermovement.target, new Vector3(0, 0, 1), down * 10);
                        if (!players.controller.isGrounded && !playermovement.rolling)
                        {
                            Quaternion rotation = Quaternion.Euler(0, 0, down * 10);
                            players.speed = rotation * players.speed;
                        }


                        if (spheregravityregion.transform.up != Vector3.forward)
                        {
                            Vector3 direction2 = Vector3.forward - spheregravityregion.transform.up;
                            float downs = Vector3.Angle(Vector3.forward, spheregravityregion.transform.up);
                            Vector3 cros2 = Vector3.Cross(Vector3.forward, spheregravityregion.transform.up);
                            downs = downs * Mathf.Deg2Rad;
                            gameObject.transform.RotateAround(playermovement.target, new Vector3(-cros2.x, -cros2.y, -cros2.z), downs * 5);


                        }

                    }
                    else
                    {
                        Vector3 direction = new Vector3(0, playermovement.target.y, playermovement.target.z) - new Vector3(0, spheregravityregion.gameObject.transform.position.y, spheregravityregion.gameObject.transform.position.z);

                        direction.Normalize();
                        if (reversesphere)
                        {
                            direction = -direction;
                        }
                        //Quaternion qua = Quaternion.Slerp(Quaternion.)
                        float down = Vector3.Angle(direction, Vector3.up);
                        Vector3 cros = Vector3.Cross(direction, Vector3.up);

                        down = down * Mathf.Deg2Rad;
                        if (cros.x > 0)
                        {
                            left = false;

                        }
                        if (cros.x < 0)
                        {
                            left = true;

                        }
                        if (left)
                        {
                            down = -down;
                        }
                        gameObject.transform.RotateAround(playermovement.target, new Vector3(1, 0, 0), down * 10);
                        if (!players.controller.isGrounded && !playermovement.rolling)
                        {
                            Quaternion rotation = Quaternion.Euler(down * 10, 0, 0);
                            players.speed = rotation * players.speed;
                        }


                        if (spheregravityregion.transform.up != Vector3.left)
                        {
                            Vector3 direction2 = Vector3.forward - spheregravityregion.transform.up;
                            float downs = Vector3.Angle(Vector3.left, spheregravityregion.transform.up);
                            Vector3 cros2 = Vector3.Cross(Vector3.left, spheregravityregion.transform.up);
                            downs = downs * Mathf.Deg2Rad;
                            gameObject.transform.RotateAround(playermovement.target, new Vector3(-cros2.x, -cros2.y, -cros2.z), downs * 5);


                        }

                    }
                }

            }
        



                
                if (cube)
                {
                float down = 0.01f;
                if (cubegravityregion.transform.up != Vector3.up)
                {



                    // Vector3 direction =  (gravityregion.transform.up-gameObject.transform.up)-Vector3.up;


                    down = Vector3.Angle(cubegravityregion.transform.up, Vector3.up);
                    Vector3 cros = Vector3.Cross(cubegravityregion.transform.up, Vector3.up);

                    down = down * Mathf.Deg2Rad;




                    /*if (left)
                    {
                        down = -down;
                    }
                    */

                    gameObject.transform.RotateAround(playermovement.target, new Vector3(cros.x, cros.y, cros.z), down * 5);
                    // gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
                    if (!players.controller.isGrounded && !playermovement.rolling)
                    {
                        Quaternion rotation = Quaternion.Euler((down * 5) * cros.x, (down * 5) * cros.y, (down * 5) * cros.z);
                        players.speed = rotation * players.speed;
                    }


                }
               
                    if (fixedplane)
                    {
                        if (cubegravityregion.transform.forward != Vector3.forward)
                        {
                            Vector3 direction2 = Vector3.forward - cubegravityregion.transform.forward;
                            float downs = Vector3.Angle(Vector3.forward, cubegravityregion.transform.forward);
                            Vector3 cros2 = Vector3.Cross(Vector3.forward, cubegravityregion.transform.forward);
                            downs = downs * Mathf.Deg2Rad;
                            gameObject.transform.RotateAround(playermovement.target, new Vector3(-cros2.x, -cros2.y, -cros2.z), downs * 5);


                        }
                    }
                
                
               
               
                }
            
            }
        }
}
