using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnlizard : MonoBehaviour
{
    public GameObject lizard;
    public int spawninterval;
    int count;
    int spawndrop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;
        if (count > spawninterval && baddie.enemynumber < 30)
        {
            int escape=0;
            float x = Random.Range(-10000, 10000);
           float  z = Random.Range(-10000, 10000);
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(new Vector3(x, x, z));
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().Move(new Vector3(x, z, z));
            if (Vector3.Distance(playermovement.target, gameObject.transform.position) < 30)
                return;
           

            Instantiate(lizard, gameObject.transform.position, new Quaternion());
            count = 0;
            spawndrop++;
        }
        if(spawndrop > 5)
        {
            spawndrop = 0;
            if(spawninterval>30)
            spawninterval-=10;
        }
    }
}
