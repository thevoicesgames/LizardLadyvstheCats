using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spheregravity : MonoBehaviour {
   public GameObject levelobject;
    public float gravity;
    public bool reversed;
    public bool cylinder;
    public bool sideways;
  
    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            levelobject.GetComponent<rotatelevel>().sphere = true;
            levelobject.GetComponent<rotatelevel>().inspheregravity = true;
            levelobject.GetComponent<rotatelevel>().spheregravityregion = gameObject;
            if (!levelobject.GetComponent<rotatelevel>().incubegravity)
            {
                levelobject.GetComponent<rotatelevel>().cube = false;
                levelobject.GetComponent<rotatelevel>().player.GetComponent<playermovement>().gravity = gravity;
            }
            levelobject.GetComponent<rotatelevel>().reversesphere = reversed;
            if(cylinder == true)
            levelobject.GetComponent<rotatelevel>().cylinder = true;
            if (sideways == true)
                levelobject.GetComponent<rotatelevel>().sideways = true;

        }
        if(other.gameObject.tag == "baddie")
        {

            if (!other.gameObject.GetComponent<baddie>().cube)
            {
                other.gameObject.GetComponent<baddie>().top = other.gameObject.transform.position - gameObject.transform.position;
                if (reversed)
                    other.gameObject.GetComponent<baddie>().top = -other.gameObject.GetComponent<baddie>().top;
                other.gameObject.GetComponent<baddie>().gravity = gravity;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "baddie")
        {
            if (!other.gameObject.GetComponent<baddie>().cube)
            {
                other.gameObject.GetComponent<baddie>().top = other.gameObject.transform.position - gameObject.transform.position;
                if (reversed)
                    other.gameObject.GetComponent<baddie>().top = -other.gameObject.GetComponent<baddie>().top;
                other.gameObject.GetComponent<baddie>().gravity = gravity;
            }
           }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (levelobject.GetComponent<rotatelevel>().spheregravityregion == gameObject)
            {
                levelobject.GetComponent<rotatelevel>().inspheregravity = false;
                levelobject.GetComponent<rotatelevel>().cylinder = false;
                levelobject.GetComponent<rotatelevel>().sideways = false;
            }
        }
    }
            // Use this for initialization
            void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
      /*  if (overlap)
        {
            Vector3 direction = playermovement.target - gameObject.transform.position;
            direction.Normalize();
            //Quaternion qua = Quaternion.Slerp(Quaternion.)
            float down = Vector3.Angle( direction,Vector3.up);
            Vector3 cros = Vector3.Cross(direction, Vector3.up);
     
            down = down * Mathf.Deg2Rad;
           if(cros.x > 0)
            {
                left = false;
               
            }
            if (cros.x < 0)
            {
                left = true;

            }
            if (left)
            {
                down = -down;
            }
            levelobject.transform.RotateAround(playermovement.target, new Vector3(1, 0, 0), down*10);
            //levelobject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, direction);
            tex.text = cros.ToString(); ;
            tex2.text = direction.ToString(); ;
        }*/
    }
}
