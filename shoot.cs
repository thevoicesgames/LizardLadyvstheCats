using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
    public GameObject muzzleflash;
    public GameObject flashposition;
    public GameObject camera;
    public Collider coll;
    bool shooting;
    public GameObject levelrotate;
    public GameObject bullet;
    public GameObject defaultdirection;
    public UnityEngine.UI.Image reticle;
    public UnityEngine.UI.Image staminabar;
    float stamina;
    int maxstamina;
    int activecount;
    public Vector3 direction;
   public bool twodplatformer;
    public GameObject aimdirection;
    // Use this for initialization
    void Start () {
        maxstamina = 10;
        stamina = maxstamina;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        activecount++;
        if(activecount > 400)
        {
            playermovement.kin.ikActive = false;
            playermovement.gunactive = false;
        }
        if(stamina < maxstamina)
        {
            stamina += 0.015f;
            if (stamina > maxstamina)
                stamina = maxstamina;
        }
        if(stamina < 1)
        {
            staminabar.color = new Color(1, 0, 0);
        }
        else
            staminabar.color = new Color(1, 1, 1);
        staminabar.rectTransform.sizeDelta = new Vector2(stamina * 30, 30);
        //if (playermovement.shootable)
        //{
            if (!twodplatformer)
            {
                RaycastHit hit;
                Vector3 direction;
                if (Physics.Raycast(camera.transform.position+(camera.transform.forward*(Vector3.Distance(camera.transform.position,playermovement.target))), camera.transform.forward, out hit, 200, LayerMask.GetMask("Default")))
                {
                    direction = hit.point - flashposition.transform.position;
                    direction.Normalize();

                if (hit.collider.gameObject.tag == "baddie" || hit.collider.gameObject.tag == "twistedclone")
                {
                    reticle.color = new Color(1, 0, 0);
                    coll = hit.collider;
                }
                else
                {
                    reticle.color = new Color(1, 1, 1);
                    coll = null;
                }

                aimdirection.transform.position = hit.point;
                }
                else
                {
                    reticle.color = new Color(1, 1, 1);
                    direction = defaultdirection.transform.position - flashposition.transform.position;
                    direction.Normalize();
                coll = null;

                }

                if (playermovement.shootinput > 0.1 && stamina >=1 && playermovement.shootable)
                {
                    if (shooting == false)
                    {
                    playermovement.kin.ikActive = true;
                    playermovement.gunactive = true;
                    activecount = 0;
                        shooting = true;
                        stamina -= 1;
                    if(coll!=null)
                    {
                        if (coll.gameObject.tag == "baddie" || coll.gameObject.tag == "twistedclone")
                        {
                            coll.gameObject.GetComponent<baddie>().takehit(1);
                        }
                    }
                        GameObject bull = Instantiate(bullet, flashposition.transform.position, flashposition.transform.rotation);
                        
                            bull.transform.parent = gameObject.transform;

                       // bull.GetComponent<freebullet>().direction = direction;




                    }
                }
                else
                    shooting = false;
           // }
           if(twodplatformer)
            {
                if (playermovement.shootinput > 0.1 && stamina >= 1)
                {
                    if (shooting == false)
                    {
                        shooting = true;
                        stamina -= 1;
                        //GameObject gam = Instantiate(muzzleflash, flashposition.transform.position, flashposition.transform.rotation);
                        //gam.transform.parent = flashposition.transform;

                        GameObject bull = Instantiate(bullet, flashposition.transform.position, flashposition.transform.rotation);
                        if(levelrotate !=null)
                        bull.transform.parent = levelrotate.transform;


                    }
                }
                else
                    shooting = false;
            }


        }
    }
}

