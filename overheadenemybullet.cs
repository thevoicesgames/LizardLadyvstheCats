using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overheadenemybullet : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Translate(0, 0, 0.05f);
    }
}
