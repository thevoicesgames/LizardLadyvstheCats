using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : baddie {
    Vector3 velocity;
    bool nottracking;
    public GameObject explosion;
    public bool twodee;
    float speedlimiter;
	// Use this for initialization
	void Start () {
        if (twodee)
        {
            speedlimiter = 10;
        }
        else
            speedlimiter = 2;
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "aimbox")
        {
            Instantiate(explosion, gameObject.transform.position, new Quaternion());
            Destroy(gameObject);
        }
    }
    public override void takehit(int strength)
    {
        Instantiate(explosion, gameObject.transform.position, new Quaternion());
        Destroy(gameObject);
    }
        // Update is called once per frame
        void FixedUpdate () {
        if (!nottracking)
        {

            velocity = gameObject.transform.forward / speedlimiter;
            gameObject.transform.position += velocity;
            gameObject.transform.forward = Vector3.Slerp(gameObject.transform.forward, playermovement.target - gameObject.transform.position, 0.3f);
            if (Vector3.Distance(gameObject.transform.position, playermovement.target) < 3)
            {
                nottracking = true;
            }
            if (twodee)
            {
                gameObject.transform.position = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        else
        {
            velocity += gameObject.transform.forward / 100;
            gameObject.transform.position += velocity;
        }
            //Quaternion rotat = Quaternion.FromToRotation(gameObject.transform.forward, direction);
            //gameObject.transform.Rotate(rotat.x,rotat.y,rotat.z);
            //gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.forward, direction);
        }
}
