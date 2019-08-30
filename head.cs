using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head : baddie
{
    public float activationdistance;
    public float movementspeed;
    Vector3 speed;
    bool activated;
    public GameObject player;
    public bool twodee;
    enemygun gun;
    public GameObject gunobject;
    int bulletcount;
    public GameObject splosion;
    public GameObject levelmaster;
    public int bulletinterval;
    public override void takehit(int strength)
    {
        enemynumber--;
        Instantiate(splosion,gameObject.transform.position,new Quaternion()).transform.parent=levelmaster.transform;

        Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
        gun = gunobject.GetComponent<enemygun>();
        attacking = false;
        enemynumber++;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!dead)
        {

            if (!activated)
            {
                if (Vector3.Distance(gameObject.transform.position, playermovement.target) < activationdistance)
                {
                    activated = true;
                    //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;



                }
            }
            else
            {
                bulletcount++;
                if(bulletcount>bulletinterval)
                {
                    gun.Shoot();
                    bulletcount = 0;
                }
                Vector3 direction = gameObject.transform.position- player.transform.position;
                direction.Normalize();
                gameObject.transform.up = Vector3.RotateTowards(gameObject.transform.up, direction,0.1f,0.1f);
               
                    
                speed = speed / 3;
                speed = new Vector3(speed.x - (gameObject.transform.up.x * movementspeed), speed.y - (gameObject.transform.up.y * movementspeed), speed.z - (gameObject.transform.up.z * movementspeed));

                gun.gameObject.transform.LookAt(player.transform.position);
                if(twodee)
                {
                    gameObject.transform.position = new Vector3(0, gameObject.transform.position.y + speed.y, gameObject.transform.position.z + speed.z);
                    
                }
                else
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + speed.x, gameObject.transform.position.y + speed.y, gameObject.transform.position.z + speed.z);

                }
            }
        }
        }
}
