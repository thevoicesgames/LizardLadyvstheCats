using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twdeeenemybullet : MonoBehaviour {


    // Use this for initialization
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag!="baddie" && other.gameObject.tag!="playerbullet")
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       // gameObject.transform.Translate(0, 0, 0.00001f);
        gameObject.transform.position = new Vector3(0, gameObject.transform.position.y+(gameObject.transform.forward.y/200), gameObject.transform.position.z+ (gameObject.transform.forward.z / 200));
    }
}