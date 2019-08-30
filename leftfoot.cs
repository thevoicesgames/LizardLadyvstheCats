using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftfoot : MonoBehaviour {

    public bool standing;
    public float pos;
    BoxCollider coll;
    // Use this for initialization
    void Start () {
        
        
    }



    // Update is called once per frame
    void FixedUpdate () {

        bool overlap = Physics.CheckBox(gameObject.transform.position, new Vector3(0.01f, 0.001f, 0.01f), gameObject.transform.rotation, LayerMask.GetMask("Default"));
        if(overlap)
        {
            pos = gameObject.transform.position.y;
            standing = true;
            while(overlap)
            {
                pos += 0.001f;
                overlap = Physics.CheckBox(new Vector3(gameObject.transform.position.x,pos+2,gameObject.transform.position.z), new Vector3(0.01f, 0.02f, 0.01f), gameObject.transform.rotation, LayerMask.GetMask("Default"));
            }
           
        }
        else
        standing = false;
      

    }


}
