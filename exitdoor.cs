using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitdoor : MonoBehaviour
{
    public GameObject exithole;
    public GameObject hinge;
    int hingecount;
    // Start is called before the first frame update
    void Start()
    {
        exithole.active = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(baddie.enemynumber<=0)
        {
            exithole.active = true;
            if (hingecount < 90)
            {
                hinge.transform.RotateAroundLocal(new Vector3(0, 1, 0), 0.015f);
                hingecount++;
            }
        }
        
    }
}
