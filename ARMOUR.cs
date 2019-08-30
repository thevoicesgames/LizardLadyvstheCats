using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMOUR : baddie {

    public GameObject parent;
    public int power;
    baddie bad;
    
    
    public override void takehit(int strength)
    {
     if(!dead)
        {
            health -= strength;
            if (health <= 0)
            {
                dead = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
                
        }
     else
        bad.takehit(strength * power);
    }
    // Use this for initialization
    void Start()
    {
        bad = parent.GetComponent<baddie>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!dead)
        {
            if (health <= 0)
            {
                dead = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
        if (bad.dead)
            gameObject.GetComponent<Collider>().enabled = false;
    }
}
