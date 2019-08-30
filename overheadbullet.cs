using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overheadbullet : MonoBehaviour {


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
    gameObject.transform.position = new Vector3(gameObject.transform.position.x + (gameObject.transform.forward.x / 3), gameObject.transform.position.y + (gameObject.transform.forward.y / 3), gameObject.transform.position.z + (gameObject.transform.forward.z / 3));
}
}
