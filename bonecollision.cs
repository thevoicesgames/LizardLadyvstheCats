using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonecollision : baddie {
    public GameObject parent;
    public int power;
    baddie bad;
    public override void takehit(int strength)
    {
        
        bad.takehit(strength*power);
    }
    // Use this for initialization
    void Start () {
		bad = parent.GetComponent<baddie>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (bad.dead)
            Destroy(gameObject);
	}
}
