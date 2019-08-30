using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baddie : MonoBehaviour {
    public bool attacking;
    public static int enemynumber;
    public bool dead;
    public Vector3 top;
    public bool cube;
    public float gravity;
    public int health;
    public virtual void takehit(int strength)
    {
        Destroy(gameObject);
    }
    
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
