using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloneshoot : MonoBehaviour {
    public GameObject bullet;
    int shootcount;
    public GameObject levelrotate;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        shootcount++;
        if(shootcount > 100)
        {

            GameObject bll = Instantiate(bullet, gameObject.transform.position, new Quaternion());
            bll.transform.parent = levelrotate.transform;
            shootcount = 0;
        }
	}
}
