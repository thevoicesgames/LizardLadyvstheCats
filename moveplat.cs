using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplat : MonoBehaviour {
    public float x;
    public float y;
    public float z;
    public float xdist;
    public float ydist;
    public float zdist;
    float xcount;
    float ycount;
    float zcount;
    GameObject player;
    bool standing;
    public bool activated;
    Vector3 previousposition;
    //public UnityEngine.UI.Text st;
	// Use this for initialization
	void Start () {
        standing = false;
       // st.text = "no";
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // other.transform.parent = null;
                standing = true;
              player = other.gameObject;
                //other.gameObject.transform.SetParent(gameObject.transform, true);*/
            activated = true;
            //st.text = "yes";


        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // other.transform.parent = null;
            standing = true;
            player = other.gameObject;
            //other.gameObject.transform.SetParent(gameObject.transform, true);*/
            activated = true;
            //st.text = "yes";


        }
    }
    void OnTriggerExit(Collider other)
    {
       if (other.gameObject.tag == "Player")
        {
            if (standing == true)
            {
                standing = false;
                //other.transform.parent = null;
            }

            //playermovement.moveDirection.x += x;
           //playermovement.moveDirection.z += z;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (activated)
        {
            xcount += x;
            ycount += y;
            zcount += z;
            if (xcount >= xdist || xcount <= -xdist)
            {
                x = -x;
                xcount = 0;
            }
            if (ycount >= ydist || ycount <= -ydist)
            {
                y = -y;
                ycount = 0;
            }
            if (zcount >= zdist || zcount <= -zdist)
            {
                z = -z;
                zcount = 0;
            }
            previousposition = gameObject.transform.position;
            gameObject.transform.Translate(x, y, z);
            if (standing == true)
            {
                //player.GetComponent<CharacterController>().Move(gameObject.transform.position - previousposition);
                Vector3 moveamount = gameObject.transform.position - previousposition;
               player.transform.position = new Vector3(player.gameObject.transform.position.x + moveamount.x, player.gameObject.transform.position.y + moveamount.y, player.gameObject.transform.position.z + moveamount.z);
            }
        }
    }
    
}
