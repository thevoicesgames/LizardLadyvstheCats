using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytracker : MonoBehaviour
{
    public UnityEngine.UI.Text enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies.text = baddie.enemynumber.ToString();  
    }
}
