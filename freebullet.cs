using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freebullet : MonoBehaviour {
    public Vector3 direction;
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "baddie" || other.gameObject.tag == "twistedclone")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<baddie>().takehit(1);

        }
        if (other.gameObject.tag != "enemybullet")
            Destroy(gameObject);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // gameObject.transform.Translate(0, 0, 0.00001f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (direction.x/2), gameObject.transform.position.y + (direction.y/2), gameObject.transform.position.z + (direction.z/2));
    }
}
