using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class cameracontrol : MonoBehaviour {
    public GameObject secondtarget;
 
    public bool overheadstick;
    public bool sidestick;
    public bool twodregion;
    float zpos;
    static bool invert = true;
    public GameObject camera;
    public GameObject camera2;
    public UnityEngine.UI.Text distance;
    public GameObject clipposition;
    public GameObject directy;
    public GameObject bulletorigin;
    Vector3 camerapos;
    public static bool locked;
    bool cameralocked;
    Quaternion camerarotation;
    public GameObject player;
    float xangle;
    public static bool autoaim;
    GameObject autoaimenemy;
    bool mouseactivated;
    public UnityEngine.UI.Image reticle;
    public static bool mobile;
  
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "baddie")
        {
            if (autoaimenemy == null)
            {
                if(!other.gameObject.GetComponent<baddie>().dead)
                autoaimenemy = other.gameObject;
            }
            else
                if (Vector3.Distance(autoaimenemy.transform.position, gameObject.transform.position) > Vector3.Distance(other.transform.position, gameObject.transform.position) && !other.gameObject.GetComponent<baddie>().dead)
            {
                autoaimenemy = other.gameObject;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "baddie")
        {
            if (autoaimenemy == null)
            {
                if (!other.gameObject.GetComponent<baddie>().dead)
                    autoaimenemy = other.gameObject;
            }
            else
                if (Vector3.Distance(autoaimenemy.transform.position, gameObject.transform.position) > Vector3.Distance(other.transform.position, gameObject.transform.position) && !other.gameObject.GetComponent<baddie>().dead)
            {
                autoaimenemy = other.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == autoaimenemy)
            autoaimenemy = null;
    }
    // Use this for initialization
    void Start () {
        xangle = -0.5f;
        zpos = gameObject.transform.position.z;
        camerapos = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z);
        camerarotation = camera.transform.localRotation;
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (sidestick || overheadstick)
        {
            if (!autoaim)
            {
                Cursor.lockState = CursorLockMode.None;
                if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
                {

                    mouseactivated = true;
                    reticle.gameObject.active = true;
                }
                if (Input.GetAxis("Stick Y") != 0 || Input.GetAxis("Stick X") != 0)
                {
                    mouseactivated = false;
                    reticle.gameObject.active = false;
                }
                if (mouseactivated)
                    reticle.rectTransform.localPosition = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, reticle.rectTransform.localPosition.z);
            }
            else
                reticle.enabled = false;
        }
        else
            Cursor.lockState = CursorLockMode.Locked;

            if (autoaim)
            if(autoaimenemy!=null)
            if (autoaimenemy.GetComponent<baddie>().dead)
                autoaimenemy = null;
        if (!overheadstick && !sidestick)
        {
            if (Input.GetAxis("lockon") > 0.1 && lockon.locktarget != null)
            {
               
                    cameralocked = true;
                    gameObject.transform.forward = Vector3.Slerp(gameObject.transform.forward, new Vector3(lockon.locktarget.transform.position.x - gameObject.transform.position.x, 0, lockon.locktarget.transform.position.z - gameObject.transform.position.z), 0.5f);
                    gameObject.transform.up = Vector3.up;
                    secondtarget.transform.forward = Vector3.Slerp(secondtarget.transform.forward, new Vector3(lockon.locktarget.transform.position.x - secondtarget.transform.position.x, (lockon.locktarget.GetComponent<Collider>().transform.position.y+1) - secondtarget.transform.position.y, lockon.locktarget.transform.position.z - secondtarget.transform.position.z), 0.5f);
                camera.transform.up = Vector3.up;
                    camera.transform.forward = new Vector3(lockon.locktarget.transform.position.x - camera.transform.position.x, (lockon.locktarget.GetComponent<Collider>().transform.position.y+1) - camera.transform.position.y, lockon.locktarget.transform.position.z - camera.transform.position.z);
                
                //gameObject.transform.rotation = new Quaternion(0, gameObject.transform.rotation.y, 0, 1);
                    locked = true;
                
            }
            else
            {
                locked = false;
                lockon.locktarget = null;
                if (cameralocked)
                {
                   // camera.transform.localRotation = camerarotation;
                    cameralocked = false;
                }

                camera.transform.localRotation = Quaternion.Slerp(camera.transform.localRotation, camerarotation,0.3f);

                float yrot = 0;
                if (mobile)
                {
                    if (-CrossPlatformInputManager.GetAxis("StickY") < 0)
                    {

                        if (secondtarget.transform.localRotation.x > -0.5f)
                            yrot = -CrossPlatformInputManager.GetAxis("StickY");
                    }
                    if (-CrossPlatformInputManager.GetAxis("StickY") > 0)
                    {
                        if (secondtarget.transform.localRotation.x < 0.5f)
                            yrot = -CrossPlatformInputManager.GetAxis("StickY");
                    }

                    transform.Rotate(0, CrossPlatformInputManager.GetAxis("StickX")/2, 0);

                    secondtarget.transform.Rotate(yrot/2, 0, 0);
                }
                else
                {
                    if (Input.GetAxis("Mouse Y") > 0)
                    {

                        if (secondtarget.transform.localRotation.x < 0.5f)
                            yrot = Input.GetAxis("Mouse Y") * 2;
                    }
                    if (Input.GetAxis("Mouse Y") < 0)
                    {
                        if (secondtarget.transform.localRotation.x > -0.5f)
                            yrot = Input.GetAxis("Mouse Y") * 2;
                    }

                    if (-Input.GetAxis("Stick Y") < 0)
                    {

                        if (secondtarget.transform.localRotation.x > -0.5f)
                            yrot = -Input.GetAxis("Stick Y");
                    }
                    if (-Input.GetAxis("Stick Y") > 0)
                    {
                        if (secondtarget.transform.localRotation.x < 0.5f)
                            yrot = -Input.GetAxis("Stick Y");
                    }

                    transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);
                    transform.Rotate(0, Input.GetAxis("Stick X") * 2, 0);

                    secondtarget.transform.Rotate(yrot, 0, 0);
                }
            }
            
                camera.transform.localPosition = new Vector3(camerapos.x, camerapos.y, camerapos.z);

            /* RaycastHit hit;
             if (Physics.Raycast(clipposition.transform.position, -secondtarget.transform.forward, out hit, 200, LayerMask.GetMask("camerablockable")))
             {

                 if (-hit.distance > camera.transform.localPosition.z)
                 {
                     //camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, -hit.distance+1);

                     camera.transform.position = Vector3.MoveTowards(camera.transform.position, secondtarget.transform.position, -camera.transform.localPosition.z - (hit.distance));
                 }

             }*/
            RaycastHit hit;

            directy.transform.position = clipposition.transform.position;
            directy.transform.LookAt(camera.transform.position);
            if (Physics.Raycast(clipposition.transform.position, directy.transform.forward, out hit, 200, LayerMask.GetMask("camerablockable")))
            {

                if (-hit.distance > camera.transform.localPosition.z-0.4f)
                {
                    //camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, -hit.distance+1);

                    camera.transform.position = Vector3.MoveTowards(camera.transform.position, clipposition.transform.position, -(camera.transform.localPosition.z-0.4f) - (hit.distance));
                }

            }
           
           /* RaycastHit hit2;
            if (Physics.Raycast(secondtarget.transform.position, secondtarget.transform.right, out hit2, 200, LayerMask.GetMask("camerablockable")))
            {

                //if (hit.collider.gameObject.tag == "camerablockable")
                if (hit2.distance-2 < camera.transform.localPosition.x)
                {
                    camera.transform.position = Vector3.MoveTowards(camera.transform.position, secondtarget.transform.position, -camera.transform.localPosition.z - (hit2.distance));

                }
            }*/

            bool overlap = Physics.CheckSphere(camera.transform.position, 0.25f, LayerMask.GetMask("camerablockable"));

            int overlapcount = 0;
             if(overlap)
             while (overlap)
               {
                  if (camera.transform.localPosition.z < 0)
                   {
                        camera.transform.localPosition = new Vector3(camera.transform.localPosition.x - (camera.transform.localPosition.x/20), camera.transform.localPosition.y, camera.transform.localPosition.z -(camera.transform.localPosition.z/20));
                        overlap = Physics.CheckSphere(camera.transform.position, 0.25f, LayerMask.GetMask("camerablockable"));
                        overlapcount++;
                        if(overlapcount>100)
                        {
                            overlap = false;
                        }
             }
             else overlap = false;
            }



        }
        if (twodregion)
        {
            if (!autoaim)
            {
                


                float xangle = Input.GetAxis("Stick X");
                float yangle = Input.GetAxis("Stick Y");
                
                if(mouseactivated)
                {
                    RaycastHit hit;
                    Ray ray = camera2.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("aimplane")))
                    {
                        secondtarget.transform.LookAt(hit.point);
                        bulletorigin.transform.LookAt(hit.point);
                    }


                }
                else
                if (xangle != 0 || yangle != 0)
                {
                    secondtarget.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, yangle, 0), Vector3.up);
                    bulletorigin.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, yangle, 0), Vector3.up);
                }
            }
            else
            {
                if (autoaimenemy != null)
                {
                    secondtarget.transform.LookAt(autoaimenemy.transform.position);
                }
                else
                {
                    float xangle = playermovement.horizontalinput;
                    if (player.GetComponent<playermovement>().walljump)
                        xangle = -player.gameObject.transform.forward.x;
                    if (xangle != 0)
                        secondtarget.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, 0, 0), Vector3.up);
                }

            }
        }
        else
        if (overheadstick)
        {
            if (!autoaim)
            {
                float xangle = Input.GetAxis("Stick X");
                float yangle = Input.GetAxis("Stick Y");
                if (mouseactivated)
                {
                    xangle = (Input.mousePosition.x-Screen.width/2) + gameObject.transform.position.x;
                    yangle = (Input.mousePosition.y-Screen.height/2) + gameObject.transform.position.y;
                    
                    RaycastHit hit;
                    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //if(Physics.Raycast(ray, out hit,200))
                    // Vector3 direction;
                    Ray ray = camera2.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray,out hit,200, LayerMask.GetMask("Default")))
                    //if (Physics.Raycast((camera2.transform.position + (camera2.transform.right*reticle.transform.position.x))+(camera.transform.up*reticle.transform.position.y), camera2.transform.forward, out hit, 200, LayerMask.GetMask("baddie")))
                    {

                        if (hit.collider.gameObject.tag == "baddie" || hit.collider.gameObject.tag == "twistedclone")
                        {
                            reticle.color = new Color(1, 0, 0);
                        }
                        else
                            reticle.color = new Color(1, 1, 1);

                        gameObject.transform.LookAt(hit.point);
                        bulletorigin.transform.LookAt(hit.point);
                        //}
                        /*else
                        {
                            reticle.color = new Color(1, 1, 1);
                            gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, 0, yangle), Vector3.up);
                        }*/
                    }
                    else

                    {
                        reticle.color = new Color(1, 1, 1);
                        gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, 0, yangle), Vector3.up);

                    }
                }
                else
                {
                    if (xangle != 0 || yangle != 0)
                    {
                        gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, 0, yangle), Vector3.up);
                        bulletorigin.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, 0, yangle), Vector3.up);


                    }
                    RaycastHit hit2;
                    if (Physics.Raycast(bulletorigin.transform.position, gameObject.transform.forward, out hit2, 200, LayerMask.GetMask("lockonregion")))
                    {

                        gameObject.transform.LookAt(hit2.transform.position);
                        bulletorigin.transform.LookAt(hit2.transform.position);
                       
                    }
                }
            }
            else
            {
                if(autoaimenemy!=null)
                {
                    gameObject.transform.LookAt(autoaimenemy.gameObject.transform.position);
                }
                else
                {
                    float xangle = playermovement.horizontalinput;
                    float yangle = playermovement.verticalinput;
                    if (xangle != 0 || yangle != 0)
                    {
                        gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(xangle, 0, yangle), Vector3.up);



                    }
                }
            }


        }

        if (sidestick)
        {

            if (!autoaim)
            {
                


                float xangle = -Input.GetAxis("Stick X");
                float yangle = Input.GetAxis("Stick Y");

                if (mouseactivated)
                {
                    RaycastHit hit;
                    Ray ray = camera2.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("aimplane")))
                    {
                        secondtarget.transform.LookAt(hit.point);
                        bulletorigin.transform.LookAt(hit.point);
                    }

                }
              else

                if (xangle != 0 || yangle != 0)
                    secondtarget.transform.rotation = Quaternion.LookRotation(new Vector3(0, yangle, xangle), Vector3.up);

            }
            else
            {
                if (autoaimenemy != null)
                {
                    secondtarget.transform.LookAt(autoaimenemy.transform.position);
                }
                else
                {
                    float xangle = -playermovement.horizontalinput;
                    if (player.GetComponent<playermovement>().walljump)
                        xangle = player.gameObject.transform.forward.x;
                    if (xangle != 0)
                        secondtarget.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, xangle), Vector3.up);
                }

            }
        }
    }
        void LateUpdate()
        {
      /* if (!overheadstick && !sidestick)
        {
            bool overlap = Physics.CheckSphere(camera.transform.position, 0.25f, LayerMask.GetMask("camerablockable"));


            if (overlap)
                while (overlap)
                {
                    if (camera.transform.localPosition.z < 0)
                    {
                        camera.transform.localPosition = new Vector3(camera.transform.localPosition.x - (camera.transform.localPosition.x / 10), camera.transform.localPosition.y, camera.transform.localPosition.z - (camera.transform.localPosition.z / 10));
                        overlap = Physics.CheckSphere(camera.transform.position, 0.25f, LayerMask.GetMask("camerablockable"));
                    }
                    else overlap = false;
                }
        }*/
        

    }
      
    
}
