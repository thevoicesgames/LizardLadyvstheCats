using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemygun : MonoBehaviour {
    public GameObject bulletorigin;
    public GameObject bullet;
    public GameObject muzzleflash;
    public GameObject levelrotate;
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	public void Shoot()
    {
       
       GameObject bull =  Instantiate(bullet, bulletorigin.transform.position, bulletorigin.transform.rotation);
        Vector3 targ = playermovement.target;
        targ.y += 1.15f;
        bull.transform.forward =  targ - gameObject.transform.position;
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
